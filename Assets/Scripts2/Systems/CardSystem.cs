using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Systems/Card System")]
public class CardSystem : ScriptableObject
{
    public PlayerSystem playerSystem;
    public ActionSystem actionSystem;
    public ResourceSystem resourceSystem;
    public CardInstanceVariable lastDrawnCard;
    public GameEvent OnCardPlacedOnTableEvent;
    public GameEvent OnCardDrawnEvent;
    public GameEvent BeforeAttackActionEvent;

    public void UseCard(CardInstance card, Target target)
    {
        if(card.numberOfUsesThisTurn < card.card.useLimit)
        {

            if (TargetingSystem.IsValid(card.card.validTargets, target, true))
            {
                actionSystem.ExecuteAction(new AttackAction(target, card, -1));
                card.numberOfUsesThisTurn++;
            }
        }
    }

    public void ResetCardUsage()
    {
        List<CardInstance> cardsToReset;

        cardsToReset = playerSystem.player.table;

        if(cardsToReset != null)
        {
            foreach (CardInstance card in cardsToReset)
            {
                card.numberOfUsesThisTurn = 0;
            }
        }
        
        cardsToReset = playerSystem.enemy.table;
         
        if (cardsToReset != null)
        {
            foreach (CardInstance card in cardsToReset)
            {
                card.numberOfUsesThisTurn = 0;
            }
        }
        
    }

    public void DrawCardFromDeck()
    {
        List<Card> cardsInDeck = new List<Card>();
        cardsInDeck.Shuffle();

        if (playerSystem.currentPlayer.Get().deck.Count > 0)
        {
            CardInstance drawnCard = playerSystem.currentPlayer.Get().deck[0];
            drawnCard.area = Areas.Hand;
            drawnCard.owner = playerSystem.currentPlayer.Get();
            playerSystem.currentPlayer.Get().deck.RemoveAt(0);
            playerSystem.currentPlayer.Get().hand.Add(drawnCard);
            lastDrawnCard.CardInstance = drawnCard;
            Debug.Log(lastDrawnCard.CardInstance);
            new DrawCardCommand(drawnCard).AddToQueue();
            OnCardDrawnEvent.Raise();
        }
    }

    public void PlaceCardOnTable(CardView placedCardView, GameObject tableView)
    {
        CardInstance cardToPlace = placedCardView.cardInstance;
        if(cardToPlace.cost <= playerSystem.currentPlayer.value.resources.value)
        {
            if(cardToPlace.card.type != CardType.Spell)
            {
                resourceSystem.WithdrawResources(playerSystem.currentPlayer.value, cardToPlace.cost);
                playerSystem.currentPlayer.Get().hand.Remove(cardToPlace);
                cardToPlace.area = Areas.Table;
                playerSystem.currentPlayer.Get().table.Add(cardToPlace);
                if (cardToPlace.abilityInstance != null && cardToPlace.card.ability is PassiveAbility)
                {
                    Debug.Log("Registering passive ability " + cardToPlace.card.ability.name + " ...");
                    ((PassiveAbilityInstance)cardToPlace.abilityInstance).RegisterAbility();
                }
                new PlaceCardOnTableCommand(placedCardView, tableView.GetComponent<PlayerTableView>()).AddToQueue();
                OnCardPlacedOnTableEvent.Raise();
            }
            
        }
        
    }

    private bool CheckIfCardInPlayersHand(CardViewVariable cardViewVar)
    {
        return playerSystem.currentPlayer.Get().hand.Contains(cardViewVar.Get().cardInstance);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Systems/Game System")]
public class GameSystem : ScriptableObject
{
    public Encounter encounter;
    public StateVariable currentState;
    public PlayerData playerData;
    public PlayerData enemyData;
    public GameEvent OnChangeTurnEvent;
    public PlayerSystem playerSystem;
    public CardSystem cardSystem;
    public ResourceSystem resourceSystem;
    public ActionSystem actionSystem;
    public BoolVariable gameEnded;
    public BoolVariable isAnimating;
    public static GameSystem Instance;
   
    private void OnEnable()
    {
        Instance = this;
    }

    public void ChangeState(State state)
    {
        this.currentState.Set(state);
    }

    public void InitializeGame()
    {
        SetupPlayer(encounter.player, playerData);
        SetupPlayer(encounter.enemy, enemyData);
        gameEnded.value = false;
        isAnimating.value = false;
    }

    private void SetupPlayer(Player player, PlayerData data)
    {
        player.hp.value = data.hp;
        player.resources.value = data.resources;
        player.side = data.side;
        player.name = data.name;

        player.hand.Clear();
        player.table.Clear();
        player.graveyard.Clear();
        player.deck.Clear();
        foreach(Card card in data.deck.cardsInDeck)
        {
            CardInstance cardInstance = new CardInstance(card);
            cardInstance.area = Areas.Deck;
            cardInstance.owner = player;
            player.deck.Add(cardInstance);
        }

        player.deck.Shuffle();
    }

    public void ChangeTurn()
    {
        encounter.ChangeTurn();
        OnChangeTurnEvent.Raise();
        cardSystem.DrawCardFromDeck();
        resourceSystem.AddResources(playerSystem.currentPlayer.value, 1);
        new ChangeTurnCommand().AddToQueue();
    }


}

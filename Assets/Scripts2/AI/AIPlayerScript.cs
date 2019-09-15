using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerScript : MonoBehaviour
{
    public GameEvent changeTurnEvent;
    public PlayerVariable currentPlayer;
    public Player aiPlayerData;
    public Player enemyData;
    public GameSystem gameSystem;
    public CardSystem cardSystem;
    public BoolVariable isAnimating;
    public GameObject tableView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating.value == false && currentPlayer.value == aiPlayerData)
        {
            PlayCards();
            UseCardsOnTable();
            UseSpells();
            gameSystem.ChangeTurn();
        }
        
    }

    private void PlayCards()
    {
        if (HasCards())
        {
            foreach(CardInstance card in aiPlayerData.hand)
            {
                if (HasResources(card))
                {
                    gameSystem.cardSystem.PlaceCardOnTable(card.cardView, tableView);
                }
            }
        }
    }

    private void UseSpells()
    {
        foreach(CardInstance card in aiPlayerData.hand)
        {
            if(card.card.type == CardType.Spell)
            {
                SpellAbility ability = ((SpellAbility)card.card.ability);
                int numberOfTargets = ability.numberOfTargets;
                List<Target> targets = new List<Target>();

                if (ability.validTargets == TargetOptions.EnemyCard)
                {
                    targets = findCardsToDamage(numberOfTargets);
                }

                if (ability.validTargets == TargetOptions.AllyCard)
                {
                    targets = findCardsToHeal(numberOfTargets);
                }

                if(targets.Count > 0)
                {
                    ability.ActivateAbility(targets, card.abilityInstance);
                }
            }
        }
    }

    private List<Target> findCardsToDamage(int amount)
    {
        List<Target> cardsToDamage = new List<Target>();
        CardInstance candidate = null;

        for (int i = 0; i < amount; i++)
        {
            candidate = null;

            if(enemyData.table.Count > 0)
            {
                foreach (CardInstance card in enemyData.table)
                {
                    if (candidate == null)
                    {
                        if (!cardsToDamage.Contains(card))
                        {
                            candidate = card;
                        }
                    }
                    else
                    {
                        if (candidate.power < card.power && !cardsToDamage.Contains(candidate))
                        {
                            candidate = card;
                        }
                    }
                }

                if(candidate != null)
                {
                    cardsToDamage.Add(candidate);
                }
            }
        }

        return cardsToDamage;
    }

    private List<Target> findCardsToHeal(int amount)
    {
        List<Target> cardsToHeal = new List<Target>();
        CardInstance candidate = null;

        for (int i = 0; i < amount; i++)
        {
            candidate = null;

            if (aiPlayerData.table.Count > 0)
            {
                foreach (CardInstance card in aiPlayerData.table)
                {
                    if (candidate == null)
                    {
                        if(card.power < card.card.power)
                        {
                            candidate = card;
                        }
                    }
                    else
                    {
                        if (candidate.power > card.power && card.power < card.card.power && !cardsToHeal.Contains(candidate))
                        {
                            candidate = card;
                        }
                    }
                }
                if (candidate != null)
                {
                    cardsToHeal.Add(candidate);
                }

            }
        }

        return cardsToHeal;
    }

    private void UseCardsOnTable()
    {
        foreach (CardInstance card in aiPlayerData.table)
        {
            while (card.numberOfUsesThisTurn < card.card.useLimit)
            {
                Debug.Log("Using card " + card.card.name + " ...");
                cardSystem.UseCard(card, determineBestTarget());
            }
        }
    }

    private bool HasCards()
    {
        if(aiPlayerData.hand.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool HasResources(CardInstance card)
    {
        if(aiPlayerData.resources.value >= card.cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Target determineBestTarget()
    {
        int enemyTablePower = 0;
        int aiTablePower = 0;
        int enemyHP = enemyData.hp.value;
        bool blockerOnEnemyTable = false;
        bool blockerOnAiTable = false;

        foreach(CardInstance card in enemyData.table)
        {
            enemyTablePower += card.power;
            if(card.card.type == CardType.Blocker)
            {
                blockerOnEnemyTable = true;
            }
        }

        foreach (CardInstance card in aiPlayerData.table)
        {
            if(card.card.useLimit > 0)
            {
                for (int i = 0; i < card.card.useLimit; i++)
                {
                    aiTablePower += card.power;
                }
            }
            
            if(card.card.type == CardType.Blocker)
            {
                blockerOnAiTable = true;
            }
        }

        //first, prevent losing
        if(enemyTablePower > aiPlayerData.hp.value && aiTablePower < enemyData.hp.value)
        {
            return findMostDangerousCard(enemyData.table);  
        }

        if(enemyData.hp.value <= aiTablePower)
        {
            return enemyData;
        }

        Debug.Log("AI attacks player...");
        return enemyData;
    }

    private Target findMostDangerousCard(List<CardInstance> candidateCards)
    {
        CardInstance cardToAttack = null;
        foreach (CardInstance card in candidateCards)
        {
            if(cardToAttack == null)
            {
                cardToAttack = card;
            }
            else if(cardToAttack.power < card.power && card.card.useLimit > 0)
            {
                cardToAttack = card;
            }
        }

        return cardToAttack;
    }

    
}

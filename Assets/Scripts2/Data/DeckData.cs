using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Data/Deck Data")]
public class DeckData : ScriptableObject
{
    public List<Card> cardsInDeck = new List<Card>();
   

    void Awake()
    {
        cardsInDeck.Shuffle();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Player owner;
    public PlayerVariable currentPlayer;
    public CardInitializer cardInitializer;

    public void DrawCard(CardInstance cardInstance)
    {
        if(owner == currentPlayer.Get())
        {
            cardInitializer.SpawnCard(cardInstance);
        }
    }

    
}

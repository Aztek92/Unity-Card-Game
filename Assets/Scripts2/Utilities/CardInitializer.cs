using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInitializer : MonoBehaviour
{
    public CardViewLogic cardViewLogic;
    public GameObject spawnLocationView;
    public GameObject cardPrefab;
    public Player owner;
    public Areas cardArea;
    
    public void SpawnCard(CardInstance cardInstance)
    {
        //Instantiate prefab with spawn location view as parent
        var cardClone = Instantiate(cardPrefab, spawnLocationView.transform);
        
        //Assign card instance to card view
        cardClone.GetComponent<CardView>().cardInstance = cardInstance;
        cardClone.GetComponent<CardView>().currentLogic = cardViewLogic;
        

        cardClone.GetComponent<CardView>().enabled = true;

        cardInstance.cardView = cardClone.GetComponent<CardView>();

    }
}

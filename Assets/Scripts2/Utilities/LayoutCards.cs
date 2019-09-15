using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LayoutCards : MonoBehaviour {

    public Canvas layoutSpaceCanvas;
    public float gap = 0.2f;
    public float margin = 0.4f;

    private float cardWidth = 0;
    private int numberOfCards = 0;
    private float centeringOffset = 0;
    private float usedGap = 0;
    private float currentX = 0;
    private float spaceRequired = 0;
    private float availableSpace = 0;
    private CardView[] cardViews;

    private void OnTransformChildrenChanged()
    {
        UpdateParameters();
        
        for (int i = 0; i < cardViews.Length; i++)
        {
            currentX = currentX + positionCard(cardViews[i]);
        }
    }

    private void UpdateParameters()
    {
        cardViews = GetComponentsInChildren<CardView>();
        numberOfCards = cardViews.Length;
        usedGap = gap;
        currentX = 0;

        if (numberOfCards > 0)
        {
            Canvas cardCanvas = cardViews[0].GetComponentInChildren<Canvas>();
            cardWidth = cardCanvas.GetComponent<RectTransform>().rect.width * cardCanvas.GetComponent<RectTransform>().localScale.x;
            availableSpace = layoutSpaceCanvas.GetComponent<RectTransform>().rect.width * cardCanvas.GetComponent<RectTransform>().localScale.x;
            spaceRequired = numberOfCards * cardWidth + (numberOfCards - 1) * usedGap;
            centeringOffset = (spaceRequired / 2F) - (cardWidth / 2F);
            
            if (spaceRequired >= availableSpace)
            {
                usedGap = maxPossibleGap();
                spaceRequired = numberOfCards * cardWidth + (numberOfCards - 1) * usedGap;
                centeringOffset = (spaceRequired / 2F) - (cardWidth / 2F);
            }
        }
        
    }
    
    private float positionCard(CardView cardView)
    {
        cardView.transform.localPosition = new Vector3(0, 0, cardView.transform.localPosition.z);
        cardView.transform.position += new Vector3(currentX - centeringOffset, 0, 0);
        return cardWidth + usedGap;
    }

    private float maxPossibleGap()
    {
        float spaceDifference = (availableSpace - margin) - cardWidth * numberOfCards;
        float maxPossibleGap = spaceDifference / (numberOfCards - 1);
        return maxPossibleGap;
    }
}

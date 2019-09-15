using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeTurnButton : MonoBehaviour, IPointerClickHandler
{
    public GameSystem gameSystem;
    public BoolVariable isAnimating;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isAnimating.value)
        {
            Debug.Log("Clicked change turn button ...");
            //gameSystem.ChangeTurn();
        }
    }
}

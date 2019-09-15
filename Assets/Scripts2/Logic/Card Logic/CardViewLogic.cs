using UnityEngine;
using UnityEditor;

public abstract class CardViewLogic : ScriptableObject
{
    public abstract void OnClick(CardView card);
    public abstract CardView GetCurrentCard();
}
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Game/Variables/Card Instance Variable")]
public class CardInstanceVariable : ScriptableObject
{
    private CardInstance cardInstance;
    public CardInstance CardInstance{ get; set; }
}
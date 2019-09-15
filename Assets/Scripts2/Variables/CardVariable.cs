using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "Game/Variables/Card Variable")]
[Serializable]
public class CardVariable : ScriptableObject
{
    [SerializeField]
    public Card value;
    
    public Card Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }
}
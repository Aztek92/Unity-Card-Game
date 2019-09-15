using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Game/Variables/Turn Variable")]
public class TurnVariable : ScriptableObject
{
    private Turn value;

    public void Set(Turn instance)
    {
        this.value = instance;
    }

    public Turn Get()
    {
        return value;
    }
}
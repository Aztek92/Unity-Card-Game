using UnityEngine;
using UnityEditor;
[CreateAssetMenu(menuName = "Game/Variables/State Variable")]
public class StateVariable : ScriptableObject
{
    private State value;

    public void Set(State state)
    {
        this.value = state;
    }

    public State Get()
    {
        return this.value;
    }
}
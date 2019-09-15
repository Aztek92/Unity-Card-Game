using UnityEngine;
using UnityEditor;

public abstract class ScriptableEventListener : ScriptableObject
{
    public abstract void OnEventRaised();
}
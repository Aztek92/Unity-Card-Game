using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();
    private List<ScriptableEventListener> scriptableListeners = new List<ScriptableEventListener>();
    private List<PassiveAbility> passiveAbilities = new List<PassiveAbility>();
    private List<PassiveAbilityInstance> abilities = new List<PassiveAbilityInstance>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }

        for (int i = scriptableListeners.Count - 1; i >= 0; i--)
        {
            scriptableListeners[i].OnEventRaised();
        }
    }

    public void RaiseActionEvent(Action action)
    {
        Debug.Log("Handling raised action event...");
        for(int i = abilities.Count - 1; i >= 0; i--)
        {
            abilities[i].ActivateAbility(action);
        }
    }
    
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RegisterListener(ScriptableEventListener listener)
    {
        scriptableListeners.Add(listener);
    }

    public void RegisterListener(PassiveAbility passiveAbility)
    {
        passiveAbilities.Add(passiveAbility);
    }

    public void RegisterListener(PassiveAbilityInstance ability)
    {
        this.abilities.Add(ability);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void UnregisterListener(ScriptableEventListener listener)
    {
        scriptableListeners.Add(listener);
    }

    public void UnregisterListener(PassiveAbility passiveAbility)
    {
        passiveAbilities.Remove(passiveAbility);
    }

    public void UnregisterListener(PassiveAbilityInstance ability)
    {
        this.abilities.Remove(ability);
    }
}
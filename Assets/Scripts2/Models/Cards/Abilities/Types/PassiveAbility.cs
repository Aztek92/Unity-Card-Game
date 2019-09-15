using UnityEngine;
using UnityEditor;

public abstract class PassiveAbility : Ability
{
    public GameEvent triggerEvent;
    public abstract void ActivateAbility(Action action, PassiveAbilityInstance instance);
}
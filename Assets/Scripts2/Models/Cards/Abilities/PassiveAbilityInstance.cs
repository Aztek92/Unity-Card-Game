using UnityEngine;
using UnityEditor;

public class PassiveAbilityInstance : AbilityInstance
{
    public GameEvent ev;

    public PassiveAbilityInstance(CardInstance owner, Ability ability, GameEvent ev) : base(owner, ability)
    {
        this.ev = ev;
    }

    public void RegisterAbility()
    {
        this.ev.RegisterListener(this);
    }

    public void UnregisterAbility()
    {
        this.ev.UnregisterListener(this);
    }

    public void ActivateAbility(Action action)
    {
        Debug.Log("Activating passive ability...");
        ((PassiveAbility)ability).ActivateAbility(action, this);
    }
}
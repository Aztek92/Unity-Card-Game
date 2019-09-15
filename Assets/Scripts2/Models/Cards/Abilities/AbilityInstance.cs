using UnityEngine;
using UnityEditor;

public class AbilityInstance 
{
    public CardInstance owner;
    public Ability ability;

    public AbilityInstance(CardInstance owner, Ability ability)
    {
        this.owner = owner;
        this.ability = ability;
    }
}
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class Ability : ScriptableObject
{
    public string name;
    public virtual AbilityInstance GetInstance(CardInstance owner)
    {
        AbilityInstance instance = new AbilityInstance(owner, this);
        return instance;
    }
}
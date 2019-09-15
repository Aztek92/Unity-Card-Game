using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class SpellAbility : Ability
{
    public int numberOfTargets;
    public TargetOptions validTargets;
    public abstract void ActivateAbility(List<Target> targets, AbilityInstance instance);
}
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Cards/Abilities/Heal Spell")]
public class HealAbility : SpellAbility
{
    public int healAmount;
    
    public override void ActivateAbility(List<Target> targets, AbilityInstance instance)
    {
        if(instance.owner.owner.resources.value >= instance.owner.cost)
        {
            if (targets != null && targets.Count > 0)
            {
                GameSystem.Instance.resourceSystem.WithdrawResources(instance.owner.owner, instance.owner.cost);
                
                foreach (Target target in targets)
                {
                    target.Heal(healAmount);
                }

                new SpellCardActivateCommand(instance.owner.cardView).AddToQueue();

                instance.owner.RemoveCard();
            }
        }
        
    }
    
}
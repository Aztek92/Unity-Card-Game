using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Game/Cards/Abilities/Block Ability")]
public class BlockAbility : PassiveAbility
{
    public bool canCounter;
    public int counterPower = 1;
    public ActionSystem actionSystem;

    public override AbilityInstance GetInstance(CardInstance owner)
    {
        PassiveAbilityInstance blockAbilityInstance = new PassiveAbilityInstance(owner, this, triggerEvent);
        return blockAbilityInstance;
    }

    public override void ActivateAbility(Action action, PassiveAbilityInstance instance)
    {
        Debug.Log("Blooooock!");
        if (action is AttackAction)
        {
            CardInstance attacker = ((AttackAction)action).attacker;
            if ( attacker.owner != instance.owner.owner)
            {
                Debug.Log("Block activated ------------");
                ChangeTarget((AttackAction)action, instance.owner);
                if (canCounter)
                {
                    if(actionSystem != null)
                    {
                        actionSystem.ExecuteAction(new AttackAction(attacker, instance.owner, counterPower));
                    }
                }
            }
        }
        else
        {
            throw new System.Exception("Invalid Action Exception - Block Ability can only handle AttackAction");
        }
    }

    private void ChangeTarget(AttackAction action, CardInstance newTarget)
    {
        action.target = newTarget;
    }
}
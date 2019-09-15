using UnityEngine;
using UnityEditor;

public class AttackAction : Action
{
    public Target target;
    public CardInstance attacker;
    int damageAmount;
    
    public AttackAction(Target target, CardInstance attacker, int damageAmount = -1)
    {
        this.target = target;
        this.attacker = attacker;
        this.damageAmount = damageAmount;
    }

    public override void Execute()
    {
        Debug.Log("Executing attack action, target : " + this.target + " , attacker : " + this.attacker.owner.name);
        if(damageAmount == -1)
        {
            target.Damage(attacker.power);
        }
        else
        {
            target.Damage(damageAmount);
        }
    }
}
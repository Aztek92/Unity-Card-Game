using UnityEngine;
using UnityEditor;

public class SpellCardActivateCommand : Command
{
    private CardView cardView;

    public SpellCardActivateCommand(CardView cardView)
    {
        this.cardView = cardView;
    }

    public override void Execute()
    {
        cardView.ShowUsingAbilityAnimation();
        Complete();
    }
}
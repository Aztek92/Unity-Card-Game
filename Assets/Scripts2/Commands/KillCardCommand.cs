using UnityEngine;
using UnityEditor;
using System.Collections;

public class KillCardCommand : Command
{
    private CardInstance cardInstance;

    public KillCardCommand(CardInstance cardInstance)
    {
        this.cardInstance = cardInstance;
    }

    public override void Execute()
    {
        cardInstance.cardView.KillCardAnimation();
        Complete();
    }

    
}
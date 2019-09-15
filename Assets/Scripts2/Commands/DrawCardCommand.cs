using UnityEngine;
using UnityEditor;

public class DrawCardCommand : Command
{
    private CardInstance cardToDraw;

    public DrawCardCommand(CardInstance cardToDraw)
    {
        this.cardToDraw = cardToDraw;
    }

    public override void Execute()
    {
        if(cardToDraw.owner == GameViewSystem.Instance.playersViews.owner)
        {
            GameViewSystem.Instance.playersViews.handController.DrawCard(cardToDraw);
        }
        else
        {
            GameViewSystem.Instance.enemyViews.handController.DrawCard(cardToDraw);
        }
        
        Complete();
    }
}
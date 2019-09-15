using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UpdateCardCommand : Command
{
    private CardView damagedCard;

    public UpdateCardCommand(CardView damagedCard)
    {
        this.damagedCard = damagedCard;
    }

    public override void Execute()
    {
        Debug.Log("Card text should update ; " + damagedCard.cardInstance.card.name);
        damagedCard.UpdatePower();
        Complete();
    }
}
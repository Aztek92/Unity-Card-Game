using UnityEngine;
using UnityEditor;

public class PlaceCardOnTableCommand : Command
{
    public CardView cardView;
    public PlayerTableView tableView;

    public PlaceCardOnTableCommand(CardView cardView, PlayerTableView tableView)
    {
        this.cardView = cardView;
        this.tableView = tableView;
    }

    public override void Execute()
    {
        tableView.PlaceCard(cardView);
        Complete();
    }

}
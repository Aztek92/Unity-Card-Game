using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Game/Card/Logic/Hand Card View Logic")]
public class HandCardViewLogic : CardViewLogic
{
    public CardViewVariable currentCardView;
    public PlayerVariable currentPlayer;
    public Player localPlayer;

    public override CardView GetCurrentCard()
    {
        return currentCardView.Get();
    }

    public override void OnClick(CardView card)
    {
        if (currentPlayer.Get() == card.cardInstance.owner && localPlayer == currentPlayer.Get())
        {
            if (currentCardView.Get() == null)
            {
                currentCardView.Set(card.gameObject);
                currentCardView.Get().ToggleActive();
            }
            else
            {
                currentCardView.Get().ToggleActive();

                if (currentCardView.Get() == card)
                {
                    currentCardView.Set(null);
                }
                else
                {
                    currentCardView.Set(card.gameObject);
                    currentCardView.Get().ToggleActive();
                }
            }
        }

    }

}
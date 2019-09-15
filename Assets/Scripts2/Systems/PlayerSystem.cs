using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Game/Systems/Player System")]
public class PlayerSystem : ScriptableObject
{
    public PlayerVariable currentPlayer;
    public Player player;
    public Player enemy;
    public GameEvent OnCardDrawnEvent;
    public GameEvent OnCardPlacedOnTableEvent;
    public GameEvent OnPlayerDamaged;
    public CardInstanceVariable lastDrawnCard;
    public CardViewVariable currectCardViewActive;
    public CardSystem cardSystem;
    
   
    public void DamagePlayer(Player target, CardInstance attacker)
    {
        target.hp.value -= attacker.power;
        new UpdatePlayerHPCommand().AddToQueue();
    }

    

    public void DamagePlayer(Player player, int amount)
    {
        if (CanDamagePlayer())
        {
            player.hp.value = player.hp.value - amount;
            OnPlayerDamaged.Raise();
        }
    }

    private bool CanDamagePlayer()
    {
        return true;
    }
    
}


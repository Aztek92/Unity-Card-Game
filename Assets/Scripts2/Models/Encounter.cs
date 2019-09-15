using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Encounter")]
public class Encounter : ScriptableObject
{
    public List<Player> players = new List<Player>();
    public PlayerVariable currentPlayer;
    public TurnVariable currentTurn;
    public Turn playerTurn;
    public Turn enemyTurn;
    public Player player;
    public Player enemy;
    
    public void ChangeTurn()
    {
        if(currentTurn.Get() == playerTurn)
        {
            currentTurn.Set(enemyTurn);
            currentPlayer.Set(enemy);
            Debug.Log("Enemy Turn");
        }
        else
        {
            currentTurn.Set(playerTurn);
            currentPlayer.Set(player);
            Debug.Log("Player Turn");
        }
    }
}
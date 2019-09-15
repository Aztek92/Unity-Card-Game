using UnityEngine;
using UnityEditor;

public class EndGameCommand : Command
{
    private Player winner;

    public EndGameCommand(Player loser)
    {
        if(GameSystem.Instance.encounter.player == loser)
        {
            this.winner = GameSystem.Instance.encounter.enemy;
        }
        else
        {
            this.winner = GameSystem.Instance.encounter.player;
        }
        
    }

    public override void Execute()
    {
        GameViewSystem.Instance.EndGame(winner);
        Complete();
    }
}
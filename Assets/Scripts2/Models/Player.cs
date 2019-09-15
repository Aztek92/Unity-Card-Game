using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Game/Player")]
public class Player : Target
{
    public string name;
    public IntVariable hp;
    public IntVariable resources;
    public Side side;
        
    public List<CardInstance> hand = new List<CardInstance>();
    public List<CardInstance> table = new List<CardInstance>();
    public List<CardInstance> graveyard = new List<CardInstance>();
    public List<CardInstance> deck = new List<CardInstance>();

    public override void Damage(int amount)
    {
        this.hp.value -= amount;
        new UpdatePlayerHPCommand().AddToQueue();
        if(this.hp.value <= 0)
        {
            new EndGameCommand(this).AddToQueue();
        }
    }

    public override void Heal(int amount)
    {
        this.hp.value += amount;
        new UpdatePlayerHPCommand().AddToQueue();
    }

    public override bool IsValidTarget(TargetOptions criteria, bool playerPerspective = true)
    {
        switch (criteria)
        {
            case TargetOptions.Player:
                return (GameViewSystem.Instance.gameSystem.playerSystem.currentPlayer.value == this);
                break;
            case TargetOptions.Enemy:
                return (GameViewSystem.Instance.gameSystem.playerSystem.currentPlayer.value != this);
                break;
            case TargetOptions.CardAndEnemy:
                return (GameViewSystem.Instance.gameSystem.playerSystem.currentPlayer.value != this);
                break;
            default:
                return false;
                break;
        }
    }
}
    
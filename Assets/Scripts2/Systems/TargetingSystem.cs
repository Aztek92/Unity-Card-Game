using UnityEngine;
using UnityEditor;

public static class TargetingSystem 
{
    public static bool IsValid(TargetOptions validTargets, Target target, bool playerPerspective = true)
    {
        switch (validTargets)
        {
            case TargetOptions.CardOnTable:
                return (target.IsValidTarget(TargetOptions.CardOnTable, playerPerspective));
                break;
            case TargetOptions.CardAndEnemy:
                return target.IsValidTarget(TargetOptions.CardAndEnemy, playerPerspective);
                break;
            case TargetOptions.Player:
                return target.IsValidTarget(TargetOptions.Player, playerPerspective);
                break;
            case TargetOptions.Enemy:
                return target.IsValidTarget(TargetOptions.Enemy, playerPerspective);
                break;
            case TargetOptions.AllyCard:
                return target.IsValidTarget(TargetOptions.AllyCard, playerPerspective);
            case TargetOptions.EnemyCard:
                return target.IsValidTarget(TargetOptions.EnemyCard, playerPerspective);
            default:
                return false;
                break;
        }
    }
}
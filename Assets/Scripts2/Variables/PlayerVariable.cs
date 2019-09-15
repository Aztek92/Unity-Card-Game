using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Game/Variables/PlayerVariable")]
public class PlayerVariable : ScriptableObject
{
    public Player value;

    public void Set(Player player)
    {
        this.value = player;
    }

    public Player Get()
    {
        return this.value;
    }
}
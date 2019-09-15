using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Game/Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public string name;
    public int hp;
    public int resources;
    public Side side;
    public DeckData deck;
}
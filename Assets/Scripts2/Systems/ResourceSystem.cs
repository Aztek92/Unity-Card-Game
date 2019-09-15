using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Game/Systems/Resources System")]
public class ResourceSystem : ScriptableObject
{
    public GameEvent resourcesTextUpdateEvent;

    public void AddResources(Player player, int amount)
    {
        player.resources.value += amount;
        updateResourcesText(player);
    }

    public void WithdrawResources(Player player, int amount)
    {
        player.resources.value -= amount;
        updateResourcesText(player);
    }

    public void updateResourcesText(Player player)
    {
        resourcesTextUpdateEvent.Raise();
    }
}
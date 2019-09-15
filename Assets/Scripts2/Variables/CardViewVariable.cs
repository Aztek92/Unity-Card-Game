using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Game/Variables/Card View Variable")]
public class CardViewVariable : ScriptableObject
{
    public GameObject value = null;

    public void Set(GameObject instance)
    {
        this.value = instance;
    }

    public CardView Get()
    {
        if(value != null)
        {
            return value.GetComponent<CardView>();
        }
        else
        {
            return null;
        }
        
    }

    public void ToggleCardActive()
    {
        if(value != null)
        {
            value.GetComponent<CardView>().ToggleActive();
            value = null;
        }
    }
}
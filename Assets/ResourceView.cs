using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceView : MonoBehaviour
{

    public Player player;
    public Text resourcesText;

    public void updateResourcesText()
    {
        this.resourcesText.text = "Resources : " + player.resources.value;
    }
}

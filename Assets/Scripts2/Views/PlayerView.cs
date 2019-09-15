using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public Text HPText;
    public IntVariable playerHP;
    public Player owner;

    public void UpdatePlayerHPText()
    {
        HPText.text = playerHP.value.ToString();
    }
}

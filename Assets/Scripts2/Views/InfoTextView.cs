using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTextView : MonoBehaviour
{
    public GameObject infoText;
    public PlayerVariable currentPlayer;
    public BoolVariable isAnimating;

    public void ShowInfo()
    {
        Debug.Log("Show info"); 
        infoText.GetComponent<Text>().text = currentPlayer.Get().name + " turn";
        StartCoroutine(Visualize());
    }

    IEnumerator Visualize()
    {
        isAnimating.value = true;
        infoText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        infoText.SetActive(false);
        isAnimating.value = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CardView : MonoBehaviour
{

    public CardInstance cardInstance;
    public bool isActive = false;
    public CardViewLogic currentLogic;
    public BoolVariable isAnimating;

    public Image cardBack;
    public Image cardFront;
    public Image im;
    public Text healthText;
    public Text attackText;
    public Text manaText;
    public Text titleText;
    public Text cardText;

    public void OnEnable()
    {
        InitCardView();
    }

    private void InitCardView()
    {
        titleText.text = cardInstance.card.name;
        manaText.text = cardInstance.card.cost.ToString();
        attackText.text = cardInstance.card.power.ToString();
        cardText.text = cardInstance.card.description;
        im.sprite = cardInstance.card.artwork;
    }
    

    public void ToggleActive()
    {
        if (isActive)
        {
           
            this.transform.localScale = Vector3.one;
            this.isActive = false;
        }
        else
        {
            //this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
           this.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
            this.isActive = true;
        }
    }

   

   void OnMouseEnter()
    {
        this.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
    }

    void OnMouseExit()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }



    public void UpdatePower()
    {
        Debug.Log("Updating card " + this.cardInstance.card.name + " power text ...");
        attackText.text = cardInstance.power.ToString();
    }

    public void KillCardAnimation()
    {
        StartCoroutine(ShrinkDown(this.transform, 2));
    }

    public void ShowUsingAbilityAnimation()
    {
        StartCoroutine(ShowUsingAbility(this.transform, 2));
    }

    public void DestroyCard()
    {
        Destroy(this.gameObject);
    }



    IEnumerator ShrinkDown(Transform target, float time)
    {
        isAnimating.value = true;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            target.localScale = Vector3.Lerp(target.localScale, new Vector3(0f, 0f, 0f), (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isAnimating.value = false;
        yield return StartCoroutine(AddToGraveyardAnimation(target, 2));
        //DestroyCard();
    }



    IEnumerator AddToGraveyardAnimation(Transform target, float time)
    {
        isAnimating.value = true;

        GraveyardView[] graveyards = GameViewSystem.Instance.GetComponentsInChildren<GraveyardView>();

        foreach (GraveyardView graveyard in graveyards)
        {
            if (graveyard.owner == cardInstance.owner)
            {
                float elapsedTime = 0;

                if (this.isActive)
                {
                    ToggleActive();
                }

                target.SetParent(graveyard.transform);

                while (elapsedTime < time)
                {
                    target.transform.localScale = Vector3.Lerp(target.localScale, new Vector3(1f, 1f, 1f), (elapsedTime / time));
                    target.localPosition = Vector3.Lerp(target.localPosition, new Vector3(0f, 0f, 0f), (elapsedTime / time));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

            }
        }

        isAnimating.value = false;
        yield return null;
    }

    IEnumerator ShowUsingAbility(Transform target, float time)
    {
        isAnimating.value = true;
        this.transform.SetParent(null);

        float elapsedTime = 0;
        
        while(elapsedTime < time)
        {
            target.localPosition = Vector3.Lerp(target.localPosition, new Vector3(0f, 0f, target.localPosition.z), (elapsedTime / time));
            target.localScale = Vector3.Lerp(target.localScale, new Vector3(1.5f, 1.5f, 1.5f), (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isAnimating.value = false;
        yield return StartCoroutine(AddToGraveyardAnimation(target, 2));
        //DestroyCard();
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected bool activeControl;

    public void Activate()
    {
        Debug.Log(this.name + " takes over control...");
        this.activeControl = true;
    }

    public GameObject GetObjectClicked()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    protected abstract void HandleLeftMouseClick();

    protected void Update()
    {
        if (activeControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleLeftMouseClick();
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerTableView : MonoBehaviour
{
    public GameObject tableView;
    
    public void PlaceCard(CardView cardView)
    {
        cardView.transform.SetParent(tableView.transform);
    }
    
}

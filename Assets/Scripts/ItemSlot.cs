using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour , IDropHandler
{
    public Organ currentOrgan;

    public void OnDrop(PointerEventData eventData)
    {
       if(eventData.pointerDrag != null)
        {
            currentOrgan = eventData.pointerDrag.GetComponent<OrganHolder>().organData;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }  
}

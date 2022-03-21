using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrganSlot : MonoBehaviour, IDropHandler
{
    public Organ currentOrgan;
    public Organ[] organs;
    public AudioClip organFixed;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            currentOrgan = eventData.pointerDrag.GetComponent<OrganHolder>().organData;
            foreach (Organ o in organs)
            {
                if(currentOrgan.id == o.id && GameManager.organAnalyzed[currentOrgan])
                {
                    Debug.Log("Placed in correct slot");
                    GetComponentInChildren<ParticleSystem>().Play();
                    AudioSource.PlayClipAtPoint(organFixed, Vector3.zero);
                    GameManager.organsFixed++;
                    eventData.pointerDrag.gameObject.SetActive(false);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 //Wrong Class Name
public class DoorAnimator : MonoBehaviour
{
    
    [SerializeField] private Animator animControll;
    [SerializeField] TMPro.TextMeshPro text;

    [SerializeField] private Material portal;

    public int Number { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        portal.color = new Color(1, 0, 0, 0.1f);
        Number = Random.Range(0, 9);
        //text.gameObject.GetComponent<MeshRenderer>().material = new Material(text.gameObject.GetComponent<MeshRenderer>().material);
        text.text = Number.ToString();
    }

    private bool DoorOpen = false;

    public void TriggerDoorOpen()
    {
        animControll.SetTrigger("Open");
    }

    public void TriggerDoorClose()
    {
        animControll.SetTrigger("Close");
    }

    public int TriggerDoor()
    {
        if (DoorOpen)
        {
            TriggerDoorClose();
            DoorOpen = !DoorOpen;
            return -Number;
        }
        else
        {
            TriggerDoorOpen();
            DoorOpen = !DoorOpen;
            return Number;
        }
        
    }

    public void SetDoorColour(Color inColour)
    {
        portal.color = inColour;
    }
}

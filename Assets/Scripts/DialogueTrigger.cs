using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue d;
    public void triggerDialogue()
    {
        if (GameObject.FindGameObjectsWithTag("FCanvas").Length > 0)
        // if (GameObject.FindGameObjectsWithTag("FCanvas").Where(c => c.activeSelf).Count() > 0)
        {
            //GameObject.FindGameObjectsWithTag("FCanvas").First(c => c.activeSelf).GetComponentInChildren<DialogueManager>().StartDialogue(d);
            Canvas canvas = GameObject.FindGameObjectsWithTag("FCanvas").First(c => c.activeSelf).GetComponent<Canvas>();
            canvas.transform.parent.GetComponentInChildren<DialogueManager>().StartDialogue(d);
            //GameObject.Find("DialogueManagerFarmer").GetComponent<DialogueManager>().StartDialogue(d);
        }
        else
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartDialogue(d);
        }
        //<DialogueManager>().StartDialogue(d);
    }
    public void triggerOptions()
    {
        if (GameObject.FindGameObjectsWithTag("FCanvas").Length > 0)
        {
            //hol mir den aktiven gameobject
            GameObject.Find("DialogueManagerFarmer").GetComponent<DialogueManager>().StartOptions(d);
        }
        else
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartOptions(d);
        }
        //FindObjectOfType<DialogueManager>().StartOptions(d);
    }
}

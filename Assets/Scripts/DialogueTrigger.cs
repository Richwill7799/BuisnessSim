using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue d;
    public void TriggerDialogue()
    {
        if (GameObject.FindGameObjectsWithTag("FCanvas").Length > 0)
        {
            Canvas canvas = GameObject.FindGameObjectsWithTag("FCanvas").First(c => c.activeSelf).GetComponent<Canvas>();
            canvas.transform.parent.GetComponentInChildren<DialogueManager>().StartDialogue(d);
        }
        else
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartDialogue(d);
        }
    }
    public void TriggerOptions()
    {
        if (GameObject.FindGameObjectsWithTag("FCanvas").Length > 0)
        {
            GameObject.Find("DialogueManagerFarmer").GetComponent<DialogueManager>().StartOptions(d);
        }
        else
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartOptions(d);
        }
    }
}

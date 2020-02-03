using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue d;
    public void triggerDialogue()
    {
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartDialogue(d);
        //<DialogueManager>().StartDialogue(d);
    }
    public void triggerOptions()
    {
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartOptions(d);
        //FindObjectOfType<DialogueManager>().StartOptions(d);
    }
}

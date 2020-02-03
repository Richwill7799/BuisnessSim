using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue d;
    public void triggerDialogue()
    {
        if (GameObject.Find("FarmerCanvas"))
        {
            GameObject.Find("DialogueManagerFarmer").GetComponent<DialogueManager>().StartDialogue(d);
        }
        else
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartDialogue(d);
        }
        //<DialogueManager>().StartDialogue(d);
    }
    public void triggerOptions()
    {
        if (GameObject.Find("FarmerCanvas"))
        {
            GameObject.Find("DialogueManagerFarmer").GetComponent<DialogueManager>().StartOptions(d);
        }
        else
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartOptions(d);
        }
        //FindObjectOfType<DialogueManager>().StartOptions(d);
    }
}

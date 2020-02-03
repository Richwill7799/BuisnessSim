﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue d;
    public void triggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(d);
    }
    public void triggerOptions()
    {
        FindObjectOfType<DialogueManager>().StartOptions(d);
    }
}

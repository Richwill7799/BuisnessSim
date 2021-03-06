﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject panel;
    public Text dialogueText;
    public Animator animator;
    public Animator animatorOptions;
    //https://www.youtube.com/watch?v=_nRzoTzeyxU
    public Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        panel.SetActive(true);

        if (GameObject.Find("FarmerCanvas"))
        {
            animator = GameObject.Find("FarmerCanvas").GetComponentsInChildren<Animator>().First(x => x.name.Equals("DialougeBoxFarmer"));
        }
        else
        {
            animator = GameObject.Find("MainCanvas").GetComponentsInChildren<Animator>().First(x => x.name.Equals("DialougeBox"));
        }

        animator.SetBool("IsOpen", true);
        Debug.Log("Starting tutorial");
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);

        }
        DisplayNextSentence();
    }
    public void StartOptions(Dialogue dialogue)
    {
        animatorOptions = GameObject.Find("MainCanvas").GetComponentsInChildren<Animator>().First(x => x.name.Equals("OpionsBox"));
        animatorOptions.SetBool("IsOpen", true);
        panel.SetActive(true);

        Debug.Log("OpenOptions");
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);

        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);
    }
    public void EndDialogue()
    {
        panel.SetActive(false);

        animator.SetBool("IsOpen", false);
    }
    public void EndOptions()
    {
        panel.SetActive(false);

        animatorOptions.SetBool("IsOpen", false);
    }
}

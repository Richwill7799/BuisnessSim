using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // public Text mainText;
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
        animatorOptions =  GameObject.Find("MainCanvas").GetComponentsInChildren<Animator>().First(x=>x.name.Equals("OpionsBox"));
        animatorOptions.SetBool("IsOpen", true);
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
        animator.SetBool("IsOpen", false);
        // Debug.Log("End of tutorial");
    }
    public void EndOptions()
    {
        animatorOptions.SetBool("IsOpen", false);
        // Debug.Log("End of tutorial");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private int defaultTime = 1;


    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;
    private Queue<int> sentencesSecondsBeforeNext;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        sentencesSecondsBeforeNext = new Queue<int>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name + ":";

        sentences.Clear();
        sentencesSecondsBeforeNext.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (int secondsVisable in dialogue.SecondsVisableSentence)
        {
            sentencesSecondsBeforeNext.Enqueue(secondsVisable);
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
        int secondsBeforeNext = defaultTime;
        if (sentencesSecondsBeforeNext.Count > 0)
        {
            secondsBeforeNext = sentencesSecondsBeforeNext.Dequeue();
        }
       
            
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, secondsBeforeNext));
    }

    IEnumerator TypeSentence(string sentence, int secondsBeforeNext)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    
            yield return new WaitForSeconds(secondsBeforeNext);
      
        
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

}
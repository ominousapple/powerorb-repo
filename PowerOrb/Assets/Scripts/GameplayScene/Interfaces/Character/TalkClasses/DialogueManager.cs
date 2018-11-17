using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private int defaultTime = 1;

    public GameObject CanvasParentOfDialogue;
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;
    private Queue<int> sentencesSecondsBeforeNext;

    // Use this for initialization
    void Start()
    {
        CanvasParentOfDialogue.GetComponent<Canvas>().worldCamera = Camera.main;
        CanvasParentOfDialogue.SetActive(false);
        sentences = new Queue<string>();
        sentencesSecondsBeforeNext = new Queue<int>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        CanvasParentOfDialogue.SetActive(true);
        if (animator != null)
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
        if (animator != null)
            animator.SetBool("IsOpen", false);
        CloseCanvasParent();


    }

    public void CloseCanvasParent() {
        CanvasParentOfDialogue.SetActive(false);
    }

}
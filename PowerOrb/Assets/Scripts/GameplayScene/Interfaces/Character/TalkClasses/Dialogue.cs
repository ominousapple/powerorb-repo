using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Dialogue(int numberOfSentences) {
        sentences = new string[numberOfSentences];
        SecondsVisableSentence = new int[numberOfSentences];
    }
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    public int[] SecondsVisableSentence;

}
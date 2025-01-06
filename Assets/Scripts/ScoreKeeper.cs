using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int CorrectQuestions { get; private set; }
    public int QuestionsSeen { get; private set; }
    public int Score => Mathf.RoundToInt((float)this.CorrectQuestions / this.QuestionsSeen * 100);

    public void IncrementCorrectQuestions()
    {
        this.CorrectQuestions++;
    }

    public void IncrementQuestionsSeen()
    {
        this.QuestionsSeen++;
    }
}

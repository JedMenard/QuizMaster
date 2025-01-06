using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    #region Fields

    [Header("Questions")]

    [SerializeField]
    private List<QuestionSO> questions;

    [SerializeField]
    private TextMeshProUGUI questionText;

    [Header("Answers")]

    [SerializeField]
    private GameObject[] answerButtons = new GameObject[4];

    [Header("Button colors")]

    [SerializeField]
    private Sprite defaultAnswerSprite;

    [SerializeField]
    private Sprite correctAnswerSprite;

    [Header("Timer")]

    [SerializeField]
    private Image timerImage;

    [Header("Scoring")]

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("Progress bar")]
    [SerializeField]
    private Slider slider;

    #endregion

    #region Properties

    private Timer timer;

    private bool hasAnsweredEarly = false;

    private QuestionSO currentQuestion;

    private ScoreKeeper scoreKeeper;

    public bool showWinScreen = false;

    #endregion

    #region Methods

    private void Awake()
    {
        this.timer = FindObjectOfType<Timer>();
        this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
        this.slider.value = 0;
        this.slider.maxValue = this.questions.Count;
    }

    private void Update()
    {
        this.timerImage.fillAmount = this.timer.FillFraction;

        if (this.timer.LoadNextQuestion)
        {
            this.hasAnsweredEarly = false;
            this.GetNextQuestion();
        }
        else if (!this.hasAnsweredEarly && !this.timer.IsAnsweringQuestion)
        {
            this.DisplaySelectedAnswer(null);
            this.SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        this.hasAnsweredEarly = true;
        DisplaySelectedAnswer(index);
        this.SetButtonState(false);
        this.timer.CancelTimer();
    }

    private void DisplaySelectedAnswer(int? index)
    {
        Image image = answerButtons[this.currentQuestion.CorrectAnswerIndex].GetComponentInChildren<Image>();
        image.sprite = correctAnswerSprite;

        if (index == this.currentQuestion.CorrectAnswerIndex)
        {
            this.questionText.text = "Correct!";
            this.scoreKeeper.IncrementCorrectQuestions();
        }
        else
        {
            this.questionText.text = "I'm sorry, that's not the right answer." + '\n'
                + "The correct answer was:" + '\n'
                + this.currentQuestion.GetAnswer(this.currentQuestion.CorrectAnswerIndex);
        }

        this.scoreText.text = $"Score: {this.scoreKeeper.Score}%";
    }

    private void GetNextQuestion()
    {
        if (this.questions.Count == 0)
        {
            this.showWinScreen = true;
            return;
        }

        this.SetButtonState(true);
        this.ResetButtonSprites();
        this.GetRandomQuestion();
        this.DisplayQuestion();
        timer.LoadNextQuestion = false;
        this.scoreKeeper.IncrementQuestionsSeen();
        this.slider.value++;

    }

    private void GetRandomQuestion()
    {
        int ndx = Random.Range(0, this.questions.Count);
        this.currentQuestion = this.questions[ndx];
        this.questions.RemoveAt(ndx);
    }

    private void DisplayQuestion()
    {
        this.questionText.text = currentQuestion.QuestionText;

        for (int i = 0; i < this.answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = this.answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    private void SetButtonState(bool state)
    {
        foreach (GameObject answerButton in this.answerButtons)
        {
            answerButton.GetComponentInChildren<Button>().interactable = state;
        }
    }

    private void ResetButtonSprites()
    {
        foreach (GameObject answerButton in this.answerButtons)
        {
            answerButton.GetComponentInChildren<Image>().sprite = this.defaultAnswerSprite;
        }
    }

    #endregion
}

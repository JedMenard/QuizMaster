using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private int timeToAnswerQuestion = 30;

    [SerializeField]
    private int timeToShowCorrectAnswer = 10;

    /// <summary>
    /// Whether a question is being shown.
    /// </summary>
    public bool IsAnsweringQuestion { get; private set; } = false;

    /// <summary>
    /// The amount of time remaining on the timer, represented as a float from 0 to 1.
    /// </summary>
    public float FillFraction => this.timerProgress / (this.IsAnsweringQuestion ? this.timeToAnswerQuestion : this.timeToShowCorrectAnswer);

    /// <summary>
    /// Whether it is time to load the next question.
    /// </summary>
    public bool LoadNextQuestion = false;

    private float timerProgress;

    private void Update()
    {
        this.UpdateTimer();
    }

    public void CancelTimer()
    {
        this.timerProgress = 0;
    }

    private void UpdateTimer()
    {
        // Decrease the remaining time.
        this.timerProgress -= Time.deltaTime;

        // Check if we're out of time.
        if (this.timerProgress <= 0)
        {
            // Toggle whether we're showing a question or an answer.
            this.IsAnsweringQuestion = !this.IsAnsweringQuestion;

            // Reset the timer to the appropriate amount.
            this.timerProgress = this.IsAnsweringQuestion
                ? this.timeToAnswerQuestion
                : this.timeToShowCorrectAnswer;

            // If we're showing a new timer, update the flag.
            if (this.IsAnsweringQuestion)
            {
                this.LoadNextQuestion = true;
            }
        }
    }
}

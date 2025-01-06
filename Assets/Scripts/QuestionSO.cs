using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [SerializeField]
    [TextArea(2, 6)]
    private string questionText = "Enter new question text here";
    public string QuestionText { get => this.questionText; set => this.questionText = value; }

    [SerializeField]
    private string[] answers = new string[4];

    [SerializeField]
    private int correctAnswerIndex;
    public int CorrectAnswerIndex { get => this.correctAnswerIndex; set => this.correctAnswerIndex = value; }

    public string GetAnswer(int index) => this.answers[index];
}

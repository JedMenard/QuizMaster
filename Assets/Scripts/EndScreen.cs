using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI finalScoreText;

    private ScoreKeeper scoreKeeper;

    private void Awake()
    {
        this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        this.ShowFinalScore();
    }

    public void ShowFinalScore()
    {
        this.finalScoreText.text = "Congratulations!"
            + '\n' + $"You got a score of {this.scoreKeeper.Score}%";
    }
}

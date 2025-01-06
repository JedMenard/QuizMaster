using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Quiz quiz;
    private EndScreen endScreen;

    private void Awake()
    {
        this.quiz = FindObjectOfType<Quiz>();
        this.endScreen = FindObjectOfType<EndScreen>();
    }

    private void Start()
    {
        this.quiz.gameObject.SetActive(true);
        this.endScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (this.quiz.isActiveAndEnabled && this.quiz.showWinScreen)
        {
            this.quiz.gameObject.SetActive(false);
            this.endScreen.gameObject.SetActive(true);
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

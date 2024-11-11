using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject GameplayPanel;
    public GameObject GameOverPanel;

    public TMP_Text InGameScoreTxt;
    public TMP_Text EndGameScoreTxt;
    public Button StartBtn;
    public Button RetryBtn;

    private int myScore;
    public void Init()
    {
        GameManager.Instance.GameStartEvent += StartGame;
        GameManager.Instance.GameEndEvent += GameOver;
        GameManager.Instance.ScoreIncreaseEvent += UpdateScoreUI;
        StartBtn.onClick.AddListener(() => GameManager.Instance.StartGame());
        RetryBtn.onClick.AddListener(() => GameManager.Instance.RetryGame());
        ShowStartPanel();
        GameplayPanel.gameObject.SetActive(false);
    }
    private void StartGame()
    {
        StartPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        GameplayPanel.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        EndGameScoreTxt.text = "Score: " + myScore;
        GameOverPanel.SetActive(true);
        GameplayPanel.gameObject.SetActive(false);
    }
    private void ShowStartPanel()
    {
        StartPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    private void UpdateScoreUI(int score)
    {
        InGameScoreTxt.text = "Score: " + score;
        myScore = score;
    }
}

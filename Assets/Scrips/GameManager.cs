using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{

    public PlayerController PlayerController;
    public PlatformManager PlatformManager;
    public UIManager UIManager;

    public float GameSpeed = 1;

    public event CallBack GameStartEvent;
    public event CallBack GameEndEvent;
    public event CallBack<int> ScoreIncreaseEvent;

    private int score;
    public int Score { get => score;}

    private void Start()
    {
        InitGame();
    }
    private void InitGame()
    {
        PlatformManager.Init();
        PlayerController.Init();
        UIManager.Init();
    }
    public void StartGame()
    {
        score = 0;
        GameStartEvent();
    }
    public void GameOver()
    {
        GameEndEvent();
    }
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void IncrementScore()
    {
        score++;
        GameSpeed = Mathf.Max(1.0f - score * 0.01f, 0.4f);
        ScoreIncreaseEvent(Score);
    }
}

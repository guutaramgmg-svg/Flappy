using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using unityroom.Api;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ScoreText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameStartPanel;
    [SerializeField] GameObject kabePool;
    [SerializeField] Player player;
    [SerializeField] Score score;

    void Start()
    {
        gameOverPanel.SetActive(false);
        gameStartPanel.SetActive(true);
        kabePool.SetActive(false);
        ScoreText.SetActive(false);
    }

    public void OnGameStartBtn()
    {
        gameStartPanel.SetActive(false);
        Debug.Log("GameStart");
        kabePool.SetActive(true);
        ScoreText.SetActive(true);
        player.StartGame();
    }


    /// <summary>
    /// retryボタンクリック時
    /// </summary>
    public void OnRetryBtn()
    {
        Debug.Log("リトライ");
        string thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }

    /// <summary>
    /// GameOver画面を表示
    /// </summary>
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        // ボードNo1にスコア123.45fを送信する。
        UnityroomApiClient.Instance.SendScore(1, score.score, ScoreboardWriteMode.HighScoreDesc);

    }
}

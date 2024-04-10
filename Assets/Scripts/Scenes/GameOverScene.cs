using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scorePlayer;
    [SerializeField] private TextMeshProUGUI _bestScorePlayer;
    [SerializeField] private float _punchScale;
    [SerializeField] private float _timeScale;
    
    private void Awake()
    {
        int scorePlayer = PlayerPrefs.GetInt(GlobalConstants.SCORE_PlAYER, 0);
        _scorePlayer.text = scorePlayer.ToString();
        
        int bestScorePlayer = PlayerPrefs.GetInt(GlobalConstants.BEST_SCORE_PlAYER, 0);
        UpdateBestScore(scorePlayer, ref bestScorePlayer);
        
        _bestScorePlayer.text = "Best " + bestScorePlayer;
    }
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
    }

    private void UpdateBestScore(int scorePlayer, ref int bestScorePlayer)
    {
        if (scorePlayer > bestScorePlayer)
        {
            bestScorePlayer = scorePlayer;
            ShowBestScoreAnimation();
            SaveBestScore(bestScorePlayer);
        }
    }

    private void SaveBestScore(int bestScorePlayer)
    {
        PlayerPrefs.SetInt(GlobalConstants.BEST_SCORE_PlAYER, bestScorePlayer);
        PlayerPrefs.Save();
    }
    
    
    private void ShowBestScoreAnimation()
    {
        _bestScorePlayer.transform.DOPunchScale(Vector3.one * _punchScale, _timeScale, 0);
        AudioManager.Instance.PlayNewBestSound();
    }
}

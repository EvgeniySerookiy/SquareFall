using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scorePlayer;
    [SerializeField] private PlayerCollisionsHandler _playerCollisionsHandler;
    [SerializeField] private float _finalScale;
    [SerializeField] private float _timeScale;
    
    public int _score = 0;
    
    private void Awake()
    {
        _playerCollisionsHandler.AllyCollisionOccurred += AddScore;
        _scorePlayer.text = "0";
        PlayerPrefs.SetInt(GlobalConstants.SCORE_PlAYER, Convert.ToInt32(_scorePlayer.text));
    }

    private void OnDisable()
    {
        _playerCollisionsHandler.AllyCollisionOccurred -= AddScore;
    }

    private void AddScore()
    {
        _score++;
        ScoreScale();
        _scorePlayer.text = _score.ToString();
        PlayerPrefs.SetInt(GlobalConstants.SCORE_PlAYER, _score);
    }

    private void ScoreScale()
    {
        AudioManager.Instance.PlayPointSound();
        _scorePlayer.transform.DOScale(new Vector3(_finalScale, _finalScale, _finalScale), _timeScale)
            .OnComplete(() =>
            {
                _scorePlayer.text = _score.ToString();
                _scorePlayer.transform.DOScale(Vector3.one, _timeScale);
            });
    }
}

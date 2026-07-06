using Player;
using UI;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement  _playerMovement;
    [SerializeField] private RetryCanvas _retryCanvas;
    [SerializeField] private ScoreView _scoreView;
    
    private ScoreCounter _scoreCounter;
    void Start()
    {
        _playerMovement.OnPlayerDied += RestartGame;
    }

    void RestartGame()
    {
        _scoreCounter = _scoreView.GetComponentInChildren<ScoreCounter>();
        var score = _scoreCounter.Score;
        _retryCanvas.Initialize(score);
        _scoreView.StopCounterView();

    }

}

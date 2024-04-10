using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SquareSpawner _squareSpawner;
    
    private void Awake()
    {
        _playerController.Initialize();
        _playerController.PlayerDied += OnPlayerDied;
        _squareSpawner.Initialize(_playerController);
    }

    private void OnDestroy()
    {
        _playerController.PlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        _squareSpawner.StopItSpawnSquare();
        StartCoroutine(LoadGameOverWithDelay());
    }
    

    private IEnumerator LoadGameOverWithDelay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(GlobalConstants.GAMEOVER_SCENE);
    }
}

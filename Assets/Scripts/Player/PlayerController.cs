using System;
using UnityEngine;

public delegate void PlayerDeathHandler();
public class PlayerController : MonoBehaviour, ITarget
{

    public event PlayerDeathHandler PlayerDied;
    public Transform Transform => _playerMovement.transform;
    
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerCollisionsHandler _playerCollisionsHandler;
    [SerializeField] private ParticleSystem _deathParticlePlayer;

    private Vector3 position;
    private bool _isDestroyed;

    public void Initialize()
    {
        _playerInput.MoveButtonPressed += _playerMovement.RightOrLeft;
        _playerCollisionsHandler.DestroyedPlayer += DestroyPlayer;
    }

    private void OnDestroy()
    {
        _playerInput.MoveButtonPressed -= _playerMovement.RightOrLeft;
        _playerCollisionsHandler.DestroyedPlayer -= DestroyPlayer;
    }

    private void Update()
    { 
        if (!_isDestroyed)
        {
            _playerMovement.Move();
        }
    }

    private void DestroyPlayer()
    {
        _isDestroyed = true;
        AudioManager.Instance.PlayExplodeSound();
        Instantiate(_deathParticlePlayer, Transform.position, Quaternion.identity);
        PlayerDied?.Invoke();
        Destroy(Transform.gameObject);
    }
}

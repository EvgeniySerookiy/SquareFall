using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField] private SquarePool _squarePool;
    [SerializeField] private SquareColorManager squareColorManager;
    [SerializeField] private float _positionY;
    
    private float _minPositionX;
    private float _maxPositionX;
    private float _timer;
    private float _delay = 0.6f;
    private float _pauseTime = 1.1f;
    private ITarget _target;
    private bool isStopped;
    
    public void StopItSpawnSquare()
    {
        isStopped = true;
    }
    
    public void Initialize(ITarget target)
    {
        _target = target;
        var camera = Camera.main;
        _minPositionX = camera.ViewportToWorldPoint(Vector3.zero).x;
        _maxPositionX = camera.ViewportToWorldPoint(Vector3.one).x;
        _squarePool.Initialize(transform);
    }

    private void Update()
    {
        if (isStopped)
        {
            return;
        }
        
        _timer += Time.deltaTime;
        
        if (_timer >= _delay)
        {
            SpawnSquare();
        }
    }

    private void SpawnSquare()
    {
        var square = _squarePool.Take(transform);
        square.SquareDestroyed += OnSquareDestroyed;
        var randomSquareType = GetRandomSquareType();
        square.Initialize(randomSquareType, squareColorManager);
        square.transform.position = new Vector2(GetRandomPointX(), _positionY);
        square.SetTargetPosition(_target);
        _timer = 0.0f;
    }

    private void OnSquareDestroyed(Square square)
    {
        square.SquareDestroyed -= OnSquareDestroyed;
        square.SetScaleZero();
        StartCoroutine(StartTimer(square));
    }
    
    private IEnumerator StartTimer(Square square)
    {
        yield return new WaitForSeconds(_pauseTime);
        square.SetScaleOne();
        _squarePool.Release(square);
    }
    
    private SquareType GetRandomSquareType()
    {
        return (SquareType)Random.Range((int)SquareType.Ally, (int)SquareType.Enemy + 1);
    }
    
    private float GetRandomPointX()
    {
        return Random.Range(_minPositionX, _maxPositionX);
        
    }
}

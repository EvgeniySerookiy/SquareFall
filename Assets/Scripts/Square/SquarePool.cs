using System.Collections.Generic;
using UnityEngine;

public class SquarePool : MonoBehaviour
{
    [SerializeField] private Square squarePrefab;
    [SerializeField]private int _poolSize = 5;
    
    private readonly List<Square> _notElementSquarePool = new();
    private readonly List<Square> _usedElementSquarePool = new();
    
    public void Initialize(Transform transform)
    {
        for (int i = 0; i < _poolSize; i++)
        {
            AddNewElementInPool(transform);
        }
    }

    private void AddNewElementInPool(Transform transform)
    {
        var enemy = Instantiate(squarePrefab, transform);
        enemy.gameObject.SetActive(false);
        _notElementSquarePool.Add(enemy);
    }
    
    public Square Take(Transform transform)
    {
        if (_notElementSquarePool.Count == 0)
        {
            AddNewElementInPool(transform);
        }
        var lastIndex = _notElementSquarePool.Count - 1;
        var squareFromPool = _notElementSquarePool[lastIndex];
        _notElementSquarePool.RemoveAt(lastIndex);
        _usedElementSquarePool.Add(squareFromPool);
        squareFromPool.gameObject.SetActive(true);
        return squareFromPool;
    }
    
    public void Release(Square square)
    {
        square.gameObject.SetActive(false);
        _usedElementSquarePool.Remove(square);
        _notElementSquarePool.Add(square);
    }
}

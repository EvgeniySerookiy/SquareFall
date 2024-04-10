using System;
using DG.Tweening;
using UnityEngine;

public class Square : MonoBehaviour
{
    public event Action<Square> SquareDestroyed;
    public SquareType SquareType { get; private set; }

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SquareRotation _squareRotation;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _durationScale = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SquareDestroyed?.Invoke(this);
    }

    public void Initialize(SquareType squareType, SquareColorManager squareColorManager)
    {
        _squareRotation.Rotate();
        _spriteRenderer.color = squareColorManager.GetSquareByType(squareType);
        SquareType = squareType;
    }

    public void SetTargetPosition(ITarget target)
    {
        var localDownDirection = (transform.position - target.Transform.position).normalized;
        _rigidbody2D.velocity = localDownDirection * -_speed;
    }
    
    public void SetScaleZero()
    {
        transform.DOScale(Vector3.zero, _durationScale);
    }

    public void SetScaleOne()
    {
        transform.DOScale(Vector3.one, 0);
    }
}
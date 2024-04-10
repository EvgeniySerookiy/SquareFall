using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private Renderer _rendererPlayerContainer;
    
    private float _maxPointX;
    private float _minPointX;
    private bool movingRight = true;
    

    private void Awake()
    {
        AudioManager.Instance.PlayStartSound();
        var sprite = GetComponent<SpriteRenderer>();
        var offset = sprite.bounds.size.x / 2;
        _maxPointX = _rendererPlayerContainer.bounds.max.x - offset;
        _minPointX = _rendererPlayerContainer.bounds.min.x + offset;
    }

    public void Move()
    {
        float speed = movingRight ? _speed : -_speed;
        MoveIfBoundaryReached(speed);
        _rigidbody.velocity = transform.right * speed;
    }

    private void MoveIfBoundaryReached(float speed)
    {
        if ((movingRight && _maxPointX <= transform.position.x) ||
            (!movingRight && _minPointX >= transform.position.x))
        {
            AudioManager.Instance.PlayReboundSound();
            _rigidbody.velocity = transform.right * speed;
            movingRight = !movingRight;
        }
    }
    
    public void RightOrLeft()
    {
        movingRight = !movingRight;
    }
}

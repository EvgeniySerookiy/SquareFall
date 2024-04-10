using System;
using UnityEngine;

public class PlayerCollisionsHandler : MonoBehaviour
{
    public event Action DestroyedPlayer;
    public event Action AllyCollisionOccurred;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Square square))
        {
            switch (square.SquareType)
            {
                case SquareType.Enemy :
                    DestroyedPlayer?.Invoke();
                    break;
                
                case SquareType.Ally :
                    AllyCollisionOccurred?.Invoke();
                    break;
            }
        }
    }
}

using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquareRotation : MonoBehaviour
{
    [SerializeField] private float _duration;
    
    public void Rotate()
    {
        var randomAngleZ = 360 * GetRandomDirectionRotation();
        var rotationAngle = new Vector3(0, 0, randomAngleZ);
        transform.DORotate(rotationAngle,_duration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
    
    private int GetRandomDirectionRotation()
    {
        var randomNumber = Random.Range(-1, 1);
        return (randomNumber == 0) ? 1 : -1;
    }
}

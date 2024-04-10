using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action MoveButtonPressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlayMoveSound();
            MoveButtonPressed?.Invoke();
        }
    }
}

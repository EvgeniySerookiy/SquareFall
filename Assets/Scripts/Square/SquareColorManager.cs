using System;
using UnityEngine;

public class SquareColorManager : MonoBehaviour
{
    [SerializeField] private Color _red;
    [SerializeField] private Color _black;
    
    public Color GetSquareByType(SquareType squareType)
    {
        switch (squareType)
        {
            case SquareType.Ally:
                return _red;
            case SquareType.Enemy:
                return _black;
            default:
                throw new Exception("Неверный тип куба!");
        }
    }
}

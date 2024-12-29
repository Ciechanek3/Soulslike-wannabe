using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    private InputReader _inputReader;
    private Transform _playerTransform;

    private int _speed;
    private float _speedMultiplier = 0;
    public float GetSpeed => _speed * (1 + _speedMultiplier);

    public void ApplyMultiplier(float value)
    {
        _speedMultiplier += value;
    }
    public PlayerMovement(InputReader inputReader, Transform playerTransform)
    {
        _inputReader = inputReader;
        _playerTransform = playerTransform;
    }

    public void HandleMovement()
    {
        _playerTransform.Translate(_inputReader.MoveInput * Time.deltaTime * GetSpeed);
    }

}

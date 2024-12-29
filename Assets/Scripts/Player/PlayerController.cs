using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = new PlayerMovement(inputReader, transform);
    }

    private void Update()
    {
        _playerMovement.HandleMovement();
    }
}

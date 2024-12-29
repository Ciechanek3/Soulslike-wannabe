using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private StateMachine _stateMachine;

    private void Awake()
    {
        var _playerIdle = new IdleState();
        var _playerGroundedState = new PlayerRunningState(inputReader, transform);
        var _playerJumpState = new PlayerJumpState(inputReader, transform);
        var _rollingState = new RollingState();
        var _playerAttack = new AttackState();

        _stateMachine = new StateMachine(_playerIdle);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}

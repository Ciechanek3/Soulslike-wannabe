using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private StateMachine _stateMachine;

    private bool _isGrounded;

    private void Awake()
    {
        var _inputReader = new InputReader(new PlayerInput());

        var _idleState = new IdleState();
        var _moveState = new MoveState(_inputReader, transform);
        var _jumpState = new JumpState();
        //var _rollState = new RollingState(); todo
        //var _playerAttack = new AttackState();

        _stateMachine = new StateMachine(_idleState);

        _stateMachine.AddAnyTransition(_moveState, IsRunning());

        SetupTransition(_idleState, _moveState, IsRunning());

        void SetupTransition(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        Func<bool> IsRunning() => () => _inputReader.RunToggled && _inputReader.MoveVector != Vector3.zero && _isGrounded;
        Func<bool> IsWalking() => () => !_inputReader.RunToggled && _inputReader.MoveVector != Vector3.zero && _isGrounded;
        Func<bool> IsIdle() => () => _inputReader.MoveVector == Vector3.zero && _isGrounded;
        Func<bool> IsJumping() => () => !_isGrounded;
       // Func<bool> 
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}

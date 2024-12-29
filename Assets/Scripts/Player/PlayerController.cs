using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private StateMachine _stateMachine;

    private bool _isGrounded;

    private void Awake()
    {
        var _inputReader = new InputReader(new PlayerInput(), 0f);

        var _idleState = new IdleState();
        var _moveState = new MoveState(_inputReader, transform);
        var _jumpState = new JumpState();
        var _rollState = new RollingState();
        var _attackState = new AttackState();

        _stateMachine = new StateMachine(_idleState);

        _stateMachine.AddAnyTransition(_moveState, IsMoving());
        _stateMachine.AddAnyTransition(_idleState, IsIdle());

        AddTran(_moveState, _jumpState, IsJumping());
        AddTran(_moveState, _attackState, IsAttacking());
        AddTran(_moveState, _rollState, IsRolling());

        AddTran(_idleState, _jumpState, IsJumping());
        AddTran(_idleState, _attackState, IsAttacking());
        AddTran(_idleState, _rollState, IsRolling());

        AddTran(_jumpState, _attackState, IsAttacking());

        void AddTran(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        Func<bool> IsMoving() => () => _inputReader.MoveVector != Vector3.zero && _isGrounded;
        Func<bool> IsIdle() => () => _inputReader.MoveVector == Vector3.zero && _isGrounded;
        Func<bool> IsJumping() => () => !_isGrounded;
        Func<bool> IsAttacking() => () => false;
        Func<bool> IsRolling() => () => _inputReader.IsRolling;
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}

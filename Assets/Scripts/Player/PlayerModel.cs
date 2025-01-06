using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    private PlayerStats _playerData;
    private int _healthPoints;
    private float _movementSpeed;
    private float _jumpSpeed;
    private float _armor;
    private float _stamina;

    private StateMachine _stateMachine;
    private GroundCheck _groundCheck;

    private bool IsGrounded => _groundCheck.IsGrounded();

    private bool _isRolling;

    public PlayerModel(Rigidbody rb, Vector3EventChannel onMoveEventChannel, EventChannelSO onJumpEventChannel, GroundCheck groundCheck, float ms, float js)
    {
        _groundCheck = groundCheck;
        _movementSpeed = ms;
        _jumpSpeed = js;

        InitStateMachine(rb, onMoveEventChannel, onJumpEventChannel);
    }

    public void InitStateMachine(Rigidbody rb, Vector3EventChannel onMoveEventChannel, EventChannelSO onJumpEventChannel)
    {
        var _inputReader = new InputReader(onMoveEventChannel, onJumpEventChannel, 0f);

        var _idleState = new IdleState(rb, onJumpEventChannel, _jumpSpeed);
        var _moveState = new MoveState(rb, onMoveEventChannel, onJumpEventChannel, _movementSpeed, _jumpSpeed);
        var _jumpState = new JumpState(rb);
        var _rollState = new RollingState();
        var _attackState = new AttackState();

        _stateMachine = new StateMachine();

        _stateMachine.AddAnyTransition(_moveState, IsMoving());
        _stateMachine.AddAnyTransition(_idleState, IsIdle());

        AddTran(_moveState, _jumpState, IsJumping());
        AddTran(_moveState, _attackState, IsAttacking());
        AddTran(_moveState, _rollState, IsRolling());

        AddTran(_idleState, _jumpState, IsJumping());
        AddTran(_idleState, _attackState, IsAttacking());
        AddTran(_idleState, _rollState, IsRolling());

        AddTran(_jumpState, _attackState, IsAttacking());

        _stateMachine.SetState(_idleState);

        void AddTran(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> IsMoving() => () => _inputReader.MoveVector != Vector3.zero && IsGrounded;
        Func<bool> IsIdle() => () => _inputReader.MoveVector == Vector3.zero && IsGrounded;
        Func<bool> IsJumping() => () => !IsGrounded;
        Func<bool> IsAttacking() => () => false;
        Func<bool> IsRolling() => () => _isRolling;
    }

    public void FixedUpdate()
    {
        _stateMachine.Tick();
    }

    public void SetRolling(bool value)
    {
        _isRolling = value;
    }

    public bool CanStartRolling()
    {
        if ((_stateMachine.CurrentState.GetType() == typeof(MoveState) ||
            _stateMachine.CurrentState.GetType() == typeof(IdleState)) && _isRolling == false)
        {
            return true;
        }
        return false;
    }
}

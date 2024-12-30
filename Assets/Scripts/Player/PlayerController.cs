using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GroundCheck groundCheck;

    private InputReader inputReader;

    private StateMachine _stateMachine;
    private float _movementSpeed;

    private bool IsGrounded => groundCheck != null ? groundCheck.IsGrounded() : true; //if we're not using grounding it is true by default for simpler logic

    private void Awake()
    {
        var _inputReader = new InputReader(new PlayerInput(), 0f);

        var _idleState = new IdleState();
        var _moveState = new MoveState(transform, _movementSpeed);
        var _jumpState = new JumpState(transform);
        var _rollState = new RollingState();
        var _attackState = new AttackState();

        inputReader = _inputReader;

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

        Func<bool> IsMoving() => () => _inputReader.MoveVector != Vector3.zero && IsGrounded;
        Func<bool> IsIdle() => () => _inputReader.MoveVector == Vector3.zero && IsGrounded;
        Func<bool> IsJumping() => () => !IsGrounded;
        Func<bool> IsAttacking() => () => false;
        Func<bool> IsRolling() => () => _inputReader.IsRolling;
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}

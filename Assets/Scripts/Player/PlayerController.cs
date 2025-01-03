using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Vector3EventChannel onMoveEventChannel;
    [SerializeField] private EventChannelSO onJumpEventChannel;

    [Header("Parameters")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpSpeed = 1f;

    private StateMachine _stateMachine;

    private bool IsGrounded => groundCheck != null ? groundCheck.IsGrounded() : true; //if we're not using grounding it is true by default for simpler logic

    //on jump pressed even that changes bool to true, on reading this bool it changes it back to false// (na jutro)

    private void Awake()
    {
        var _inputReader = new InputReader(onMoveEventChannel, onJumpEventChannel, 0f);

        var _idleState = new IdleState(rb, onJumpEventChannel, jumpSpeed);
        var _moveState = new MoveState(rb, onMoveEventChannel, onJumpEventChannel, movementSpeed, jumpSpeed);
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
        Func<bool> IsRolling() => () => _inputReader.IsRolling;
    }

    private void FixedUpdate()
    {
        Debug.LogError(!IsGrounded);
        _stateMachine.Tick();
    }
}

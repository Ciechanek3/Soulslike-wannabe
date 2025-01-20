using System;
using UnityEngine;

public class PlayerModel
{
    private PlayerStats _playerData;
    private int _healthPoints;
    private float _movementSpeed;
    private float _jumpSpeed;
    private float _rollingSpeed;
    private float _armor;
    private float _stamina;

    private StateMachine _stateMachine;
    private GroundCheck _groundCheck;

    private Vector3 _moveVector;

    private Vector3 _velocity;
    private Quaternion _rotation;

    public Vector3 Velocity => _velocity;
    public Quaternion Rotation => _rotation;
    public Vector3 Position { get; private set; }

    private bool IsGrounded => _groundCheck.IsGrounded();

    public bool IsRolling;

    public Transform LockTarget;
    private Vector3 _lookVector;
    private bool _isLocked => LockTarget != null;
    

    public PlayerModel(Transform cameraTransform, Vector3EventChannel onMoveEventChannel, EventChannelSO onJumpEventChannel, GroundCheck groundCheck, float movementSpeed, float jumpSpeed, float rollingSpeed)
    {
        _groundCheck = groundCheck;
        _movementSpeed = movementSpeed;
        _jumpSpeed = jumpSpeed;
        _rollingSpeed = rollingSpeed;

        _rotation = Quaternion.identity;

        InitStateMachine(cameraTransform, onMoveEventChannel, onJumpEventChannel);
        onMoveEventChannel.RegisterObserver(UpdateMovement);
    }

    public void InitStateMachine(Transform cameraTransform, Vector3EventChannel onMoveEventChannel, EventChannelSO onJumpEventChannel)
    {
        var _idleState = new IdleState(onJumpEventChannel, _jumpSpeed);
        var _moveState = new MoveState(cameraTransform, onMoveEventChannel, onJumpEventChannel, _movementSpeed, _jumpSpeed);
        var _jumpState = new JumpState();
        var _rollState = new RollingState(_rollingSpeed);
        var _attackState = new AttackState();

        _stateMachine = new StateMachine();

        _stateMachine.AddAnyTransition(_moveState, IsMoving());
        _stateMachine.AddAnyTransition(_idleState, IsIdle());

        AddTran(_moveState, _jumpState, IsJumping());
        AddTran(_moveState, _attackState, IsAttacking());
        AddTran(_moveState, _rollState, IsCurrentlyRolling());

        AddTran(_idleState, _jumpState, IsJumping());
        AddTran(_idleState, _attackState, IsAttacking());
        AddTran(_idleState, _rollState, IsCurrentlyRolling());

        AddTran(_jumpState, _attackState, IsAttacking());

        _stateMachine.SetState(_idleState);

        void AddTran(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> IsMoving() => () => _moveVector != Vector3.zero && IsGrounded && !IsRolling;
        Func<bool> IsIdle() => () => _moveVector == Vector3.zero && IsGrounded;
        Func<bool> IsJumping() => () => !IsGrounded;
        Func<bool> IsAttacking() => () => false;
        Func<bool> IsCurrentlyRolling() => () => IsRolling;
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
    }

    public void FixedUpdate()
    {
        _stateMachine.Tick();
        if(_stateMachine.CurrentState is IMovementModel)
        {
            var state = _stateMachine.CurrentState as IMovementModel;
            state.GetVelocityAndRotation(ref _velocity, ref _rotation);
        }
        if(_isLocked)
        {
            _lookVector = LockTarget.position - Position;
            _lookVector.y = 0;
            _rotation = Quaternion.LookRotation(_lookVector);
        }
    }

    public void SetRolling(bool value)
    {
        IsRolling = value;
    }

    public bool CanStartRolling()
    {
        if ((_stateMachine.CurrentState.GetType() == typeof(MoveState) ||
            _stateMachine.CurrentState.GetType() == typeof(IdleState)) && IsRolling == false)
        {
            return true;
        }
        return false;
    }

    private void UpdateMovement(Vector3 moveVector)
    {
        _moveVector = moveVector;
    }
}

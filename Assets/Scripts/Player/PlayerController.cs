using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraTransform;

    [Header("Controller")]
    [SerializeField] private CameraController cameraController;

    [Header("Move events")]
    [SerializeField] private Vector3EventChannel onMoveEventChannel;
    [SerializeField] private EventChannelSO onJumpEventChannel;
    [SerializeField] private EventChannelSO onRollEventChannel;
    [SerializeField] private EventChannelSO onAttackEventChannel;
    [SerializeField] private EventChannelSO onLockEventChannel;

    [Header("Parameters")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float rollingSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;

    private PlayerModel _playerModel;
    private PlayerView _playerView;

    private Transform _lookTarget;

    private void Awake()
    {
        _playerModel = new PlayerModel(cameraTransform, onMoveEventChannel, onJumpEventChannel, groundCheck, movementSpeed, jumpSpeed, rollingSpeed);
        _playerView = new PlayerView(rb, animator);
    }

    private void OnEnable()
    {
        onRollEventChannel.RegisterObserver(EnableRolling);
        cameraController.OnCameraChanged += SetLookTarget;
    }

    private void OnDisable()
    {
        onRollEventChannel.UnregisterObserver(EnableRolling);
        cameraController.OnCameraChanged -= SetLookTarget;
    }



    private void FixedUpdate()
    {
        _playerModel.SetPosition(rb.position);
        _playerModel.FixedUpdate();
        _playerView.UpdateRotation(_playerModel.Rotation);
        _playerView.UpdateVelocity(_playerModel.Velocity);
    }

    private void EnableRolling()
    {
        if (_playerModel.CanStartRolling())
        {
            _playerModel.SetRolling(true);
            _playerView.StartRolling();
        }
    }

    public void DisableRolling()
    {
        _playerModel.IsRolling = false;
        animator.ResetTrigger("Roll");
    }

    private void SetLookTarget()
    {
        if (_playerModel.LockTarget == null && cameraController.CurrentTraget.LockTransform != null)
        {
            _playerModel.LockTarget = cameraController.CurrentTraget.LockTransform;
        }
        else
        {
            _playerModel.LockTarget = null;
        }
    }
}

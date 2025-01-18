using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraTransform;

    [Header("Move events")]
    [SerializeField] private Vector3EventChannel onMoveEventChannel;
    [SerializeField] private EventChannelSO onJumpEventChannel;
    [SerializeField] private EventChannelSO onRollEventChannel;
    [SerializeField] private EventChannelSO onAttackEventChannel;

    [Header("Parameters")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float rollingSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;

    private PlayerModel _playerModel;
    private PlayerView _playerView;

    private void Awake()
    {
        _playerModel = new PlayerModel(rb, transform, cameraTransform, onMoveEventChannel, onJumpEventChannel, groundCheck, movementSpeed, jumpSpeed, rotationSpeed, rollingSpeed);
        _playerView = new PlayerView(animator);
    }

    private void OnEnable()
    {
        onRollEventChannel.RegisterObserver(EnableRolling);
    }

    private void OnDisable()
    {
        onRollEventChannel.UnregisterObserver(EnableRolling);
    }

    private void FixedUpdate()
    {
        _playerModel.FixedUpdate();
    }

    private void EnableRolling()
    {
        if(_playerModel.CanStartRolling())
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
}

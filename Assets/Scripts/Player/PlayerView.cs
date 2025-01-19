using UnityEngine;

public class PlayerView
{
    private Rigidbody _rigidbody;
    private Animator _animator;

    private int hashRollAnim = Animator.StringToHash("Roll");

    public PlayerView(Rigidbody rigidbody, Animator animator)
    {
        _rigidbody = rigidbody;
        _animator = animator;
    }

    public void StartRolling()
    {
        _animator.SetTrigger(hashRollAnim);
    }

    public void UpdateVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    public void UpdateRotation(Quaternion rotation)
    {
        _rigidbody.rotation = rotation;
    }
}

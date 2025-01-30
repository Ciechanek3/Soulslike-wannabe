using UnityEngine;

public class PlayerView
{
    private Rigidbody _rigidbody;
    private PlayerAnimatorController _animatorController;

    

    public PlayerView(Rigidbody rigidbody, Animator animator)
    {
        _rigidbody = rigidbody;
        _animatorController = new PlayerAnimatorController(animator);
    }

    public void StartRolling()
    {
        
    }

    public void UpdateVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    public void UpdateRotation(Quaternion rotation)
    {
        _rigidbody.rotation = rotation;
    }

    public void UpdateAnimation(bool isGrounded)
    {
        _animatorController.UpdateAnimator(_rigidbody.velocity, isGrounded);
    }
}

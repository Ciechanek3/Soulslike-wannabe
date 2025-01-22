using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController
{
    public readonly int InputHorizontal = Animator.StringToHash("InputHorizontal");
    public readonly int InputVertical = Animator.StringToHash("InputVertical");
    public readonly int InputMagnitude = Animator.StringToHash("InputMagnitude");
    public readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    public readonly int IsStrafing = Animator.StringToHash("IsStrafing");
    public readonly int IsSprinting = Animator.StringToHash("IsSprinting");
    public readonly int GroundDistance = Animator.StringToHash("GroundDistance");

    private Animator _animator;

    public AnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public void UpdateAnimator(Vector3 moveInput, bool isGrounded)
    {
        _animator.SetFloat(InputHorizontal, moveInput.x);
        _animator.SetFloat(InputVertical, moveInput.z);

        _animator.SetBool(IsGrounded, isGrounded);
    }
}

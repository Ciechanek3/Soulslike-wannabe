using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController
{
    protected Animator _animator;
    public AnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public bool IsAnimationFinished => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
}

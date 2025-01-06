using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView
{
    private Animator _animator;

    public PlayerView(Animator animator)
    {
        _animator = animator;
    }

    private void PlayAnimation()
    {

    }

    public void StartRolling()
    {
        _animator.SetTrigger("Roll");
    }
}

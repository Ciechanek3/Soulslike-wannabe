using UnityEngine;

public class PlayerView
{
    private Animator _animator;
    private int hashRollAnim = Animator.StringToHash("Roll");

    public PlayerView(Animator animator)
    {
        _animator = animator;
    }

    public void StartRolling()
    {
        _animator.SetTrigger(hashRollAnim);
    }
}

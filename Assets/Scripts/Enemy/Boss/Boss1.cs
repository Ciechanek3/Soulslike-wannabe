using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    [Inject("Player")]
    private Transform _player;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float range;
    [SerializeField] private Collider weaponCollider;

    protected override void Awake()
    {
        base.Awake();

        Debug.LogError(_player.gameObject.name);

        var chasePlayer = new Leaf(new ChaseStrategy(_player, _navMeshAgent, movementSpeed, range));
        var attackPlayer = new Leaf(new AttackStrategy(weaponCollider, _animatorController));
        var attackSequence = new Sequence();
        attackSequence.AddChild(chasePlayer);
        attackSequence.AddChild(attackPlayer);

        _behaviourTree.AddChild(attackSequence);
    }

}

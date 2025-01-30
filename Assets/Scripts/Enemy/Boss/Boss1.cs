using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    [Inject("Player")]
    private Transform _player;

    protected override void Awake()
    {
        base.Awake();

        Debug.LogError(_player.gameObject.name);

        /*_behaviourTree.AddChild();

        var ChasePlayer = new Leaf(new ChaseStrategy(navMeshAgent));
        var AttackSequence = new Sequence();*/
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    private IStrategy _strategy;

    public Leaf(IStrategy strategy)
    {
        _strategy = strategy;
    }

    public override Status Process() => _strategy.Process();

    public override void Reset() => _strategy.Reset();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public IStrategy strategy;

    public override Status Process() => strategy.Process();
}

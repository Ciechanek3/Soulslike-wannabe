using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence(int selValue = 0) : base(selValue)
    {
    }

    public override Status Process()
    {
        if (children[currentChild].Process() != Status.Success)
        {
            return children[currentChild].Process();
        }
        else
        {
            currentChild++;
            return currentChild == children.Count ? Status.Success : Status.Running;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BehaviourTree : Node
{
    public override Status Process()
    {
        while (currentChild < children.Count)
        {
            var status = children[currentChild].Process();
            if (status != Status.Success)
            {
                return status;
            }
            currentChild++;
        }
        return Status.Success;
    }
}

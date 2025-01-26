using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public override Status Process()
    {
        if (currentChild < children.Count)
        {
            switch (children[currentChild].Process())
            {
                case Status.Running:
                    {
                        return Status.Running;
                    }
                case Status.Success:
                    {
                        Reset();
                        return Status.Success;
                    }
                case Status.Failure:
                    {
                        currentChild++;
                        return Status.Running;
                    }
            }
        }

        Reset();
        return Status.Failure;
    }
}

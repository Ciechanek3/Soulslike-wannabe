using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrioritySelector : Node
{
    private List<Node> sortedChildren;
    private List<Node> SortedChildren => sortedChildren ??= SortChildren();
    protected virtual List<Node> SortChildren() => children.OrderByDescending(child => child.SelectValue).ToList();
    public override Status Process()
    {
        if (currentChild < SortedChildren.Count)
        {
            switch (SortedChildren[currentChild].Process())
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

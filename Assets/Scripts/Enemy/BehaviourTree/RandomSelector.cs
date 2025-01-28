using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelector : Node
{
    protected List<Node> RandomizedList = new();

    public RandomSelector(int selValue = 0) : base(selValue)
    {
        RandomizeList();
    }

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

    private void RandomizeList()
    {
        int totalChance = 0;
        for (int i = 0; i < children.Count; i++)
        {
            totalChance += children[i].SelectValue;
        }

        var randomValue = Random.Range(0, totalChance + 1);
        var checkThreshold = 0;

        while (children.Count > 0)
        {
            for (int i = 0; i < children.Count; i++)
            {
                checkThreshold += children[i].SelectValue;
                if (randomValue < checkThreshold)
                {
                    checkThreshold -= children[i].SelectValue;
                    RandomizedList.Add(children[i]);
                    RandomizedList.Remove(children[i]);
                }

            }
        }
    }
}

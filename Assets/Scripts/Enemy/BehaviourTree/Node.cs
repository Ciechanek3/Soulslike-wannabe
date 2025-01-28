using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public enum Status
    {
        Success,
        Failure,
        Running
    }

    protected List<Node> children = new();
    protected int currentChild;
    protected int selectValue; //for random or priority

    public int SelectValue => selectValue;

    public Node(int selValue = 0)
    {
        selectValue = selValue;
    }

    public virtual Status Process() => children[currentChild].Process();

    public void AddChild(Node child) => children.Add(child);

    public virtual void Reset()
    {
        currentChild = 0;
        foreach (var child in children)
        {
            child.Reset();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    
    public int a = 1;
    public enum Status
    {
        Success,
        Failure,
        Running
    }

    [SerializeField] protected List<Node> children;
    protected int currentChild;

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

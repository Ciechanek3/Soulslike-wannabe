using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class InjectAttribute : Attribute
{
    public string Tag { get; }

    public InjectAttribute(string tag = null)
    {
        Tag = tag;
    }
}

using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
public class RegisterAttribute : Attribute
{
    public string Tag { get; }

    public RegisterAttribute(string tag = null)
    {
        Tag = tag;
    }
}

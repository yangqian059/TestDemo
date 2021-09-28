using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field,Inherited = true,AllowMultiple = false)]
public class ShowWithBoolAttribute : PropertyAttribute
{
    public string boolName;
    public bool showWhenEqual;

    public ShowWithBoolAttribute(string boolName, bool showWhenEqual)
    {
        this.boolName = boolName;
        this.showWhenEqual = showWhenEqual;
    }
}

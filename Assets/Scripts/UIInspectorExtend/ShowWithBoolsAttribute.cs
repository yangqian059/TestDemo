using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ShowWithBoolsAttribute : PropertyAttribute
{
    public bool showWhenEqual;
    public string[] boolNames;

    public ShowWithBoolsAttribute(bool showWhenEqual,params string[] boolNames)
    {
        this.showWhenEqual = showWhenEqual;
        this.boolNames = boolNames;
    }
}

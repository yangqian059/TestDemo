using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ShowWithEnumAttribute : PropertyAttribute
{
    public string enumName;
    public bool showWhenEqual;
    public int[] enumIndexs;

    public ShowWithEnumAttribute(string enumName, bool showWhenEqual,params int[] enumIndexs)
    {
        this.enumName = enumName;
        this.showWhenEqual = showWhenEqual;
        this.enumIndexs = enumIndexs;
    }
}

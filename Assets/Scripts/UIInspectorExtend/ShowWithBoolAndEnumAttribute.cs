using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ShowWithBoolAndEnumAttribute : PropertyAttribute
{
    public string boolName;
    public int[] enumIndexs;
    public string enumName;
    public bool showWhenEqualEnum;
    public bool showWhenEqualBool;

    public ShowWithBoolAndEnumAttribute(string boolName, bool showWhenEqualBool, string enumName, bool showWhenEqualEnum, params int[] enumIndexs)
    {
        this.boolName = boolName;
        this.showWhenEqualBool = showWhenEqualBool;
        this.enumName = enumName;
        this.showWhenEqualEnum = showWhenEqualEnum;
        this.enumIndexs = enumIndexs;
    }
}

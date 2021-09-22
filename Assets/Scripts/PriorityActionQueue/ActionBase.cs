using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase
{
    protected Action doAction;
    protected Func<bool> checkFinishAction;
    protected Action doFinish;

    public bool isBegan = false;

    public virtual ActionBase Init(Action doAction, Func<bool> checkFinishAction, Action doFinish = null)
    {
        this.doAction = doAction;
        this.checkFinishAction = checkFinishAction;
        this.doFinish = doFinish;
        isBegan = false;
        return this;
    }

    public virtual bool CheckFinish()
    {
        if (checkFinishAction == null || checkFinishAction())
        {
            doFinish?.Invoke();
            return true;
        }
        else
            return false;
    }

    public virtual void DoAction()
    {
        doAction?.Invoke();
        isBegan = true;
    }

    public ActionBase Clone()
    {
        ActionBase ab = new ActionBase();
        ab.Init(this.doAction, this.checkFinishAction, this.doFinish);
        return ab;
    }
}

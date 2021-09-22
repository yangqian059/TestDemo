using System;

public class ThrowawayAction : ActionBase
{
    public override ActionBase Init(Action doAction, Func<bool> checkFinishAction, Action doFinish = null)
    {
        base.Init(doAction, checkFinishAction, doFinish);
        return this;
    }

    public override void DoAction()
    {
        if (isBegan)
            return;
        base.DoAction();
    }
}

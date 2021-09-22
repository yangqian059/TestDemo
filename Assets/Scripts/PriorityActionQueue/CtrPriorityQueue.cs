using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrPriorityQueue<T, V> : SingleMono<V> where V : CtrPriorityQueue<T, V> where T : Enum
{
    private Dictionary<T, ActionBase> pri_aciton_dic;
    private List<T> enums;
    private bool start = false;
    private int curIndex;
    private ActionBase curAction;
    public void Init()
    {
        pri_aciton_dic = new Dictionary<T, ActionBase>();
        Array enumArr = Enum.GetValues(typeof(T));
        enums = new List<T>(enumArr.Length);
        foreach (T myCode in enumArr)
            enums.Add(myCode);
    }

    private void Update()
    {
        DoUpdate();
    }

    protected virtual void DoUpdate()
    {
        if (!start)
            return;
        if (!curAction.isBegan|| !curAction.CheckFinish())
        {
            curAction.DoAction();
        }
        if (curAction.CheckFinish())
        {
            MoveToNextAction();
            DoUpdate();
        }
    }


    public virtual void StartQueue()
    {
        start = true;
        curIndex = 0;
        SetCurAction();
        if (curAction == null)
            MoveToNextAction();
    }

    public void AddAction(T pri, ActionBase ab)
    {
        if (pri_aciton_dic.ContainsKey(pri))
            pri_aciton_dic[pri] = ab;
        else
            pri_aciton_dic.Add(pri, ab);
    }

    private void MoveToNextAction()
    {
        curIndex++;
        if (curIndex == enums.Count)
        {
            start = false;
            return;
        }
        SetCurAction();
        if (curAction == null)
            MoveToNextAction();
    }

    private void SetCurAction()
    {
        if (pri_aciton_dic.ContainsKey(enums[curIndex]))
            curAction = pri_aciton_dic[enums[curIndex]].Clone();
        else
            curAction = null;
    }
}



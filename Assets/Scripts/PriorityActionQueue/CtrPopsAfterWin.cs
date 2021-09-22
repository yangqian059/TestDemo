using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrPopsAfterWin : CtrPriorityQueue<CtrPopsAfterWin.EnumPopSeq, CtrPopsAfterWin>
{
    public bool CanGoNext_PopsAfterWin { get; set; }
    public override void StartQueue()
    {
        base.StartQueue();
        CanGoNext_PopsAfterWin = false;
    }

    public enum EnumPopSeq
    {
        PopD,
        PopB,
        PopA,
        PopC
    }
}




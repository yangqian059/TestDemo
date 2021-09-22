using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrPopsEnterGarden : CtrPriorityQueue<CtrPopsEnterGarden.EnumPopSeq, CtrPopsEnterGarden>
{
    public bool CanGoNext_PopsEnterGarden { get; set; }
    public override void StartQueue()
    {
        base.StartQueue();
        CanGoNext_PopsEnterGarden = false;
    }

    public enum EnumPopSeq
    {
        PopB,
        PopC,
        PopD,
        PopA,
    }
}




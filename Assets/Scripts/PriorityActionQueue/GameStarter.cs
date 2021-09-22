using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject PageA;
    public GameObject PageB;
    public GameObject PageC;
    public GameObject PageD;
    private void Start()
    {
        CtrPopsAfterWin.instance.Init();
        CtrPopsEnterGarden.instance.Init();

        PageA.GetComponent<CtrPage>().Init("A", CtrPopsAfterWin.EnumPopSeq.PopA, CtrPopsEnterGarden.EnumPopSeq.PopA);
        PageB.GetComponent<CtrPage>().Init("B", CtrPopsAfterWin.EnumPopSeq.PopB, CtrPopsEnterGarden.EnumPopSeq.PopB);
        PageC.GetComponent<CtrPage>().Init("C", CtrPopsAfterWin.EnumPopSeq.PopC, CtrPopsEnterGarden.EnumPopSeq.PopC);
        PageD.GetComponent<CtrPage>().Init("D", CtrPopsAfterWin.EnumPopSeq.PopD, CtrPopsEnterGarden.EnumPopSeq.PopD);
    }
}

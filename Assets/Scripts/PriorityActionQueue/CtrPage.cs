using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CtrPage : MonoBehaviour
{
    private Text text;
    private Button btn;
    public void Init(string content, CtrPopsAfterWin.EnumPopSeq afterWinPop, CtrPopsEnterGarden.EnumPopSeq enterGardenPop)
    {
        text = transform.Find("Text").GetComponent<Text>();
        btn = transform.Find("closeBtn").GetComponent<Button>();
        text.text = content;

        btn.onClick.AddListener(OnClickCloseBtn);
        CtrPopsAfterWin.instance.AddAction(afterWinPop, GetAfterWinAction());
        CtrPopsEnterGarden.instance.AddAction(enterGardenPop, GetEnterGardenAction());
    }

    private void OnClickCloseBtn()
    {
        CtrPopsAfterWin.instance.CanGoNext_PopsAfterWin = true;
        CtrPopsEnterGarden.instance.CanGoNext_PopsEnterGarden = true;
        Close();
    }

    private ActionBase GetAfterWinAction()
    {
        ThrowawayAction ta = new ThrowawayAction();
        ta.Init(() =>
        {
            CtrPopsAfterWin.instance.CanGoNext_PopsAfterWin = false;
            Open();
        },
        () => CtrPopsAfterWin.instance.CanGoNext_PopsAfterWin);
        return ta;
    }

    private ActionBase GetEnterGardenAction()
    {
        ThrowawayAction ta = new ThrowawayAction();
        ta.Init(() =>
        {
            CtrPopsEnterGarden.instance.CanGoNext_PopsEnterGarden = false;
            Open();
        },
        () => CtrPopsEnterGarden.instance.CanGoNext_PopsEnterGarden);
        return ta;
    }

    float startScale = 0.5f;
    float openTime = 0.5f;
    private void Open()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * startScale;
        transform.DOScale(Vector3.one, openTime);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}

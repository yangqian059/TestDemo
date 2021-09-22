using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrAfterwinBtn : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("CtrPopsAfterWin");
            CtrPopsAfterWin.instance.StartQueue();
        });
    }
}

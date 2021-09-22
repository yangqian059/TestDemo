using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrEnterGardenBtn : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("CtrPopsEnterGarden");
            CtrPopsEnterGarden.instance.StartQueue();
        });
    }
}

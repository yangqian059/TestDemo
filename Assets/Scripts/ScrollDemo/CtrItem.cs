using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrItem : MonoBehaviour
{
    public int row = 0;

    bool inited = false;
    Text[] childTexts;
    GridLayoutGroup layoutGroup;

    public void Init(float height)
    {
        childTexts = transform.GetComponentsInChildren<Text>();
        layoutGroup = transform.GetComponent<GridLayoutGroup>();

        layoutGroup.cellSize = new Vector2(layoutGroup.cellSize.x, height);

        inited = true;
    }

    public void SetRow(int row)
    {
        this.row = row;

        for (int i = 0; i < childTexts.Length; i++)
        {
            childTexts[i].text = "第" + row + "行";
        }
        

    }
}

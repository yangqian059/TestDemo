using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{

    public Transform Top_;
    public Transform Bottom_;


    private ScrollRect scrollRect;
    private GameObject ItemGroup;
    private List<CtrItem> itemList;


    private RectTransform ViewPort;
    private RectTransform Content;


    

    private int contentsNum = 600;
    private float contentHeight = 100;
    private float spacingY = 20;
    private const int addtioalNum = 4;
    private int fixedcontentsNum;
    private float contentSpacing;
    private int top;
    private int bottom;
    private float lastY;


    private void Start()
    {
        
        ItemGroup = Resources.Load<GameObject>("ScrollDemo/itemGroup");
        scrollRect = gameObject.GetComponent<ScrollRect>();
        Content = scrollRect.content;
        ViewPort = scrollRect.viewport;
        itemList = new List<CtrItem>();


        //scrollRect.onValueChanged.AddListener(OnPosChanged);
        contentSpacing = contentHeight + spacingY;
        fixedcontentsNum = (int)(ViewPort.rect.height / contentHeight) + addtioalNum;
        top = 0;
        bottom = fixedcontentsNum - 1;
        Content.sizeDelta = new Vector2(Content.sizeDelta.x, (contentsNum) * contentSpacing);
        lastY = Content.position.y;

        Debug.LogError(Content.sizeDelta);
        Debug.LogError(Content.rect);



        glg = InitContentLayoutGroup(Content.rect.width, contentHeight, 0, spacingY);

        for (int i = 0; i < fixedcontentsNum; i++)
        {
            GameObject go = Instantiate<GameObject>(ItemGroup, Content);
            CtrItem ctr = go.AddComponent<CtrItem>();
            ctr.Init(contentHeight);
            ctr.SetRow(i);
            itemList.Add(ctr);
        }

        Top_.position = new Vector3(Top_.position.x, ViewPort.position.y + addtioalNum / 2 * contentSpacing, Top_.position.z);
        Bottom_.position = new Vector3(Bottom_.position.x, ViewPort.position.y - ViewPort.rect.height - addtioalNum / 2 * contentSpacing, Bottom_.position.z);

        StartCoroutine(DestroyWait(glg));
        
    }

    GridLayoutGroup glg;
    bool inited = false;





    IEnumerator DestroyWait( Component comp)
    {
        yield return null;
        Destroy(comp);
        inited = true;
    }

    private GridLayoutGroup InitContentLayoutGroup(float x, float y, int spacingX, float spacingY)
    {
        GridLayoutGroup glg = Content.GetComponent<GridLayoutGroup>();
        if(!glg) glg = Content.gameObject.AddComponent<GridLayoutGroup>();
        glg.cellSize = new Vector2(x, y);
        glg.spacing = new Vector2(spacingX, spacingY);
        glg.startCorner = GridLayoutGroup.Corner.UpperLeft;
        glg.startAxis = GridLayoutGroup.Axis.Vertical;
        glg.childAlignment = TextAnchor.UpperCenter;

        
        
        return glg;
    }

    float offsetY;

    private void Update()
    {
        if (!inited) return;

        offsetY = Content.position.y - lastY;
        lastY = Content.position.y;

        if (offsetY > 0)      // 上滑
        {
            while (itemList[top].transform.position.y > Top_.position.y)
            {
                if (itemList[bottom].row == contentsNum - 1) return;
                itemList[top].transform.position = itemList[bottom].transform.position - new Vector3(0, contentSpacing, 0);
                itemList[top].SetRow(itemList[bottom].row + 1);
                bottom = top;
                top = (top + 1) % fixedcontentsNum;
            }
        }
        else                  // 下滑
        {
            while (itemList[bottom].transform.position.y < Bottom_.position.y)
            {
                if (itemList[top].row == 0) return;
                itemList[bottom].transform.position = itemList[top].transform.position + new Vector3(0, contentSpacing, 0);
                itemList[bottom].SetRow(itemList[top].row - 1);
                top = bottom;
                bottom = (bottom + fixedcontentsNum -1) % fixedcontentsNum;
            }
        }
    }
}

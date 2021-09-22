using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBlocks : MonoBehaviour
{
    private  int count = 1;
    private GameObject Block;
    public GridLayoutGroup gridLayout;

    public RectOffset padding;
    public  Vector2 cellSize = new Vector2(100, 100);
    public Vector2 spacing = new Vector2(20, 20);

    public GridLayoutGroup.Corner startCorner = GridLayoutGroup.Corner.UpperLeft;
    public GridLayoutGroup.Axis startAxis = GridLayoutGroup.Axis.Vertical;
    public TextAnchor childAlignment = TextAnchor.UpperLeft;


    void Start()
    {
        Block = Resources.Load<GameObject>("GridLayoutDemo/Block");
        Debug.Log(gameObject.GetComponent<RectTransform>().position);
        list = new List<GameObject>();
    }

    IEnumerator Create()
    {
        for (int i = 0; i < count; i++)
        {

            yield return null;
            Instantiate(Block, transform);
        }
    }


    List<GameObject> list = new List<GameObject>();
    void CreateBy()
    {

        for (int i = 0; i < 17; i++)
        {
            list.Add(Instantiate(Block));
        }

    }

    void DestroyAll()
    {
        for (int i = 0; i < list.Count; i++) Destroy(list[i]);
        list.Clear();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Create());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateBy();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            gridLayout.cellSize = cellSize;
            gridLayout.spacing = spacing;
            gridLayout.padding = padding;
            gridLayout.startCorner = startCorner;
            gridLayout.startAxis = startAxis;
            gridLayout.childAlignment = childAlignment;
            GetComponent<RectTransform>().InitLayout(list, cellSize, spacing, padding, startCorner, startAxis, childAlignment);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyAll();
        }
    }
}

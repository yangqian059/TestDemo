using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class CarouselFigure : ScrollRect
{
    private const float MOVE_TIME = 0.2f;
    private const float CRITICAL_VELOCITY = 500.0f;
    private const float CRITICAL_RATE = 0.4f;

    [SerializeField]
    private Transform toggleGroup;
    [SerializeField]
    private int contentCount;
    private float contentLen;
    private GridLayoutGroup contentGrid;
    private float viewPortLeft;
    private float viewPortRight;
    private float totalSpacingX;
    private float totalSpacingXRate;
    private float[] fixedPosArr;
    private Toggle[] toggleArr;
    private int curIndex;
    private int tarIndex;
    private bool draging;
    private float startLocalX;
    private Vector3 UIPos = Vector3.zero;
    private float offset;
    private float viewPortWidth;
    public event Action<int> OnContentChanged;

    protected override void Awake()
    {
        Init(contentCount);
    }

    public void Init(int contentCount, int defaultIndex = 0)
    {
        this.contentCount = contentCount;
        this.fixedPosArr = new float[contentCount];
        this.toggleArr = toggleGroup.GetComponentsInChildren<Toggle>();
        this.contentGrid = content.GetComponent<GridLayoutGroup>();
        contentGrid.GetComponent<RectTransform>().pivot = Vector2.up * 0.5f;
        contentGrid.GetComponent<RectTransform>().anchorMin = Vector2.up * 0.5f;
        contentGrid.GetComponent<RectTransform>().anchorMax = Vector2.up * 0.5f;
        viewPortWidth = viewport.rect.width;
        this.contentGrid.padding.left = (int)(viewPortWidth - contentGrid.cellSize.x) / 2;
        this.contentGrid.spacing = Vector2.right * contentGrid.padding.left;
        this.totalSpacingX = contentGrid.cellSize.x + contentGrid.spacing.x;
        for (int i = 0; i < contentCount; i++)
        {
            fixedPosArr[i] = i * totalSpacingX;
            Debug.LogError("fixedPosArr[i]" + fixedPosArr[i]);
        }
        Debug.LogError("contentLen" + contentLen);
        this.contentLen = totalSpacingX * contentCount + contentGrid.padding.left + contentGrid.padding.right;
        this.totalSpacingXRate = totalSpacingX / contentLen;
        this.content.sizeDelta = new Vector2(contentLen, content.sizeDelta.y);
        //
        if (defaultIndex < 0 || defaultIndex >= contentCount)
            defaultIndex = 0;
        this.curIndex = defaultIndex;
        this.content.localPosition = GetPos(defaultIndex);
        this.toggleArr[defaultIndex].isOn = true;
        onValueChanged.AddListener((v2) =>
        {
            tarIndex = (int)(v2.x / totalSpacingXRate);
        });

        InitBound();
    }

    

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        draging = true;
        startLocalX = content.transform.localPosition.x;
        Debug.LogError("OnBeginDrag startLocalX:" + startLocalX);
    }



    public override void OnEndDrag(PointerEventData eventData)
    {
        if (draging == false)
            return;
        base.OnEndDrag(eventData);
        draging = false;
        offset = content.transform.localPosition.x - startLocalX;
        if (tarIndex == curIndex)
        {
            Debug.LogError("offset: " + offset + "--totalSpacingX * CRITICAL_RATE: " + totalSpacingX * CRITICAL_RATE);
            Debug.LogError("velocity:" + velocity.x);
            if (offset > totalSpacingX * CRITICAL_RATE)
            {
                Debug.LogError("offset > totalSpacingX * CtrticalRate");
                MoveToPos(tarIndex - 1);
                return;
            }
            if (offset < -totalSpacingX * CRITICAL_RATE)
            {
                Debug.LogError("offset < -totalSpacingX * CtrticalRate");
                MoveToPos(tarIndex + 1);
                return;
            }

            if (velocity.x > CRITICAL_VELOCITY)
            {
                Debug.LogError("velocity.x > VelocityCtrtical");
                MoveToPos(tarIndex - 1);
                return;
            }
            if (velocity.x < -CRITICAL_VELOCITY)
            {
                Debug.LogError("velocity.x < -VelocityCtrtical");
                MoveToPos(tarIndex + 1);
                return;
            }

            if (!FloatEquals(content.localPosition.x, fixedPosArr[curIndex]))
            {
                Debug.LogError("!FloatEquals(content.localPosition.x, fixedPosArr[curIndex])");
                MoveToPos(tarIndex);
            }

        }
        else if (tarIndex < curIndex)
        {
            Debug.LogError("tarIndex < curIndex");
            MoveToPos(curIndex - 1);
        }
        else
        {
            Debug.LogError("tarIndex > curIndex");
            MoveToPos(curIndex + 1);
        }
    }

    private void GetUIPos(Vector3 pos,ref Vector3 uiPos)
    {
        uiPos.x = pos.x - Screen.width / 2;
        uiPos.y = pos.y - Screen.height / 2;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        GetUIPos(eventData.position, ref UIPos);
        Debug.LogError("UIPos.position" + UIPos);

        if (UIPos.x < viewPortLeft)
        {
            OnEndDrag(eventData);
            return;
        }
        if (UIPos.x > viewPortRight)
        {
            OnEndDrag(eventData);
            return;
        }
        base.OnDrag(eventData);
    }

    private bool FloatEquals(float a, float b)
    {
        if (Mathf.Abs(a - b) > 0.01f)
            return false;
        return true;
    }


    private void MoveToPos(int index)
    {
        if (index < 0)
            index = 0;
        if (index >= contentCount)
            index = contentCount - 1;
        Debug.LogError("MoveTo:" + index + "-" + fixedPosArr[index]);
        velocity = Vector2.zero;
        content.DOLocalMove(GetPos(index), MOVE_TIME).onComplete = () =>
        {
            toggleArr[index].isOn = true;
        };
        if (curIndex != index)
            OnContentChanged?.Invoke(index);
        curIndex = index;
    }

    private Vector3 GetPos(int index)
    {
        return Vector2.up * content.localPosition.y + -Vector2.right * fixedPosArr[index];
    }

    public void InitBound()
    {
        viewPortLeft = viewport.localPosition.x - viewport.pivot.x * viewPortWidth;
        viewPortRight = viewport.localPosition.x + (1 - viewport.pivot.x) * viewPortWidth;
        Debug.LogError("viewPortLeft" + viewPortLeft);
        Debug.LogError("viewPortRight" + viewPortRight);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            MoveToPos(0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            MoveToPos(1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            MoveToPos(2);
        }
    }
}

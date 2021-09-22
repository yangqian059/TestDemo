using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class RectTransformEnhance 
{

    static Vector2Int targetRect;
    static int row, col;
    static int maxCount;
    public static void InitLayout(this RectTransform grid, List<GameObject> goList, Vector2 cellSize, Vector2 spacing, RectOffset padding  = null, 
        GridLayoutGroup.Corner startCorner = GridLayoutGroup.Corner.UpperLeft, 
        GridLayoutGroup.Axis startAxis = GridLayoutGroup.Axis.Vertical, 
        TextAnchor childAlignment = TextAnchor.UpperLeft)
    {
        if (goList == null || goList.Count == 0) return;
        // 初始化父物体，锚点，位置
        GameObject go;
        for (int i = 0; i < goList.Count; i++)
        {
            go = goList[i];
            go.transform.SetParent(grid);
            go.GetComponent<RectTransform>().sizeDelta = cellSize;
            go.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            go.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        }
        
        if (padding == null) padding = new RectOffset(0, 0, 0, 0);

        float xDis = cellSize.x + spacing.x;
        float yDis = cellSize.y + spacing.y;
        float rectWidth = grid.rect.width - padding.left - padding.right;
        float rectHeight = grid.rect.height - padding.top - padding.bottom;
        // 计算形状
        switch (startAxis)
        {
            case GridLayoutGroup.Axis.Horizontal:
                maxCount = (int)((rectWidth + spacing.x) / xDis);       // 每行最多容纳rowNum个
                row = goList.Count / maxCount + (goList.Count % maxCount == 0 ? 0 : 1);
                col = goList.Count < maxCount ? goList.Count : maxCount;
                break;
            case GridLayoutGroup.Axis.Vertical:
                maxCount = (int)((rectHeight + spacing.y) / yDis);       // 每列最多容纳colNum个
                col = goList.Count / maxCount + (goList.Count % maxCount == 0 ? 0 : 1);
                row = goList.Count < maxCount ? goList.Count : maxCount;
                break;
        }
        targetRect = new Vector2Int(row, col);       // 行数，列数


        // 计算偏移值
        float offsetX = cellSize.x * goList[0].GetComponent<RectTransform>().pivot.x;
        float offsetY = cellSize.y * (1 - goList[0].GetComponent<RectTransform>().pivot.y);
        //switch (startCorner)
        //{
        //    case GridLayoutGroup.Corner.UpperLeft:

        //        offsetX += padding.left;
        //        offsetY += padding.top;
        //        break;
        //    case GridLayoutGroup.Corner.LowerLeft:
        //        offsetX += padding.left;
        //        offsetY += padding.bottom;

        //        break;
        //    case GridLayoutGroup.Corner.LowerRight:
        //        offsetX += padding.right;
        //        offsetY += padding.bottom;
        //        break;
        //    case GridLayoutGroup.Corner.UpperRight:
        //        offsetX += padding.right;
        //        offsetY += padding.top;
        //        break;
        //}


        offsetX += padding.left;
        offsetY += padding.top;
        switch (childAlignment)
        {
            case TextAnchor.UpperLeft:
                break;
            case TextAnchor.UpperCenter:
                // 计算
                //offsetX += 
                break;
            case TextAnchor.UpperRight:
                offsetX += padding.right;
                break;
            case TextAnchor.MiddleLeft:
                // 计算

                break;
            case TextAnchor.MiddleCenter:
                // 计算

                break;
            case TextAnchor.MiddleRight:
                offsetX += padding.right;
                // 计算

                break;
            case TextAnchor.LowerLeft:
                offsetY += padding.bottom;
                break;
            case TextAnchor.LowerCenter:
                offsetX += padding.bottom;
                // 计算

                break;
            case TextAnchor.LowerRight:
                offsetX += padding.right;
                offsetY += padding.bottom;
                break;
        }
        float _offsetX = offsetX;
        float _offsetY = offsetY;


        Debug.Log("targetRect" + targetRect);




        // 摆出来
        int index = 0;
        float x = 0, y = 0;
        switch (startAxis)
        {
            case GridLayoutGroup.Axis.Horizontal:
                for (int i = 0; i < targetRect.x; i++)
                {
                    for (int j = 0; j < targetRect.y; j++)
                    {
                        go = goList[index++];
                        switch (startCorner)
                        {
                            case GridLayoutGroup.Corner.UpperLeft:
                                x = j * xDis + offsetX;
                                y = -i * yDis - offsetY;
                                break;
                            case GridLayoutGroup.Corner.UpperRight:
                                x = (targetRect.y - 1 - j) * xDis + offsetX;
                                y = -i * yDis - offsetY;
                                break;
                            case GridLayoutGroup.Corner.LowerLeft:
                                offsetY = _offsetY + (targetRect.x - 1) * yDis;
                                x = j * xDis + offsetX;
                                y = i * yDis - offsetY;
                                break;
                            case GridLayoutGroup.Corner.LowerRight:
                                offsetY = _offsetY + (targetRect.x - 1) * yDis;
                                x = (targetRect.y - 1 - j) * xDis + offsetX;
                                y = i * yDis - offsetY;
                                break;
                        }
                        
                        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                        Debug.LogError(go.GetComponent<RectTransform>().anchoredPosition);
                        if (index == goList.Count) break;
                    }
                    if (index == goList.Count) break;
                }
                break;
            case GridLayoutGroup.Axis.Vertical:
                for (int j = 0; j < targetRect.y; j++)
                {
                    for (int i = 0; i < targetRect.x; i++)
                    {
                        go = goList[index++];
                        switch (startCorner)
                        {
                            case GridLayoutGroup.Corner.UpperLeft:
                                x = j * xDis + offsetX;
                                y = -i * yDis - offsetY;
                                break;
                            case GridLayoutGroup.Corner.UpperRight:
                                x = (targetRect.y - 1 - j) * xDis + offsetX;
                                y = -i * yDis - offsetY;
                                break;
                            case GridLayoutGroup.Corner.LowerLeft:
                                offsetY = _offsetY - (targetRect.x - 1) * yDis;
                                x = j * xDis + offsetX;
                                y = i * yDis + offsetY;
                                break;
                            case GridLayoutGroup.Corner.LowerRight:
                                offsetY = _offsetY - (targetRect.x - 1) * yDis;
                                x = (targetRect.y - 1 - j) * xDis + offsetX;
                                y = i * yDis + offsetY;
                                break;
                        }
                        
                        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                        Debug.LogError(go.GetComponent<RectTransform>().anchoredPosition);

                        if (index == goList.Count) break;
                    }
                    if (index == goList.Count) break;
                }
                break;
        }


    }


    private static Vector2 GetPos(this RectTransform grid, TextAnchor childAlignment)
    {
        switch (childAlignment)
        {
            case TextAnchor.UpperLeft:

                break;
            case TextAnchor.UpperCenter:

                break;
            case TextAnchor.UpperRight:

                break;
            case TextAnchor.MiddleLeft:


                break;
            case TextAnchor.MiddleCenter:


                break;
            case TextAnchor.MiddleRight:


                break;
            case TextAnchor.LowerLeft:

                break;
            case TextAnchor.LowerCenter:

                break;
            case TextAnchor.LowerRight:

                break;
        }

        return Vector2.zero;
    }
}

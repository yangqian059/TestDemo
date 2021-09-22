using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowPos : MonoBehaviour,IPointerDownHandler, IPointerUpHandler,IDragHandler
{
    public RectTransform rect;

    private Vector2 offset;

    void Awake()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = offset + eventData.position;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = (Vector2)transform.position- eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.LogError(gameObject.name + "\nposition" + transform.position + "--localPosition" + transform.localPosition + "--anchoredPosition" + rect?.anchoredPosition
            + "\nrotation"+ transform.rotation.eulerAngles+ "--localRotation" + transform.rotation.eulerAngles + "--localScale" + transform.localScale);


        Debug.LogError(transform.localPosition + transform.parent.position);
        Debug.LogError(transform.position + CalcPosByLocalPosAndParent(transform.parent));
        //transform.localScale;
        //transform.rotation;
        //transform.localRotation;
        
    }

    void Update()
    {
        if(rect == null)
        {
            Debug.LogError(gameObject.name + "\nposition" + transform.position + "--localPosition" + transform.localPosition 
       +"\nrotation" + transform.rotation.eulerAngles + "--localRotation" + transform.rotation.eulerAngles + "--localScale" + transform.localScale) ;

        }
        

    }


    public Vector3 CalcPosByLocalPosAndParent(Transform parent)
    {
        Vector3 pos = parent.position + DotCalc(transform.localPosition,parent.localScale);

        float l = Vector3.Distance(transform.localPosition, Vector3.zero);
        float tanz = Mathf.Atan(pos.y / pos.x);
        float a = tanz - parent.rotation.eulerAngles.z;

        pos.x = Mathf.Cos(l);
        pos.y = Mathf.Sin(l);
        return pos;
    }


    private Vector3 DotCalc(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }

}

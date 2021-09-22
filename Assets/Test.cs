using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    void Start()
    {
        gameObject.SetActive(false);
        StartCoroutine(DelayToShowHello());

    }

    IEnumerator DelayToShowHello()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Hello");
        StartCoroutine(DelayToEnd());
    }


    IEnumerator DelayToEnd()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("End");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
        }
    }
}

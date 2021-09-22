using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleMono<T> : MonoBehaviour where T : SingleMono<T>
{
	private static Transform singleRoot;
	private static Transform SingleRoot
	{
		get
		{
			if (singleRoot == null)
				singleRoot = GameObject.Find("SingleRoot").transform;
			return singleRoot;
		}
	}
	private static T _Instance = null;
	private static bool created = false;


	public static T instance
	{
		get
		{
			return GetInstance();
		}
	}

	public static T GetInstance()
	{
		if (!created)
		{
			GameObject gObj = new GameObject(typeof(T).Name);
			gObj.transform.SetParent(SingleRoot);
			_Instance = gObj.AddComponent<T>();
			created = true;
		}
		return _Instance;
	}
}

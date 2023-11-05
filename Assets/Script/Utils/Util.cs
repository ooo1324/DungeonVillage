using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Util
{
    /// <summary>
    /// 게임 오브젝트의 T를 찾아주는 함수, 없을 경우 추가해서 반환
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns>찾거나, 추가한 T 반환</returns>
    public static T GetOrAddComponent<T>(GameObject obj) where T : UnityEngine.Component
    {
        T component = obj.GetComponent<T>();

        if (component == null)
            component = obj.AddComponent<T>();

        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {     
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform != null)
            return transform.gameObject;
        else
            return null;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            // 자기 자식만 찾는 경우
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if(component != null)
                        return component;
                }
            }
        
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                    return component;

            }
        }

        return null;
    }
}

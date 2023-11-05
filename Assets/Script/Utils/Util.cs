using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Util
{
    /// <summary>
    /// ���� ������Ʈ�� T�� ã���ִ� �Լ�, ���� ��� �߰��ؼ� ��ȯ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns>ã�ų�, �߰��� T ��ȯ</returns>
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
            // �ڱ� �ڽĸ� ã�� ���
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

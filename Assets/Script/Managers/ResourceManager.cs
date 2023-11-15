using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');

            // ��� �� �̸��� ã�� �ڸ���
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject obj = Managers.Pool.GetOriginal(name);

            if (obj != null)
                return obj as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        // Ǯ���Ⱦְ� ������ �¸� ��
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        // Ǯ�� ����� �ƴ� ���
        GameObject obj = Object.Instantiate(original, parent);
        obj.name = original.name;

        return obj;
    }

    public void Destroy(GameObject obj) 
    {
        if (obj == null)
            return;

        // ���� Ǯ���� �ʿ��� ���̶�� -> Ǯ�� �Ŵ������� ��Ź
        Poolable poolable = obj.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        // Ǯ�� ����� �ƴ� ���
        Object.Destroy(obj);
    }
}

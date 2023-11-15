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

            // 경로 중 이름만 찾아 자르기
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

        // 풀링된애가 있으면 걔를 뱉어냄
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        // 풀링 대상이 아닌 경우
        GameObject obj = Object.Instantiate(original, parent);
        obj.name = original.name;

        return obj;
    }

    public void Destroy(GameObject obj) 
    {
        if (obj == null)
            return;

        // 만약 풀링이 필요한 아이라면 -> 풀링 매니저한테 위탁
        Poolable poolable = obj.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        // 풀링 대상이 아닌 경우
        Object.Destroy(obj);
    }
}

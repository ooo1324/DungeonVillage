using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }
    
        Stack<Poolable> poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        Poolable Create()
        {
            GameObject obj = Object.Instantiate<GameObject>(Original);
            obj.name = Original.name;
            return obj.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;
            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (poolStack.Count > 0)
                poolable = poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive (true);

            // DontDestroyOnLoad 해제 용도
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }

    #endregion

    Dictionary<string, Pool> poolDic = new Dictionary<string, Pool>();
    Transform root;

    public void Init()
    {
        if (root == null)
        {
            root = new GameObject { name="@Pool_Root"}.transform; 
            Object.DontDestroyOnLoad(root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = root;

        poolDic.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if (!poolDic.ContainsKey(name))
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }            

        poolDic[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null) 
    {
        if (!poolDic.ContainsKey(original.name))
            CreatePool(original);

        return poolDic[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (!poolDic.ContainsKey(name))
            return null;

        return poolDic[name].Original;
    }

    public void Clear()
    {
        foreach(Transform child in root)
            GameObject.Destroy(child.gameObject);

        poolDic.Clear();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDic();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDic { get; private set; } = new Dictionary<int, Data.Stat>();

    public void Init()
    {
        StatDic = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDic();

    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}

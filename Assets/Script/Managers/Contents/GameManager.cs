using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject player;

    HashSet<GameObject> monsterDic = new HashSet<GameObject>();

    public GameObject GetPlayer() { return player; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject obj = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                monsterDic.Add(obj);
                break;
            case Define.WorldObject.Player:
                player = obj;
                break;
        }

        return obj;
    }

    public Define.WorldObject GetWorldObjectType(GameObject obj)
    {
        BaseController bc = obj.GetComponent<BaseController>();

        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;   
    }

    public void Despawn(GameObject obj)
    {
        Define.WorldObject type = GetWorldObjectType(obj);

        switch (type)
        {
            case Define.WorldObject.Monster:
                {
                    if (monsterDic.Contains(obj))
                        monsterDic.Remove(obj);
                }
                break;
            case Define.WorldObject.Player:
                {
                    if (player == obj)
                        player = null;
                }
                break;
        }

        Managers.Resource.Destroy(obj);
    }
}

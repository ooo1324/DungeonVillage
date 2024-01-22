using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        // 인벤토리 Pop
        Managers.UI.ShowSceneUI<UI_Inven>();
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDic;
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        Managers.Game.Spawn(Define.WorldObject.Monster, "Skeleton");

        GameObject obj = new GameObject { name = "SpawningPool" };
        SpawningPool spawnPool = obj.GetOrAddComponent<SpawningPool>();
        obj.transform.position = new Vector3();
        spawnPool.SetKeepMonsterCount(5);
    }


    public override void Clear()
    {
        
    }
}

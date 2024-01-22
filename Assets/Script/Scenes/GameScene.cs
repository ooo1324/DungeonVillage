using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        // 인벤토리 Pop
       // Managers.UI.ShowSceneUI<UI_Inven>();

        // Stat 딕셔너리
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDic;

        // 커서 설정
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        GameObject obj = new GameObject { name = "SpawningPool" };
        SpawningPool spawnPool = obj.GetOrAddComponent<SpawningPool>();
        spawnPool.SpawnPos = new Vector3(-34, 0, 70);
        spawnPool.SetKeepMonsterCount(5);
    }



    public override void Clear()
    {
        
    }
}

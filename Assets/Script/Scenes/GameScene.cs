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

        Managers.UI.ShowSceneUI<UI_Inven>();

        gameObject.GetOrAddComponent<CursorController>();
    }


    public override void Clear()
    {
        
    }
}

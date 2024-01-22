using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
        
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);


        GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform).gameObject;

        UI_Inven_Item inven_item = item.GetOrAddComponent<UI_Inven_Item>();
        inven_item.SetInfo($"도란반지");

        //실제 인벤토리 정보 참고해서 넣기
        for (int i = 0; i < 19; i++)
        {
            GameObject item2 = Managers.UI.MakeSubItem<UI_Inven_Item>(parent : gridPanel.transform, "UI_Inven_Item_not").gameObject;

            UI_Inven_Item inven_item2 = item2.GetOrAddComponent<UI_Inven_Item>();
            inven_item2.SetInfo($"");
        }

    }

}

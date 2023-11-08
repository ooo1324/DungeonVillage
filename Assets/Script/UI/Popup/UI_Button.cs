using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        ItemIcon

    }

    void Start()
    {
        Init();
    }


    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        GameObject obj = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(obj, (PointerEventData data) => { obj.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    int scoreText = 0;
    public void OnButtonClicked(PointerEventData data) 
    {
        scoreText++;
        GetText((int)Texts.ScoreText).text = $"Á¡¼ö : {scoreText}";
    }
}

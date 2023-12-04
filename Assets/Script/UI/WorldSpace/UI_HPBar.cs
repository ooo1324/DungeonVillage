using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar    
    }

    Stat stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        // Player혹은 붙여놓은 컴포넌트의 Stat 추출
        stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        // 메인 카메라 바라보도록
        transform.rotation = Camera.main.transform.rotation;

        float ratio = (float)stat.Hp / (float)stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}

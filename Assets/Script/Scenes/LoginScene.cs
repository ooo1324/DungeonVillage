using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        //List<GameObject> list = new List<GameObject>();
        //for (int i = 0; i < 5; i++)
        //    list.Add(Managers.Resource.Instantiate("Player"));

        //foreach(GameObject obj in list)
        //    Managers.Resource.Destroy(obj);
    }

    private void Update()
    {
    }

    public void GameStart()
    {
        Managers.Scene.LoadScene(Define.Scene.Loading);
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : BaseScene
{
    [SerializeField]
    Image progressBar;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Loading;
    }

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    void Update()
    {
     
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(Managers.Scene.GetSceneName(Define.Scene.Game));

        // Scene�� ��⸸ �س��� ��ȯ������ �ʴ� ����
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                // �ּ� �ε�
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
        //Managers.Scene.AsyncLoadScene(Define.Scene.Game);
    }

    public override void Clear()
    {
        Debug.Log("LoadingScene Clear!");
    }
}

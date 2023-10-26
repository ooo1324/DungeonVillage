using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region instance
    private static Managers _instance;

    public static Managers Instance { get { Init(); return _instance; } }
    #endregion

    #region private Manager ������

    InputManager _input = new InputManager();
    ResourceManager _resourceManager;

    #endregion

    #region static Manager properties

    /// <summary>
    /// Input Manager : �Է� ���� �Ŵ���
    /// </summary>
    public static InputManager Input { get { return Instance._input; } }

    /// <summary>
    /// ResourceManager : ���ҽ� Load, Destroy ���� �Ŵ���
    /// </summary>
    public static ResourceManager ResourceManager { get { return Instance._resourceManager; } }
    #endregion


    private static string managersName = "@Managers";


    private void Awake()
    {
        Init();
    }

    void Update()
    {
        Input.OnUpdate();
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject managerObj = GameObject.Find(managersName);

            if (managerObj == null)
            {
                managerObj = new GameObject { name = managersName }; 
                managerObj.AddComponent<Managers>();
            }
            
            DontDestroyOnLoad(managerObj);

            _instance = managerObj.GetComponent<Managers>();
        }

    }
}

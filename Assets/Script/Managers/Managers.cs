using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region instance
    private static Managers _instance;

    public static Managers Instance { get { Init(); return _instance; } }
    #endregion

    #region private Manager 函荐甸

    InputManager _inputManager = new InputManager();
    ResourceManager _resourceManager = new ResourceManager();
    SceneManagerEx _sceneManager = new SceneManagerEx();
    SoundManager _soundManager = new SoundManager();
    UIManager _uiManager = new UIManager();

    #endregion

    #region static Manager properties

    /// <summary>
    /// Input Manager : 涝仿 包府 概聪历
    /// </summary>
    public static InputManager Input { get { return Instance._inputManager; } }

    /// <summary>
    /// ResourceManager : 府家胶 Load, Destroy 包府 概聪历
    /// </summary>
    public static ResourceManager Resource { get { return Instance._resourceManager; } }

    /// <summary>
    /// Scene Manager : 纠 包府 概聪历
    /// </summary>
    public static SceneManagerEx Scene { get { return Instance._sceneManager; } }

    /// <summary>
    /// Sound Manager : 荤款靛 包府 概聪历
    /// </summary>
    public static SoundManager Sound { get { return Instance._soundManager; } }

    /// <summary>
    /// UIManager : UI 包府 概聪历
    /// </summary>
    public static UIManager UI { get { return Instance._uiManager; } }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int order = 10;

    Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    UI_Scene sceneUI = null;

    public GameObject Root
    {
        get 
        {
            GameObject root = GameObject.Find("@UI_Root");

            if (root == null)
                root = new GameObject { name = "@UI_Root" };

            return root;
        }
    }

    public void SetCanvas(GameObject obj, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(obj);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject obj = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            obj.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(obj);

    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject obj = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T scene = Util.GetOrAddComponent<T>(obj);
        sceneUI = scene;

        obj.transform.SetParent(Root.transform);

        return scene;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        GameObject obj = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(obj);
        popupStack.Push(popup);

      
        obj.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count == 0)
            return;

        if (popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        UI_Popup popup = popupStack.Pop();

        //부모 통해서 삭제
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        order--;
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();

    }
}

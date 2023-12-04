using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    Texture2D attackIcon;
    Texture2D handIcon;

    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType cursorType = CursorType.None;

    int mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    void Start()
    {
        attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Cursor_Attack");
        handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Cursor_Hand");

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(attackIcon, new Vector2(attackIcon.width / 5, 0), CursorMode.Auto);
                    cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(handIcon, new Vector2(handIcon.width / 3, 0), CursorMode.Auto);
                    cursorType = CursorType.Hand;
                }
            }
        }
    }
}

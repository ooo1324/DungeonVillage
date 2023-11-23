using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    PlayerStat stat;
    Vector3 destPos;

    Texture2D attackIcon;
    Texture2D handIcon;

    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType cursorType = CursorType.None;

    void Awake()
    {
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    void Start()
    {
        attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Cursor_Attack");
        handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Cursor_Hand");

        stat = GetComponent<PlayerStat>();
    }

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill
    }

    PlayerState state = PlayerState.Idle;

    void UpdateDie()
    {
       
    }

    void UpdateMoving()
    {
        // ���⺤�� ���
        Vector3 dir = destPos - transform.position;

        // ��Ȯ�� 0�� ������ �������� �ֱ� ������ ����
        if (dir.magnitude < 0.3f)
        {
           // moveToDest = false;
            state = PlayerState.Idle;
        }
        else
        {
            //TODO
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            // Clamp �ּ� �ִ� ���� ����
            float moveDist = Mathf.Clamp(stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);

            //nma.CalculatePath()
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 2f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 2f, dir, 1f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false)
                    state = PlayerState.Idle;
                return;
            }

            //transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }


        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", stat.MoveSpeed);
  
    }

    void UpdateIdle()
    {
        // ��ų������ ó���� blend ��� ó�� state machine�� ���� �ʰ� ���� �����ϴ� ��찡 �� ����
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);

    }

    void Update()
    {
        UpdateMouseCursor();

        switch (state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        } 
    }

    /// <summary>
    /// ���콺 Ŀ�� ���� Update �Լ�
    /// </summary>
    void UpdateMouseCursor()
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

    int mask = (1 << (int)Define.Layer.Ground) | (1 <<  (int)Define.Layer.Monster);

    GameObject lockTarget;

    void OnMouseEvent(Define.MouseEvent evt)
    {
        if (state == PlayerState.Die)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100f, mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        destPos = hit.point;
                        state = PlayerState.Moving;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            lockTarget = hit.collider.gameObject;
                        else
                            lockTarget = null;
                    }
                }
                break;          
            case Define.MouseEvent.Press:
                {
                    if (lockTarget != null)
                        destPos = lockTarget.transform.position;
                    else
                        if (raycastHit)
                            destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                lockTarget = null;
                break;
        }
    }

    #region Old
    //void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += Vector3.forward * Time.deltaTime * speed;
    //    }

    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //        transform.position += Vector3.back * Time.deltaTime * speed;
    //    }

    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //        transform.position += Vector3.left * Time.deltaTime * speed;
    //    }

    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //        transform.position += Vector3.right * Time.deltaTime * speed;
    //    }
    //    moveToDest = false;
    //}
    #endregion
}
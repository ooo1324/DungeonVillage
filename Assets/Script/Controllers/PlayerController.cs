using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;

   // bool moveToDest = false;
    Vector3 destPos;

    void Awake()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    void Start()
    {

    }

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Channeling,
        Jumping,
        Falling
    }

    PlayerState state = PlayerState.Idle;

    void UpdateDie()
    {
        // 아무것도 몬함
    }

    void UpdateMoving()
    {
        // 방향벡터 계산
        Vector3 dir = destPos - transform.position;

        // 정확한 0이 나오지 않을때가 있기 때문에 유의
        if (dir.magnitude < 0.0001f)
        {
           // moveToDest = false;
            state = PlayerState.Idle;
        }
        else
        {
            // Clamp 최소 최대 범위 제한
            float moveDist = Mathf.Clamp(speed * Time.deltaTime, 0, dir.magnitude);

            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }


        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", speed);
  
    }

    void UpdateIdle()
    {
        // 스킬같은건 처음에 blend 방식 처럼 state machine에 넣지 않고 따로 관리하는 경우가 더 많음
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);

    }

    void Update()
    {
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

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Wall")))
        {
            // hit.point : hit 위치를 월드좌표 기준으로 반환해줌
            destPos = hit.point;
            state = PlayerState.Moving;
            //moveToDest = true;
        }
    }
}
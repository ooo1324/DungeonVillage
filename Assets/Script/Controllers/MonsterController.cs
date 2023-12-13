using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat stat;

    [SerializeField]
    float scanRange = 10;

    [SerializeField]
    float attackRange = 2;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;

        stat = GetComponent<Stat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {
        // TODO : 매니저가 생기면 옮기기
        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;

        float dist = (player.transform.position - transform.position).magnitude;
        
        // 사정거리 안에 들어옴
        if (dist <= scanRange)
        {
            lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        // 플레이어가 내 사정거리보다 가까우면 공격
        if (lockTarget != null)
        {
            destPos = lockTarget.transform.position;
            float distance = (destPos - transform.position).magnitude;

            if (distance <= attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }

        // 방향벡터 계산
        Vector3 dir = destPos - transform.position;

        // 정확한 0이 나오지 않을때가 있기 때문에 유의
        if (dir.magnitude < 0.3f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.SetDestination(destPos);
            nma.speed = stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        if (lockTarget != null)
        {
            Vector3 dir = lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if (lockTarget != null)
        {
            // 체력 소비
            Stat targetStat = lockTarget.GetComponent<Stat>();

            targetStat.OnAttacked(stat);

            if (targetStat.Hp > 0)
            {
                float dist = (lockTarget.transform.position - transform.position).magnitude;
                if (dist <= attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}

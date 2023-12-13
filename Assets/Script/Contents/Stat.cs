using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int level;

    [SerializeField]
    protected int hp;

    [SerializeField]
    protected int maxHp;

    [SerializeField]
    protected int attack;

    [SerializeField]
    protected int defense;

    [SerializeField]
    protected float moveSpeed;

    public int Level { get { return level; } set { level = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defence { get { return defense; } set { defense = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    private void Start()
    {
        level = 1;
        hp = 100;
        maxHp = 100;
        attack = 10;
        defense = 5;
        MoveSpeed = 15;
    }

    public virtual void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defence);
        Hp -= damage;

        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }

    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;

        if (playerStat != null)
        {
            // TODO: 몬스터 고유의 경험치로 변경해야 함
            playerStat.Exp += 15;
        }

        Managers.Game.Despawn(gameObject);
    }
}

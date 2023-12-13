using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int exp;

    [SerializeField]
    protected int gold;

    public int Exp 
    {
        get { return exp; } 
        set 
        {
            exp = value;
            // 레벨업 체크
            int level = Level;
            
            while (true)
            {
                Data.Stat stat;
                if (!Managers.Data.StatDic.TryGetValue(level + 1, out stat))
                    break;

                if (exp < stat.totalExp)
                    break;

                level++;
            }

            if (level != Level)
            {
                Debug.Log("Level Up!");
                Level = level;
                SetStat(Level);
            }
        } 
    }
    public int Gold { get { return gold; } set { gold = value; } }

    private void Start()
    {
        level = 1;
        exp = 0;
        defense = 5;
        MoveSpeed = 15;
        gold = 0;

        SetStat(level);
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDic;
        Data.Stat initStat = dict[level];
        hp = initStat.maxHp;
        maxHp = initStat.maxHp;
        attack = initStat.attack;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
    }
}

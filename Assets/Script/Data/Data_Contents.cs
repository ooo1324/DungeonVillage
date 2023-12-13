using System;
using System.Collections.Generic;
namespace Data
{
    #region Stat

    [Serializable]
    public class Stat
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDic()
        {
            Dictionary<int, Stat> dic = new Dictionary<int, Stat>();

            foreach (Stat stat in stats)
                dic.Add(stat.level, stat);

            return dic;
        }
    }

    #endregion

}

using System;
using System.Collections.Generic;

namespace HOA.Stats
{
    public class Booster : Scalar
    {
        Func<Stat> toBoost;

        private Booster(Unit self, string name, sbyte normal, Func<Stat> toBoost)
            : base(self, name, normal)
        {
            this.toBoost = toBoost;
        }

        public override void Add(sbyte amount, byte index = 0) 
        {
            if (index > 0)
                throw new IndexOutOfRangeException();
            primary += amount;
            Stat boosted = toBoost();
            boosted += amount;
        }

        #region Builders

        public static Booster FoIn(Unit self)
        { return new Booster(self, "Focus", 0, () => self.initiative); }

        public static Booster FoDef(Unit self)
        { return new Booster(self, "Focus", 0, () => self.defense); }
        
        #endregion

        public static Booster operator +(Booster s, sbyte i) { s.Add(i); return s; }
    }
}
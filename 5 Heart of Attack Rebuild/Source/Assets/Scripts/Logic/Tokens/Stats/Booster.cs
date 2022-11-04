using System;
using System.Collections.Generic;

namespace HOA.To.St
{
    public class Booster<T> : Scalar<T>
        where T: struct, IComparable<T>, IEquatable<T>
    {
        Func<Stat<T>> toBoost;

        protected Booster(Unit self, string name, T normal, Func<Stat<T>> toBoost)
            : base(self, name, normal)
        {
            this.toBoost = toBoost;
        }

        public override void Add(Func<T, T, T> adder, T amount) 
        { 
            primary.Add(adder, amount); 
            toBoost().Add(adder, amount);
        }
        
	}

    public class Booster : Booster<sbyte>
    {

        private Booster(Unit self, string name, sbyte normal, Func<Stat<sbyte>> toBoost)
            : base(self, name, normal, toBoost)
        { }

        public static Booster FoIn(Unit self)
        { return new Booster(self, "Focus", 0, () => self.initiative); }

        public static Booster FoDef(Unit self)
        { return new Booster(self, "Focus", 0, () => self.defense); }

    }
}
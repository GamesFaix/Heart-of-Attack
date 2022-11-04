using System;
using HOA.To.St;

namespace HOA.To
{
    /// <summary> Manages unit health, defense, and damage </summary>
    public class Vitality : TokenComponent
    {
        /// <summary> Health  </summary>
        public virtual Capped<sbyte> health { get; protected set; }
        /// <summary> Defense </summary>
        public virtual Stat<sbyte> defense { get; protected set; }

        public Func<sbyte, bool> Damage { get; protected set; }

        private Vitality(Unit thisToken, Capped<sbyte> health, Stat<sbyte> defense)
            : base (thisToken)
        {
            this.health = health;
            this.defense = defense;
            Damage = DamageStandard;
        }
        
        /// <summary>  Create new Vitality </summary>
        public Vitality(Unit thisToken, sbyte hp = 0, sbyte def = 0)
            : this(thisToken, Capped.Hel(thisToken,hp), Scalar.Def(thisToken, def))
        { }

        public static Vitality DodgeHalf(Unit thisToken, sbyte hp, sbyte def = 0)
        {
            Vitality v = new Vitality(thisToken, hp, def);
            v.Damage = v.DamageDodgeHalf;
            return v;
        }

        public static Vitality DefenseCap(Unit thisToken, sbyte hp, sbyte def = 0, sbyte defCap = 127)
        {
            return new Vitality(thisToken, Capped.Hel(thisToken, hp), Capped.Def(thisToken, def, defCap));
        }


        /// <summary> Destroy parent if Health is less than 1.  </summary>
        protected void DieIfZero() 
        {
            if (health < 1)
            {
                Ef.Queue.Add(Ef.Effect.DestroyUnit(ThisToken, new Ef.Args(ThisToken))); 
                Log.Game("{0}'s health is less than 1!  Destroying...", ThisToken); 
            }
        }

        /// <summary> Add to Health, check if less than 1. </summary>
         public virtual int AddHealth(sbyte amount)
        {
            health.Add((a, b) => (sbyte)(a + b), amount);
            DieIfZero();
            return health;
        }
        
        /// <summary>  Add to Health.Max, check if less than 1 </summary>
        public virtual int AddHealthCap(sbyte amount)
        {
            health.AddCap((a, b) => (sbyte)(a + b), amount); 
            DieIfZero();
            return health.Cap;
        }

        /// <summary>  Health -= (n - Defense), checks if health less than 1 </summary> 
        public bool DamageStandard(sbyte amount)
        {
            if (amount < 1)
                return false;
            else if (amount <= defense)
                return false;
            else
            {
                int dmg = amount - defense;
                health.Add((a, b) => (sbyte)(a + b), (sbyte)(0 - dmg));
                DieIfZero();
                return true;
            }
        }

        public bool DamageDodgeHalf(sbyte damage)
        {
            if (HOA.Random.Range(0,1) == 1)
                return DamageStandard(damage);
            else
                return false;
        }

        /// <summary> "[Parent]'s Health" </summary>
        public override string ToString() { return ThisToken + "'s Health"; }
    }
}

using System;

namespace HOA.To
{
    /// <summary>
    /// Manages unit health, defense, and damage
    /// </summary>
    public class Vitality : TokenComponent
    {
        /// <summary>
        /// Health 
        /// </summary>
        public virtual Stat Health { get; protected set; }
        /// <summary>
        /// Defense
        /// </summary>
        public virtual Stat Defense { get; protected set; }

        public Func<int, bool> Damage { get; protected set; }

        private Vitality(Unit thisToken, Stat health, Stat defense)
            : base (thisToken)
        {
            Health = health;
            Defense = defense;
            Damage = DamageStandard;
        }
        
        /// <summary>
        /// Create new Vitality
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="hp"></param>
        /// <param name="def"></param>
        public Vitality(Unit thisToken, int hp = 0, int def = 0)
            : this(thisToken,
            Stat.Health(thisToken, hp),
            Stat.Defense(thisToken, def)) { }

        public static Vitality DodgeChance(Unit thisToken, int hp, int dodgePercent, int def = 0)
        {
            Vitality v = new Vitality(thisToken, hp, def);
            v.Damage = v.DamageDodgeHalf;
            return v;
        }

        public static Vitality DefenseCap(Unit thisToken, int hp, int def = 0, int defCap = 255)
        {
            return new Vitality(thisToken,
                Stat.Health(thisToken, hp),
                Stat.Defense(thisToken, def, defCap));
        }


        /// <summary>
        /// Destroy parent if Health is less than 1.
        /// </summary>
        protected void DieIfZero() 
        {
            if (Health < 1)
            //  EffectQueue.Add(Effect.DestroyUnit(source, Parent)); 
            { Log.Debug("Not implemented."); }
        }

        /// <summary>
        /// Add to Health, check if less than 1.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public virtual int AddHealth(int n)
        {
            Health.Add(n);
            DieIfZero();
            return Health;
        }
        
        /// <summary>
        /// Add to Health.Max, check if less than 1
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public virtual int AddHealthMax(int n)
        {
            Health.AddMax(n);
            DieIfZero();
            return Health.Max;
        }

        /// <summary>
        /// Health -= (n - Defense), checks if health less than 1
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool DamageStandard(int n)
        {
            if (n < 1)
                return false;
            else if (n <= Defense)
                return false;
            else
            {
                int dmg = n - Defense;
                Health.Add(0 - dmg);
                DieIfZero();
                return true;
            }
        }

        public bool DamageDodgeHalf(int damage)
        {
            if (HOA.Random.Range(0,1) == 1)
                return DamageStandard(damage);
            else
                return false;
        }

        /// <summary>
        /// "[Parent]'s Health"
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return ThisToken + "'s Health"; }
    }
}

using System;
using System.Collections.Generic;
using HOA.Stats;
using HOA.Fargo;


namespace HOA.Tokens
{	

    public class StatSheet : TokenComponent
    {
        #region Properties

        ArgTable<FS, Stat> stats;

        public Capped health 
        { 
            get { return stats[FS.Health] as Capped; } 
            private set { stats[FS.Health] = value; }
        }

        public Stat defense
        { 
            get { return stats[FS.Defense]; }
            private set { stats[FS.Defense] = value; }
        }

        public Scalar initiative 
        { 
            get { return stats[FS.Initiative] as Scalar; }
            private set { stats[FS.Initiative] = value; }
        }

        public Capped energy 
        { 
            get { return stats[FS.Energy] as Capped; } 
            private set { stats[FS.Energy] = value; }
        }

        public Scalar focus 
        { 
            get { return stats[FS.Focus] as Scalar; } 
            private set { stats[FS.Focus] = value; }
        }

        public Stat this[Fargo.FS statName]
        {
            get
            {
                return stats[statName];
            }
        }

        public bool skipped { get; private set; }

        public Func<sbyte, bool> Damage { get; private set; }

        #endregion

        #region Constructors

        private StatSheet(Unit self, sbyte health, sbyte defense, sbyte initiative, sbyte energy, sbyte defCap = -100)
            : base(self)
        {
            stats = ArgTable.Stat(
                Arg.Stat(FS.Health, new Capped(health, health, DieIfZero)),
                Arg.Stat(FS.Initiative, new Scalar(initiative)),
                Arg.Stat(FS.Energy, new Capped(0, energy)));

            Stat s;
            if (defCap == -100)
                s = new Scalar(defense);
            else
                s = new Capped(defense, defCap);
            stats.Add(Arg.Stat(FS.Defense, s));
            
            stats.Add(Arg.Stat(FS.Focus, new Scalar(0)));

            this.skipped = false;

            Damage = DamageStandard;
        }

        public StatSheet(Unit self, sbyte health, sbyte initiative, sbyte defense = 0)
            : this(self, health, defense, initiative, 2)
        { }

        public static StatSheet King(Unit self, sbyte health, sbyte initiative, sbyte defense = 0)
        {
            return new StatSheet(self, health, defense, initiative, 3);
        }

        public static StatSheet FocusSideEffects(Unit self, sbyte health, sbyte initiative, Action<sbyte> sideEffect, sbyte defense = 0)
        {
            var sheet = new StatSheet(self, health, defense, initiative, 2);
            sheet.focus = new Scalar(0, sideEffect);
            return sheet;
        }

        public static StatSheet DefenseCap(Unit self, sbyte health, sbyte initiative, sbyte defense, sbyte defCap)
        {
            return new StatSheet(self, health, defense, initiative, 2, defCap);
        }

        public static StatSheet FocusSideEffectsDefenseCap(Unit self, sbyte health, sbyte initiative, sbyte defense, sbyte defCap, Action<sbyte> sideEffect)
        {
            var sheet = new StatSheet(self, health, defense, initiative, 2, defCap);
            sheet.focus = new Scalar(0, sideEffect);
            return sheet;
        }

        public static StatSheet HalfDodge(Unit self, sbyte health, sbyte initiative, sbyte defense = 0)
        {
            var sheet = new StatSheet(self, health, defense, initiative, 2);
            sheet.Damage = sheet.DamageDodgeHalf;
            return sheet;
        }

        #endregion

        

        #region Payment

        public bool CanAfford(Price p) { return (energy >= p.Energy && focus >= p.Focus); }
        public void Charge(Price p) { energy -= p.Energy; focus -= p.Focus; }
        public void Refund(Price p) { energy += p.Energy; focus += p.Focus; }

        #endregion

        #region Damage
        
        
        
        void DieIfZero(sbyte n)
        {
            if (n < 1)
            {
                Ef.Queue.Add(Ef.Effect.DestroyUnit(self, new Ef.EffectArgs(
                    Fargo.Arg.Target(Fargo.FT.Token, self))));
                Log.Game("{0}'s health is less than 1!  Destroying...", self);
            }
        }

        public bool DamageStandard(sbyte amount)
        {
            if (amount < 1 || amount <= defense[0])
                return false;
            else
            {
                checked 
                { 
                    health -= (sbyte)(amount - defense[0]); 
                }               
                return true;
            }
        }

        public bool DamageDodgeHalf(sbyte amount)
        {
            return (HOA.Random.Range(0, 1) == 1) ? DamageStandard(amount) : false;
        }

        #endregion


        public override string ToString()
        {
            return string.Format(
                "HP:{0} / DEF:{1} / IN:{2} / Skip:{3} / EP:{4} / FP:{5}", 
                health, defense, initiative, skipped, energy, focus);
        }
    }
}
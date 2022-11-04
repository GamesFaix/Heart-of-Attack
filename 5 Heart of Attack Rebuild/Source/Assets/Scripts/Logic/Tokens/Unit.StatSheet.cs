using System;
using System.Collections.Generic;
using HOA.Stats;
using HOA.Args;
using Farg = HOA.Args.Arg;

namespace HOA.Tokens
{
    public partial class Unit
    {
        public Capped energy { get { return stats.energy; } }
        public Scalar focus { get { return stats.focus; } }
        public Scalar initiative { get { return stats.initiative; } }
        public bool skipped { get { return stats.skipped; } }
        public Capped health { get { return stats.health; } }
        public Stat defense { get { return stats.defense; } }
        
        public void Charge(Price p) { stats.Charge(p); }
        public bool CanAfford(Price p) { return stats.CanAfford(p); }

        public void StatAdd(object source, RS stat, sbyte amount, bool secondary = false)
        {
            if (!secondary)
                stats[stat].Add(amount);
            else
                stats[stat].Add(amount, 1);
        }
        public void StatSet(object source, RS stat, sbyte amount, bool secondary = false)
        {
            if (!secondary)
                stats[stat].Set(amount);
            else
                stats[stat].Set(amount, 1);
        }

        public bool Damage(object source, sbyte n) { return stats.Damage(n); }


        internal class StatSheetArgs
        {
            internal sbyte health, initiative, energy, defense, defCap;
            internal Func<StatSheet, Action<sbyte>> focusEffect;
            internal Func<StatSheet, Func<sbyte, bool>> damage;

            internal StatSheetArgs(
                sbyte health,
                sbyte initiative,
                sbyte energy = 2,
                sbyte defense = 0,
                sbyte defCap = -100,
                Func<StatSheet, Action<sbyte>> focusEffect = null,
                Func<StatSheet, Func<sbyte, bool>> damage = null)
            {
                this.health = health;
                this.initiative = initiative;
                this.energy = energy;
                this.defense = defense;
                this.defCap = defCap;
                this.focusEffect = focusEffect;
                this.damage = damage;
            }

        }

        internal class StatSheet
        {
            #region Properties

            readonly Unit self;

            ArgTable<RS, Stat> stats;

            public Capped health
            {
                get { return stats[RS.Health] as Capped; }
                private set { stats[RS.Health] = value; }
            }

            public Stat defense
            {
                get { return stats[RS.Defense]; }
                private set { stats[RS.Defense] = value; }
            }

            public Scalar initiative
            {
                get { return stats[RS.Initiative] as Scalar; }
                private set { stats[RS.Initiative] = value; }
            }

            public Capped energy
            {
                get { return stats[RS.Energy] as Capped; }
                private set { stats[RS.Energy] = value; }
            }

            public Scalar focus
            {
                get { return stats[RS.Focus] as Scalar; }
                private set { stats[RS.Focus] = value; }
            }

            public Stat this[RS statName]
            {
                get
                {
                    return stats[statName];
                }
            }

            public bool skipped { get; private set; }

            public Func<sbyte, bool> Damage { get; private set; }

            #endregion

            internal StatSheet(Unit self, StatSheetArgs args)
            {
                this.self = self;
                stats = ArgTable.Stat(
                    Arg.Stat(RS.Health, new Capped(args.health, args.health, DieIfZero)),
                    Arg.Stat(RS.Initiative, new Scalar(args.initiative)),
                    Arg.Stat(RS.Energy, new Capped(0, args.energy)));

                Stat s;
                if (args.defCap == -100)
                    s = new Scalar(args.defense);
                else
                    s = new Capped(args.defense, args.defCap);
                stats.Add(Arg.Stat(RS.Defense, s));

                if (args.focusEffect == null)
                    stats.Add(Arg.Stat(RS.Focus, new Scalar(0)));
                else
                    stats.Add(Arg.Stat(RS.Focus, new Scalar(0, 
                        args.focusEffect(this))));

                this.skipped = false;

                Damage = (args.damage == null) ? DamageStandard : args.damage(this);
            }

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
                    Effects.EffectQueue.Add(Effects.Effect.DestroyUnit(self, new Effects.EffectArgs(
                        Arg.Target(RT.Token, self))));
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

            internal static Func<sbyte, bool> DamageDodgeHalf(StatSheet stats)
            {
                return stats.DamageDodgeHalf;
            }

            #endregion

            #region Focus Effects

            internal static Action<sbyte> BoostDefense(StatSheet stats)
            {
                return (n) => { stats.defense.Add(n); };
            }

            internal static Action<sbyte> BoostInitiative(StatSheet stats)
            {
                return (n) => { stats.initiative.Add(n); };
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
}
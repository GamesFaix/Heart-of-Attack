using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HOA.Abilities;
using HOA.Collections;
using HOA.Stats;
using HOA.Args;

using ATuple = HOA.Abilities.AbilityTuple;
using AClosure = HOA.Abilities.AbilityClosure;
using ARank = HOA.Abilities.AbilityRank;
using Pred = System.Predicate<HOA.Abilities.AbilityClosure>;

namespace HOA.Tokens
{
    public partial class Unit
    {
        private void Learn(AbilityTuple tuple)
        {
            arsenal.Add(new AbilityClosure(this, tuple.ability, tuple.args));
        }
        private void Learn(Ability ability, AbilityArgs args)
        {
            arsenal.Add(new AbilityClosure(this, ability, args));
        }

        internal class Arsenal : IEnumerable<AClosure>
        {
            #region Properties

            readonly Unit self;

            readonly Set<AClosure> abilities;

            public AClosure Move
            {
                get
                {
                    Pred p = (a) => { return a.rank == ARank.Move; };
                    return abilities.SingleOrDefault(p.ToFunc());
                }
            }

            public AClosure Focus
            {
                get
                {
                    Pred p = (a) => { return a.rank == ARank.Focus; };
                    return abilities.SingleOrDefault(p.ToFunc());
                }
            }

            public AClosure Attack
            {
                get
                {
                    Pred p = (a) => { return a.rank == ARank.Attack; };
                    return abilities.SingleOrDefault(p.ToFunc());
                }
            }

            #endregion

            internal Arsenal(Unit self, ATuple[] abilities)
            {
                this.self = self;
                this.abilities = new Set<AClosure>(9);
                Add(abilities);
            }

            internal Arsenal(Unit self, Ledger<Ability, AbilityArgs> abilities)
            {
                this.self = self;
                this.abilities = new Set<AClosure>(9);
                Add(abilities);
            }

            public override string ToString() { return self + "'s Arsenal"; }

            public void Reset() { foreach (AClosure a in abilities) a.Reset(); }

            public bool HasAbility(string name, out AClosure ability)
            {
                ability = null;
                foreach (AClosure a in abilities)
                    if (a.name == name)
                    {
                        ability = a;
                        return true;
                    }
                return false;
            }

            public bool Contains(AClosure a) { return (abilities.Contains(a)); }

            public void Sort() { abilities.Sort(); }

            bool RankFull(AClosure a)
            {
                ARank r = a.rank;
                if (this[r].Count < Content.Tokens.rankLimits[r])
                    return true;
                Log.Debug("Arsenal cannot hold any more {0} abilities.", r);
                return false;
            }
            bool RankFull(Ability a)
            {
                ARank r = a.rank;
                if (this[r].Count < Content.Tokens.rankLimits[r])
                    return true;
                Log.Debug("Arsenal cannot hold any more {0} abilities.", r);
                return false;
            }

            #region Add/Remove/Replace

            public void Add(AClosure a)
            {
                if (!RankFull(a))
                    abilities.Add(a);
                Sort();
            }

            public void Add(params AClosure[] abilities)
            {
                foreach (AClosure a in abilities)
                    if (!RankFull(a))
                        this.abilities.Add(a);
                Sort();
            }

            public void Add(params ATuple[] abilities)
            {
                foreach (ATuple a in abilities)
                    if (!RankFull(a.ability))
                        this.abilities.Add(new AbilityClosure(self, a.ability, a.args));
                Sort();
            }


            public void Add(Ledger<Ability, AbilityArgs> abilities)
            {
                foreach (Ability a in abilities)
                    if (!RankFull(a))
                        this.abilities.Add(new AbilityClosure(self, a, abilities[a]));
            }

            public bool Remove(string name)
            {
                AClosure a;
                if (!HasAbility(name, out a))
                    return false;
                abilities.Remove(a);
                return true;
            }

            public bool Remove(AClosure a) { return abilities.Remove(a); }

            public bool Replace(AClosure oldAb, AClosure newAb)
            {
                if (oldAb == null || newAb == null)
                    throw new ArgumentNullException();
                if (!Remove(oldAb))
                    throw new ArgumentException("Ability not found");
                Add(newAb);
                Sort();
                return true;
            }

            public bool Replace(string oldName, AClosure newAb)
            {
                AClosure oldAb;
                if (!HasAbility(oldName, out oldAb))
                    return false;
                return Replace(oldAb, newAb);
            }

            #endregion

            #region Indexers/Iterators

            public IEnumerator<AClosure> GetEnumerator() { return abilities.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

            public AClosure this[int i] { get { return abilities[i]; } }

            public Set<AClosure> this[ARank rank]
            {
                get
                {
                    Set<AClosure> result = new Set<AClosure>(Content.Tokens.rankLimits[rank]);
                    foreach (AClosure a in this)
                        if (a.rank == rank)
                            result.Add(a);
                    result.TrimExcess();
                    return result;
                }
            }

            #endregion

        }

    }
}
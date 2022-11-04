using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HOA.Abilities;


namespace HOA.Tokens
{
    public class Arsenal : TokenComponent, IEnumerable<AbilityClosure>
    {
        #region Properties

        Set<AbilityClosure> list;
        static Dictionary<AbilityRank, byte> rankLimits;

        public AbilityClosure Move
        {
            get
            {
                Predicate<AbilityClosure> p = (a) => { return a.rank == AbilityRank.Move; };
                return list.SingleOrDefault(p.ToFunc());
            }
        }

        public AbilityClosure Focus
        {
            get
            {
                Predicate<AbilityClosure> p = (a) => { return a.rank == AbilityRank.Focus; };
                return list.SingleOrDefault(p.ToFunc());
            }
        }

        public AbilityClosure Attack
        {
            get
            {
                Predicate<AbilityClosure> p = (a) => { return a.rank == AbilityRank.Attack; };
                return list.SingleOrDefault(p.ToFunc());
            }
        }

        #endregion

        #region Constructors

        public static void Load()
        {
            InitializeRankLimits();
            Log.Start("Arsenal rank limits set.");
        }

        static void InitializeRankLimits()
        {
            rankLimits = new Dictionary<AbilityRank, byte>(6);
            rankLimits.Add(AbilityRank.Move, 1);
            rankLimits.Add(AbilityRank.Focus, 1);
            rankLimits.Add(AbilityRank.Attack, 1);
            rankLimits.Add(AbilityRank.Special, 3);
            rankLimits.Add(AbilityRank.Create, 3);
            rankLimits.Add(AbilityRank.None, 5);
        }

        public Arsenal(Token self) 
            : base(self)
        {
           // if (rankLimits == null)
             //   InitializeRankLimits();
            list = new Set<AbilityClosure>(9);
        }

        

        #endregion

        public override string ToString() { return self + "'s Arsenal"; }

        public void Reset() { foreach (AbilityClosure a in list) a.Reset(); }

        public bool HasAbility(string name, out AbilityClosure ability)
        {
            ability = null;
            foreach (AbilityClosure a in list)
                if (a.name == name)
                {
                    ability = a;
                    return true;
                }
            return false;
        }

        public bool Contains(AbilityClosure a) { return (list.Contains(a)); }

        public void Sort() { list.Sort(); }

        bool RankFull(AbilityClosure a)
        {
            AbilityRank r = a.rank;
            if (this[r].Count < rankLimits[r])
                return true;
            Log.Debug("Arsenal cannot hold any more {0} abilities.", r);
            return false;
        }

        #region Add/Remove/Replace

        public void Add(AbilityClosure a) 
        { 
            if (!RankFull(a)) 
                list.Add(a);
            Sort();
        }

        public void Add(params AbilityClosure[] abilities) 
        {
            foreach (AbilityClosure a in abilities)
                if (!RankFull(a))
                    list.Add(a);
            Sort();
        }

        public bool Remove(string name)
        {
            AbilityClosure a;
            if (!HasAbility(name, out a))
                return false;
            list.Remove(a);
            return true;
        }

        public bool Remove(AbilityClosure a) { return list.Remove(a); }

        public bool Replace(AbilityClosure oldAb, AbilityClosure newAb)
        {
            if (oldAb == null || newAb == null)
                throw new ArgumentNullException();
            if (!Remove(oldAb))
                throw new ArgumentException("Ability not found");
            Add(newAb);
            Sort();
            return true;
        }

        public bool Replace(string oldName, AbilityClosure newAb)
        {
            AbilityClosure oldAb;
            if (!HasAbility(oldName, out oldAb))
                return false;
            return Replace(oldAb, newAb);
        }

        #endregion

        #region Indexers/Iterators

        public IEnumerator<AbilityClosure> GetEnumerator() { return list.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public AbilityClosure this[int i] { get { return list[i]; } }

        public Set<AbilityClosure> this[AbilityRank rank]
        {
            get
            {
                Set<AbilityClosure> result = new Set<AbilityClosure>(rankLimits[rank]);
                foreach (AbilityClosure a in this)
                    if (a.rank == rank)
                        result.Add(a);
                result.TrimExcess();
                return result;
            }
        }

        #endregion

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HOA.Ab;


namespace HOA.To
{
    public class Arsenal : TokenComponent, IEnumerable<Ab.Closure>
    {
        #region Properties
        
        Set<Ab.Closure> list;
        static Dictionary<Ab.Rank, byte> rankLimits;

        public Ab.Closure Move
        {
            get
            {
                Predicate<Ab.Closure> p = (a) => { return a.rank == Ab.Rank.Move; };
                return list.SingleOrDefault(p.ToFunc());
            }
        }

        public Ab.Closure Focus
        {
            get
            {
                Predicate<Ab.Closure> p = (a) => { return a.rank == Ab.Rank.Focus; };
                return list.SingleOrDefault(p.ToFunc());
            }
        }

        public Ab.Closure Attack
        {
            get
            {
                Predicate<Ab.Closure> p = (a) => { return a.rank == Ab.Rank.Attack; };
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
            rankLimits = new Dictionary<Ab.Rank, byte>(6);
            rankLimits.Add(Ab.Rank.Move, 1);
            rankLimits.Add(Ab.Rank.Focus, 1);
            rankLimits.Add(Ab.Rank.Attack, 1);
            rankLimits.Add(Ab.Rank.Special, 3);
            rankLimits.Add(Ab.Rank.Create, 3);
            rankLimits.Add(Ab.Rank.None, 5);
        }

        public Arsenal(Token thisToken) 
            : base(thisToken)
        {
           // if (rankLimits == null)
             //   InitializeRankLimits();
            list = new Set<Ab.Closure>(9);
        }

        

        #endregion

        public override string ToString() { return ThisToken + "'s Arsenal"; }

        public void Reset() { foreach (Ab.Closure a in list) a.Reset(); }

        public bool HasAbility(string name, out Ab.Closure ability)
        {
            ability = null;
            foreach (Ab.Closure a in list)
                if (a.name == name)
                {
                    ability = a;
                    return true;
                }
            return false;
        }

        public bool Contains(Ab.Closure a) { return (list.Contains(a)); }

        public void Sort() { list.Sort(); }

        bool RankFull(Ab.Closure a)
        {
            Ab.Rank r = a.rank;
            if (this[r].Count < rankLimits[r])
                return true;
            Log.Debug("Arsenal cannot hold any more {0} abilities.", r);
            return false;
        }

        #region Add/Remove/Replace
        
        public void Add(Ab.Closure a) 
        { 
            if (!RankFull(a)) 
                list.Add(a);
            Sort();
        }

        public void Add(params Ab.Closure[] abilities) 
        {
            foreach (Ab.Closure a in abilities)
                if (!RankFull(a))
                    list.Add(a);
            Sort();
        }

        public bool Remove(string name)
        {
            Ab.Closure a;
            if (!HasAbility(name, out a))
                return false;
            list.Remove(a);
            return true;
        }

        public bool Remove(Ab.Closure a) { return list.Remove(a); }

        public bool Replace(Ab.Closure oldAb, Ab.Closure newAb)
        {
            if (oldAb == null || newAb == null)
                throw new ArgumentNullException();
            if (!Remove(oldAb))
                throw new ArgumentException("Ability not found");
            Add(newAb);
            Sort();
            return true;
        }

        public bool Replace(string oldName, Ab.Closure newAb)
        {
            Ab.Closure oldAb;
            if (!HasAbility(oldName, out oldAb))
                return false;
            return Replace(oldAb, newAb);
        }

        #endregion

        #region Indexers/Iterators

        public IEnumerator<Ab.Closure> GetEnumerator() { return list.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public Ab.Closure this[int i] { get { return list[i]; } }

        public Set<Ab.Closure> this[Ab.Rank rank]
        {
            get
            {
                Set<Ab.Closure> result = new Set<Ab.Closure>(rankLimits[rank]);
                foreach (Ab.Closure a in this)
                    if (a.rank == rank)
                        result.Add(a);
                result.TrimExcess();
                return result;
            }
        }

        #endregion

    }
}
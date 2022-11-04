using System;
using System.Collections;
using System.Collections.Generic;
using HOA.Abilities;
using HOA.Collections;

namespace HOA.Tokens
{
    public class Arsenal : TokenComponent, IEnumerable<Ability>
    {
        #region Properties
        
        ListSet<Ability> list;
        static Dictionary<Abilities.Rank, byte> rankLimits;

        public Ability Move
        {
            get
            {
                ListSet<Ability> moves = this[Abilities.Rank.Move];
                return ((moves == null) ? moves[0] : null);
            }
        }

        public Ability Focus
        {
            get
            {
                ListSet<Ability> foci = this[Abilities.Rank.Focus];
                return ((foci == null) ? foci[0] : null);
            }
        }

        public Ability Attack
        {
            get
            {
                ListSet<Ability> attacks = this[Abilities.Rank.Attack];
                return ((attacks == null) ? attacks[0] : null);
            }
        }

        #endregion

        #region Constructors
        
        public Arsenal(Token thisToken) 
            : base(thisToken)
        {
            if (rankLimits == null)
                InitializeRankLimits();
            list = new ListSet<Ability>(9);
        }

        static void InitializeRankLimits()
        {
            rankLimits = new Dictionary<Abilities.Rank, byte>(5);
            rankLimits.Add(Abilities.Rank.Move, 1);
            rankLimits.Add(Abilities.Rank.Focus, 1);
            rankLimits.Add(Abilities.Rank.Attack, 1);
            rankLimits.Add(Abilities.Rank.Special, 3);
            rankLimits.Add(Abilities.Rank.Create, 3);
        }

        #endregion

        public override string ToString() { return ThisToken + "'s Arsenal"; }

        public void Reset() { foreach (Ability a in list) a.Reset(); }

        public bool HasAbility(string name, out Ability ability)
        {
            ability = null;
            foreach (Ability a in list)
                if (a.Name == name)
                {
                    ability = a;
                    return true;
                }
            return false;
        }

        public bool Contains(Ability a) { return (list.Contains(a)); }

        public bool Perform(int i)
        {
            Debug.Log("Not implemented.");
            return false;
        }

        public void Sort() { list.Sort(); }

        bool RankFull(Ability a)
        {
            Abilities.Rank r = a.Rank;
            if (this[r].Count < rankLimits[r])
                return true;
            Debug.Log("Arsenal cannot hold any more " + a.Rank + " abilities.");
            return false;
        }

        #region Add/Remove/Replace
        
        public void Add(Ability a) 
        { 
            if (!RankFull(a)) 
                list.Add(a);
            Sort();
        }

        public void Add(params Ability[] abilities) 
        {
            foreach (Ability a in abilities)
                if (!RankFull(a))
                    list.Add(a);
            Sort();
        }

        public bool Remove(string name)
        {
            Ability a;
            if (!HasAbility(name, out a))
                return false;
            list.Remove(a);
            return true;
        }

        public bool Remove(Ability a) { return list.Remove(a); }

        public bool Replace(Ability oldAb, Ability newAb)
        {
            if (oldAb == null || newAb == null)
                throw new ArgumentNullException();
            if (!Remove(oldAb))
                throw new ArgumentException("Ability not found");
            Add(newAb);
            Sort();
            return true;
        }

        public bool Replace(string oldName, Ability newAb)
        {
            Ability oldAb;
            if (!HasAbility(oldName, out oldAb))
                return false;
            return Replace(oldAb, newAb);
        }

        #endregion

        #region Indexers/Iterators

        public IEnumerator<Ability> GetEnumerator() { return list.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public Ability this[int i] { get { return list[i]; } }

        public ListSet<Ability> this[Abilities.Rank rank]
        {
            get
            {
                ListSet<Ability> result = new ListSet<Ability>(rankLimits[rank]);
                foreach (Ability a in this)
                    if (a.Rank == rank)
                        result.Add(a);
                return ((result.Count > 0) ? result : null);
            }
        }

        #endregion

    }
}
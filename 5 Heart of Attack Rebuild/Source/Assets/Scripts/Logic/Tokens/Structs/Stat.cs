using System;
using UnityEngine;

namespace HOA.Tokens
{
    public partial class Stat : IEquatable<Stat>
    {
        #region Properties
        /// <summary>
        /// Name of stat
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Unit stat belongs to
        /// </summary>
        public Unit Parent { get; private set; }
        /// <summary>
        /// Stat enum value
        /// </summary>
        public Stats Stats { get; private set; }

        /// <summary>
        /// Default or starting value
        /// </summary>
        public int Normal { get; private set; }
        /// <summary>
        /// Current value
        /// </summary>
        public int Current { get; private set; }
        /// <summary>
        /// Min value, enforced by Clamp()
        /// </summary>
        public int Min { get; private set; }
        /// <summary>
        /// Max value, enforced by Clamp()
        /// </summary>
        public int Max { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int Modifier { get; private set; }
        /// <summary>
        /// Used to invert display for negative effects
        /// </summary>
        public bool Debuff { get; private set; }

        #endregion
        #region Methods

        /// <summary>
        /// Returns 0 if Current = Normal, -1 if Current lower, +1 if Current higher
        /// </summary>
        public Func<int> Mod { get; private set; }
        /// <summary>
        /// Not implemented
        /// </summary>
        public Func<int> ModMax { get; private set; }
        /// <summary>
        /// Sets Current value
        /// </summary>
        public Func<int, int> Set { get; private set; }
        /// <summary>
        /// Sets Max value
        /// </summary>
        public Func<int, int> SetMax { get; private set; }
        /// <summary>
        /// Adds amount to current value
        /// </summary>
        public Func<int, int> Add { get; private set; }
        /// <summary>
        /// Adds amount to max value
        /// </summary>
        public Func<int, int> AddMax { get; private set; }

        #endregion

        private Stat(string name, Unit parent, Stats stats, 
            int normal, int min = 0, int max = 100, 
            int modifier = 0, bool debuff = false)
        {
            Name = name;
            Parent = parent;
            Stats = stats;

            Normal = normal;
            Current = Normal;
            Min = min;
            Max = max;
            Modifier = modifier;
            Debuff = debuff;

            Mod = () =>
            {
                int comparison = Current.CompareTo(Normal);
                if (debuff) comparison *= (-1);
                return comparison;
            };
            ModMax = () => { return 0; };
            Set = (n) =>
            {
                Current = n;
                Clamp();
                return Current;
            };
            SetMax = (n) =>
            {
                Max = n;
                Clamp();
                return Max;
            };
            Add = (n) =>
            {
                Current += n;
                Clamp();
                return Current;
            };
            AddMax = (n) =>
            {
                Max += n;
                Clamp();
                return Max;
            };
        }

        /// <summary>
        /// Returns current value as int.
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public static implicit operator int(Stat stat) { return stat.Current; }

        /// <summary>
        /// Returns current value as string
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return Current + ""; }

        private void Clamp()
        {
            if (Current < Min) Current = Min;
            if (Current > Max) Current = Max;
        }

        #region IEquatable
        /// <summary>
        /// Checks if enum and current are equal
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Equals(Stat s)
        {
            return (Stats == s.Stats && Current == s.Current);
        }
        /// <summary>
        /// Checks if obj is Stat, and if enum and current are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is Stat && (Equals(obj as Stat)));
        }
        /// <summary>
        /// a.Equals(b)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Stat a, Stat b) { return a.Equals(b); }
        /// <summary>
        /// !a.Equals(b)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Stat a, Stat b) { return !a.Equals(b); }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            Debug.Log("Default hashcode.");
            return base.GetHashCode();
        }

        #endregion
    }
}

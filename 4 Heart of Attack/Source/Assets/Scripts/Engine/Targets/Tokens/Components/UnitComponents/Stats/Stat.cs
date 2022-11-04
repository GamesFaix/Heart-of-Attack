using System;
using UnityEngine;

namespace HOA { 

	public enum Stats : byte {DEFAULT, Health, MaxHealth, Defense, Initiative, Energy, Focus, Stun}

	public partial class Stat : IInspectable{
	
        public string Name {get; protected set;}
        public Unit Parent { get; protected set; }
		public Stats Stats { get; protected set; }
        public int Min {get; protected set;}
		public int Max {get; protected set;}
		public int Current {get; protected set;}
		public int Normal {get; protected set;}
        public int Modifier { get; protected set; }
        protected bool debuff;

        public Func<int> Modified { get; private set; }
        public Func<int> MaxModified { get; private set; }
        public Func<int, int> Set { get; private set; }
        public Func<int, int> SetMax { get; private set; }
        public Func<Source, int, bool, int> Add { get; private set; }
        public Func<Source, int, bool, int> AddMax { get; private set; }

        private Stat (string name, Unit parent, Stats stats, int normal, int min = 0, int max = 100, int modifier = 0) 
        {
			Name = name;
            Parent = parent;
			Stats = stats;
            Normal = normal;
            Min = min;
            Max = max;
            Current = Normal;
            Modifier = modifier;
			debuff = false;

            Modified = () => 
            {
                int comparison = Current.CompareTo(Normal);
                if (debuff) comparison *= (-1);
                return comparison;
            };
            MaxModified = () => { return 0; };
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
            Add = (s, n, log) =>
            {
                Current += n;
                Clamp();
                if (log)
                      GameLog.Out(s+ ": " +Parent+ " " +Sign(n)+n+Name + ". " +Name+ " = " +Current);
                return Current;
            };
            AddMax = (s, n, log) =>
            {
                Max += n;
                Clamp();
                if (log)
                    GameLog.Out(s+ ": " +Parent+ " " + Sign(n)+n+ " Max " +Name+ ". " +
                        Name+ " = " +Current+ "/" +Max);
                return Max;
            };
		}

		public static implicit operator int (Stat stat) {return stat.Current;}

		public override string ToString () {return Current+"";}
        
		private void Clamp () {
			if (Current < Min) Current = Min;
			if (Current > Max) Current = Max;
		}

        private string Sign(int n) { return (n > 0 ? "+" : "");  }

        public void Draw(Panel panel) { InspectorInfo.Stat(this, panel); }
	}
}

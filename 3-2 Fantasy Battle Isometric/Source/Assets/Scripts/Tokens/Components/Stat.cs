namespace HOA { 

	public class Stat {
	
		sbyte min;
		byte max;
		public int Current {get; protected set;}
		public byte Normal {get; protected set;}
		bool debuff;


		Stat (byte normal, sbyte min=-127, byte max=255, bool debuff=false) {
			Normal = normal;
			Current = Normal;
			this.min = min;
			this.max = max;
			this.debuff = debuff;
		}

		public static Stat HP (byte normal) {return new Stat (normal, 0);}
		public static Stat DEF (byte normal) {return new Stat (normal, 0);}
		public static Stat AP () {return new Stat (0,0);}
		public static Stat FP () {return new Stat (0,0);}
		public static Stat IN (byte normal) {return new Stat (normal);}
		public static Stat Stun () {return new Stat (0,0,255,true);}
		public static Stat Cor () {return new Stat (0,0,255,true);}

		public static implicit operator int (Stat stat) {return stat.Current;}

		public override string ToString() {return Current.ToString();}

		public int Modified {
			get {
				int comparison = Current.CompareTo(Normal);
				if (debuff) {comparison *= (-1);}
				return comparison;
			} 
		}
	
		public int Add (int n) {
			Current += n;
			Clamp();
			return Current;
		}

		public int Set (int n) {
			Current = n;
			Clamp();
			return Current;
		}

		void Clamp () {
			if (Current < min) {Current = min;}
			if (Current > max) {Current = max;}
		}

	
	
	}
}

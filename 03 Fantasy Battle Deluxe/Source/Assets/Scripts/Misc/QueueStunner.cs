using System.Collections.Generic;

namespace HOA {

	public static class QueueStunner {

		static Dictionary<Unit, int> stunned;

		public static void Find (HOAQueue<Unit> units) {
			stunned = new Dictionary<Unit,int>();
			foreach (Unit u in units) {
				if (u.IsStunned()) {stunned.Add(u, units.IndexOf(u));}
			}
		}

		public static void Hold (HOAQueue<Unit> units) {
			foreach (Unit sU in stunned.Keys){
				foreach (Unit u in units) {
					if (u == sU){
						if (units.IndexOf(u) != stunned[sU]){
							units.Remove(sU);
							if (stunned[sU] != 0){units.Insert(stunned[sU], sU);}
							else {units.Add(sU);}
						}
					}
				}
			}
		}

		public static void Decrement () {
			foreach (Unit u in stunned.Keys) {u.AddStat(new Source(), STAT.STUN, -1);}
		}
	}
}
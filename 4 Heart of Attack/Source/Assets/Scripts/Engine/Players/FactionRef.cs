using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {
	
	public static class FactionRef {
	
		static List<Faction> factions = new List<Faction> { 
			new FGearp(), new FNewRepublic(), new FTorridale(), new FGrove(), 
			new FChrono(), new FPsycho(), new FPsilent(), new FVoidoid(), new FObstacle()
		};

		public static Faction Index (int i) {return factions[i];}
		public static int Count {get {return factions.Count;} }
		
		public static int LargestSize {
			get {
				int largest = 0;
				foreach (Faction f in factions) {
					if (f.Count > largest) {largest = f.Count;}
				}
				return largest;
			}
		}
		
		public static List<Faction> Playable {
			get {
				List<Faction> playable = new List<Faction>();
				foreach (Faction f in factions) {if (f.Playable) {playable.Add(f);} }
				return playable;
			}
		}

		public static Faction Neutral () {return factions[Count-1];}
		
		public static List<Species> Kings {
			get {
				List<Species> kings = new List<Species>();
				foreach (Faction f in factions) {if (f.King != Species.None) {kings.Add(f.King);} }
				return kings;
			}
		}

		static List<Faction> taken = new List<Faction>();
		public static List<Faction> Taken {get {return taken;} }

		public static void Take (Faction f) {taken.Add(f);}
		public static void Release (Faction f) {taken.Remove(f);}
		public static void ReleaseAll () {
			taken = new List<Faction>();}

		public static List<Faction> Free {
			get {
				List<Faction> free = new List<Faction>();
				foreach (Faction f in factions) {if (!taken.Contains(f) && f.Playable) {free.Add(f);} }
				return free;
			}
		}
		
		public static string[] FreeNames {
			get {
				string[] names = new string[Free.Count];
				for (int i=0; i<names.Length; i++) {names[i] = Free[i].ToString();}
				return names;	
			}
		}

		public static Faction RandomFree {
			get {
				int random = Mathf.RoundToInt(Random.Range(0, Free.Count));
				return Free[random];
			}			
		}
	}
}

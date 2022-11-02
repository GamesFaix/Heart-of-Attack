using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class EffectQueue {

		static List<List<Effect>> effects = new List<List<Effect>>();

		public static void Add (Effect e) {
			effects.Add(new List<Effect> {e});
			if (!processing) {Process();}
		}
		public static void Add (List<Effect> e) {
			effects.Add(e);
			if (!processing) {Process();}
		}

		static List<Effect> Top {get {return effects[0];} }
		static void RemoveTop () {effects.Remove(Top);}

		static float duration = 2;
		static float startTime = 0;
		static bool processing = false;

		static void Process () {
			processing = true;
			startTime = Time.time;

			foreach (Effect e in Top) {e.Process();}
			if (Time.time - startTime >= duration) {
				RemoveTop();
				if (effects.Count > 0) {Process();}
				else {processing = false;}
			}
		}
	}
}
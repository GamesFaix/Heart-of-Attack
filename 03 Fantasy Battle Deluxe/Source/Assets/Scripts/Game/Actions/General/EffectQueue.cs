using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class EffectQueue : MonoBehaviour {

		static List<IEffect> effects = new List<IEffect>();

		public static void Add (IEffect e) {
			effects.Add(e);
			if (!processing) {Process();}
		}

		public static void Interrupt (IEffect e) {
			effects.Insert(1, e);
		}

		static IEffect Top {get {return effects[0];} }
		static void RemoveTop () {effects.Remove(Top);}

		static float duration = 0.5f;
		static float startTime = 0;
		static bool processing = false;
		public static bool Processing { get { return processing; } }

		static void Process () {
			processing = true;
			startTime = Time.time;
			Top.Process();
		}

		void Update () {
			if (processing && Time.time - startTime >= duration) {
				if (!ActiveSequence) {RemoveTop();}
				if (effects.Count > 0) {Process();}
				else {processing = false;}
			}

		}

		bool ActiveSequence {
			get {
				if (Top is EffectSeq) {
					EffectSeq es = (EffectSeq)Top;
					if (es.Count > 1) {return true;}
				}
				return false;
			}
		}
	}
}
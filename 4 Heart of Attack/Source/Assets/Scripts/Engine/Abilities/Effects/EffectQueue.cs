using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class EffectQueue : MonoBehaviour {

		static List<IEffect> effects = new List<IEffect>();
		static float duration = 0.3f;
		static float startTime = 0;

		static bool processing = false;
		public static bool Processing { get {return processing;} }

		public static void Add (IEffect e) {
			effects.Add(e);
			if (!processing) {Process();}
		}

		public static void Interrupt (IEffect e) {
			effects.Insert(1, e);
		}

		static IEffect Top {get {return effects[0];} }

		static IEffect Pop () {
			if (effects.Count > 0) {
				IEffect e = Top;
				effects.Remove(e);
				return e;
			}
			return default(IEffect);
		}

		static void Process () {
			processing = true;
			startTime = Time.time;
			Top.Process2();
		}

		void Update () {
			if (processing && Time.time - startTime >= duration) {
				if (!ActiveSequence) {Pop();}
				if (effects.Count > 0) {Process();}
				else {
					if (Game.ActivePending) {Game.Activate();}
					processing = false;
				}
			}
		}

		bool ActiveSequence { 
			get {
				if (Top is EffectSeq && ((EffectSeq)Top).Count > 0) {
					return true;
				}
				return false;
			} 
		}
	}
}
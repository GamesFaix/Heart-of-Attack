using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Mine : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Mine (source, template);
		}

		Mine(Source s, bool template=false){
			ID = new ID(this, EToken.MINE, s, false, template);
			Special.Add(EType.DEST);
			Plane = Plane.Sunk;
			Body = new BodySensor9(this, SensorMine.Instantiate);
		}
		public override string Notes () {return "If any Token enters Mine's Cell or a neighboring Cell, destroy Mine.\nWhen Mine is destroyed, do 10 damage to all units in its cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";}
		
		public override void Die (Source s, bool corpse = false, bool log=true) {
			GameObject.Destroy(Display.gameObject);
			Debug.Log(this+" dying");
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			TokenFactory.Remove(this);
//			Cell oldCell = Cell;
			Body.Exit();
			if (log && !Special.Is(EType.HEART)) {GameLog.Out(s.ToString()+" destroyed "+this+".");}
			/*if (s.Sequence != default(EffectSeq)) {
				Debug.Log("valid sequence");
				s.Sequence.AddToNext(new EExplosion(s, Cell, 12));
			}
			else {
		*/
			EffectQueue.Interrupt(new EExplosion(new Source(this), Body.Cell, 12));
		//	}
			((BodySensor9)Body).DestroySensors();
		}
	}

	public class EDetonate : EffectSeq {
		public override string ToString () {return "EffectSeq - Detonate";}
		Token target;
		
		public EDetonate (Source s, Token t) {
			source = s; target = t;

			list = new List<EffectGroup>();

			EffectGroup group = new EffectGroup();
			group.Add(new EDetonate1 (new Source(source.Token, this), target));
			list.Add(group);
		}
	}

	public class EDetonate1 : Effect {
		public override string ToString () {return "Effect - Detonate1";}
		Token target;
		
		public EDetonate1 (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			Mixer.Play(SoundLoader.Effect(EEffect.DETONATE));
			target.Display.Effect(EEffect.DETONATE);
			source.Sequence.AddToNext(new EDetonate2(source, target));
		}

	}

	public class EDetonate2 : Effect {
		public override string ToString () {return "Effect - Detonate2";}
		Token target;
		
		public EDetonate2 (Source s, Token t) {
			source = s; target = t;
			Debug.Log(ToString());
		}
		public override void Process() {
			Debug.Log("processing "+ToString());
			target.Die(source);
		}
	}
}
using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Mine : Obstacle {
		public Mine(Source s, bool template=false){
			NewLabel(EToken.MINE, s, false, template);
			sprite = new HOA.Sprite(this);
			body = new BodyMine(this);
		}
		public override string Notes () {return "If any Token enters Mine's Cell or a neighboring Cell, destroy Mine.\nWhen Mine is destroyed, do 10 damage to all units in its cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";}
		
		public override void Die (Source s, bool corpse = false, bool log=true) {
			Debug.Log(this+" dying");
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			TokenFactory.Remove(this);
//			Cell oldCell = Cell;
			Exit();
			if (log && !IsClass(EClass.HEART)) {GameLog.Out(s.ToString()+" destroyed "+this+".");}
			/*if (s.Sequence != default(EffectSeq)) {
				Debug.Log("valid sequence");
				s.Sequence.AddToNext(new EExplosion(s, Cell, 12));
			}
			else {
		*/
				EffectQueue.Interrupt(new EExplosion(new Source(this), Cell, 12));
		//	}
			BodyMine bodyMine = (BodyMine)body;
			bodyMine.DestroySensors();
		}
	}

	public class BodyMine : Body{
		List<Sensor> sensors;
		
		public BodyMine(Token t){
			parent = t;
			SetPlane(EPlane.SUNK);
			SetClass(new List<EClass> {EClass.OB, EClass.DEST});
			OnDeath = EToken.NONE;
			sensors = new List<Sensor>();
		}
		
		public override bool Enter (Cell newCell) {

			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				
				foreach (Sensor s in sensors) {s.Delete();}
				
				CellGroup sensorCells = newCell.Neighbors(true);
				foreach (Cell c in sensorCells) {
					Sensor s = new SensorMine(parent, c);
					sensors.Add(s);
					c.AddSensor(s);
				}
				
				return true;
			}	
			if (newCell == TemplateFactory.c) {
				cell = newCell;
				return true;	
			}
			return false;
		}
		
		public override void Exit () {
			cell.Exit(parent);
		}
		
		public void DestroySensors () {
			for (int i=sensors.Count-1; i>=0; i--) {
				Sensor s = sensors[i];
				s.Delete();
			}
		}
		
	}

	public class SensorMine : Sensor {

		public SensorMine (Token par, Cell c) {
			parent = par;	
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {cell = c;}
		public override void Exit () {}
		
		public override void OtherEnter (Token t) {
			EffectQueue.Interrupt(new EDetonate(new Source(t), parent));
		}
		public override void OtherExit (Token t) {}
		
		public override string ToString () {
			return "Trigger ("+parent.ToString()+")";
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
			target.SpriteEffect(EEffect.DETONATE);
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
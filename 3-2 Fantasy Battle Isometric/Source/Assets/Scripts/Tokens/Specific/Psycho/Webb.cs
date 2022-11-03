using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Web : Obstacle {
		Dictionary<Unit, int> affected;
		public Dictionary<Unit, int> Affected {get {return affected;} }

		public Web(Source s, bool template=false){
			ID = new ID(this, EToken.WEBB, s, false, template);
			Plane = Plane.Sunk;
			Special.Add(EType.DEST);
			Body = new BodySensor1(this, SensorWeb.Instantiate);	
			Neutralize();
			affected = new Dictionary<Unit, int>();
		}
		public override string Notes () {return 
			"Ground and Air units may not move through "+ID.Name+"." +
			"\nUnits sharing "+ID.Name+"'s Cell have a Move Range of 1.";
		}
	
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(s,corpse,log);
		}
	
	}

	public class EStick : Effect {
		public override string ToString () {return "Effect - Stick";}
		Web parent;
		Unit target;
		
		public EStick (Source s, Unit u) {
			source = s; target = u;
			parent = (Web)source.Token; 
		}
		public override void Process() {
			Task move = target.Arsenal.Move;
			if (move != default(Task)) {
				parent.Affected.Add(target, move.Aim[0].Range);
				move.Aim[0].Range = 1;
				Mixer.Play(SoundLoader.Effect(EEffect.STICK));
				target.Display.Effect(EEffect.STICK);
			}
		}
	}
}
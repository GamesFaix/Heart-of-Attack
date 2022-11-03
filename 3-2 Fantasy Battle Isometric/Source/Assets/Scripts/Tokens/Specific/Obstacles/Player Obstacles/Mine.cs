using UnityEngine;
using System.Collections.Generic;

namespace HOA.Tokens {

	public class Mine : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Mine (source, template);
		}

		Mine(Source s, bool template=false){
			ID = new ID(this, EToken.MINE, s, false, template);
			TokenType type = this.TokenType;
			type.destructible = true;
			this.TokenType = type;
			Plane = Plane.Sunken;
			Body = new BodySensor9(this, SensorMine.Instantiate);
		}
		public override string Notes () {return "If any Token enters Mine's Cell or a neighboring Cell, destroy Mine.\nWhen Mine is destroyed, do 10 damage to all units in its cell. \nAll units in neighboring cells take 50% damage (rounded down). \nDamage continues to spread outward with 50% reduction until 1. \nDestroy all destructible tokens that would take damage.";}
		
		public override void Die (Source s, bool corpse = false, bool log=true) {
			GameObject.Destroy(Display.gameObject);
			Debug.Log(this+" dying");
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			TokenFactory.Remove(this);

			Body.Exit();
			if (log) {GameLog.Out(s.ToString()+" destroyed "+this+".");}

			EffectQueue.Interrupt(new Effects.Explosion(new Source(this), Body.Cell, 12));

			((BodySensor9)Body).DestroySensors();
		}
	}


}
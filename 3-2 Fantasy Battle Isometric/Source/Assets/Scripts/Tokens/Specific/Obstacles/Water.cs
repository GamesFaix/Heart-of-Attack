namespace HOA {

	public class Water : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Water (source, template);
		}

		Water(Source s, bool template=false){
			ID = new ID(this, EToken.WATR, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodySensor1(this, SensorWater.Instantiate);	
			Neutralize();
		}
		public override string Notes () {return 
			"Ground units must stop on "+ID.Name+"." +
			"\nGround Units sharing "+ID.Name+"'s Cell take 5 damage at the end of their turn.";
		}

		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}

	public class EWaterlog : Effect {
		public override string ToString () {return "Effect - Waterlog";}
		Unit target; int dmg;
		
		public EWaterlog (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.Damage(source, dmg);
			Mixer.Play(SoundLoader.Effect(EEffect.WATERLOG));
			target.Display.Effect(EEffect.WATERLOG);
		}
	}
}
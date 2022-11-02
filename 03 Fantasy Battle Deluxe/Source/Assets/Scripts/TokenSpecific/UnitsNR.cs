using HOA.Tokens.Components;
using HOA.Players;
using HOA.Actions;
using HOA.Map;
using UnityEngine;

namespace HOA.Tokens{
	
	public class Demolitia : Unit {
		public Demolitia(Source s, bool template=false){
			NewLabel(TTYPE.DEMO, s, false, template);
			BuildGround();
			
			health = new HealthDemo(this, 30);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AGrenade("Grenade", Price.Cheap, this, 3, 10));
			arsenal.Sort();
		}
		public override string Notes () {return "Defense +1 per Focus (up to 4).";}
	}
	
	public class MeinSchutz : Unit {
		public MeinSchutz(Source s, bool template=false){
			NewLabel(TTYPE.MEIN, s, false, template);
			BuildGround();
			
			NewHealth(40);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(5)));
			arsenal.Add(new AAttack("Shoot", Price.Cheap, this, Aim.Shoot(2), 12));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.MINE));
			arsenal.Add(new AMeinDetonate(new Price(1,1), this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
	
	public class Panopticannon : Unit {
		public Panopticannon(Source s, bool template=false){
			NewLabel(TTYPE.PANO, s, false, template);
			BuildTrample();
			
			health = new HealthPano(this, 65);
			NewWatch(1);
			
			arsenal.Add(new AMove(this, Aim.MovePath(1)));

			Aim aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 3, 2);
			arsenal.Add(new APanoCannon(Price.Cheap, this, aim, 17));
		
			aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 4, 3);
			arsenal.Add(new APanoPierce(new Price(1,2), this, aim, 20));
			arsenal.Sort();
		}		
		public override string Notes () {return "Defense +1 per Focus (up to 2).";}
	}		
	
	public class Decimatrix : Unit {
		public Decimatrix(Source s, bool template=false){
			NewLabel(TTYPE.DECI, s, true);
			BuildTrample();
			AddKing();
			OnDeath = TTYPE.HSTE;
			
			NewHealth(85,3);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new AAttack("Shoot", Price.Cheap, this, Aim.Shoot(3), 18));
			Aim fireAim = new Aim (AIMTYPE.LINE, TARGET.TOKEN, TTAR.UNITDEST, 2);
			arsenal.Add(new AAttackFir("Flamethrower", new Price(1,1), this, fireAim, 12));
			arsenal.Add(new ADeciFortify(this));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.DEMO));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.MEIN));
			arsenal.Add(new ACreate(new Price(2,2), this, TTYPE.PANO));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
	
	public class Mine : Obstacle {
		public Mine(Source s, bool template=false){
			NewLabel(TTYPE.MINE, s, false, template);
			BuildSunken();
			AddDest();
		}
		public override string Notes () {return "";}

		public override void Die (Source s, bool corpse = false, bool log=true) {
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			TokenFactory.Remove(this);
			Cell oldCell = Cell;
			Exit();
			if (log && !IsSpecial(SPECIAL.HOA)) {GameLog.Out(s.ToString()+" destroyed "+this+".");}
			InputBuffer.Submit(new RExplosion (new Source(this), Cell, 12));

		}
	}
}
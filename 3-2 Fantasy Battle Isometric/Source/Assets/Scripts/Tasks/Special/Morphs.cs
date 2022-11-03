using UnityEngine; 
using System.Collections.Generic;

namespace HOA { 
	public class ADeciFortify : Task {
		
		public override string Desc {get {return "Health +10/10" +
				"\nDefense + 1" +
					"\nAttack range +1" +
						"\nAttack damage +4" +
						"\nForget 'Move'" +
						"\nLearn 'Mortar'";} }
		
		public ADeciFortify (Unit parent) {
			Name = "Fortify";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.MHP, 10));
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.HP, 10));
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.DEF, 1));
			EffectQueue.Add(nextEffects);
			
			Parent.Arsenal.Remove("Move");
			Parent.Arsenal.Replace("Shoot", new AShoot(Parent, 4, 22));
			Parent.Arsenal.Replace("Fortify", new ADeciMobilize(Parent));
			Parent.Arsenal.Add(new ADeciMortar(Parent));
			Parent.Arsenal.Sort();
		}
	}
	public class ADeciMobilize : Task {
		
		public override string Desc {get {return "Health -10/10" +
				"\nDefense -1" +
					"\nAttack range -1" +
						"\nAttack damage -4" +
						"\nLearn 'Move'" +
						"\nForget 'Mortar'";} }
		
		public ADeciMobilize (Unit parent) {
			Name = "Mobilize";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.MHP, -10));
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.HP, -10));
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.DEF, -1));
			EffectQueue.Add(nextEffects);
			
			Parent.Arsenal.Add(new ADeciMove(Parent));
			Parent.Arsenal.Replace("Shoot", new AShoot(Parent, 3, 18));
			Parent.Arsenal.Replace("Mobilize", new ADeciFortify(Parent));
			Parent.Arsenal.Remove("Mortar");
			Parent.Arsenal.Sort();
		}
	}

	public class AGargLand : Task {
		
		public override string Desc {get {return "Becomes trampling ground unit. " +
				"\nMove range -2 " +
					"\nDefense +2" +
						"\nForget 'Create Rook' " +
						"\nLearn 'Tail Whip'";} }
		
		public AGargLand (Unit u) {
			Name = "Land";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		public override bool Restrict () {
			if (!Parent.Body.Cell.Contains(EPlane.GND)) {return false;}
			Token t;
			if (Parent.Body.Cell.Contains(EPlane.GND, out t)) {
				if (t.Special.Is(EType.DEST)) {return false;}
			}
			return true;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t;
			if (Parent.Body.Cell.Contains(EPlane.GND, out t)) {
				if (t.Special.Is(EType.DEST)) {
					EffectQueue.Add(new EDestruct(new Source(Parent), t));
				}
			}
			
			EffectQueue.Add(new EAddStat(new Source(Parent), Parent, EStat.DEF, 2));
			Parent.Plane.Set(EPlane.GND);
			
			Cell cell = Parent.Body.Cell;
			Parent.Body.Exit();
			Parent.Body.Enter(cell);
			
			Parent.Special.Set(new List<EType> {EType.UNIT, EType.KING, EType.TRAM});
			
			Parent.Arsenal.Replace("Move", new AMovePath(Parent, 3));
			Parent.Arsenal.Replace("Land", new AGargFly(Parent));
			Parent.Arsenal.Replace("Create Rook", new AGargTailWhip(Parent));
			Parent.Arsenal.Sort();
			
			Parent.Display.Effect(EEffect.STATUP);
		}
	}
	public class AGargFly : Task {
		
		public override string Desc {get {return "Becomes air unit. " +
				"\nMove range +2" +
					"\nDefense -2" +
						"\nForget 'Tail Whip'" +
						"\nLearn 'Create Rook'";} }
		
		public AGargFly (Unit u) {
			Name = "Take Flight";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
			
		}
		public override bool Restrict () {
			if (Parent.Body.Cell.Contains(EPlane.AIR)) {return true;}
			return false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EAddStat(new Source(Parent), Parent, EStat.DEF, -2));
			Parent.Plane.Set(EPlane.AIR);
			Cell cell = Parent.Body.Cell;
			Parent.Body.Exit();
			Parent.Body.Enter(cell);
			
			Parent.Special.Set(new List<EType> {EType.UNIT, EType.KING, EType.TRAM});
			
			Parent.Arsenal.Replace("Move", new AMovePath(Parent, 5));
			Parent.Arsenal.Replace("Take Flight", new AGargLand(Parent));
			Parent.Arsenal.Replace("Tail Whip", new AGargRook(Parent));
			Parent.Arsenal.Sort();
			
			Parent.Display.Effect(EEffect.STATUP);
		}
	}



}

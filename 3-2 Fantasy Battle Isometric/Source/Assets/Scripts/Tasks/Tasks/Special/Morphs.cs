using UnityEngine; 
using System.Collections.Generic;

namespace HOA.Actions { 

	public class Fortify : Task {
		
		public override string Desc {get {return "Health +10/10" +
				"\nDefense + 1" +
					"\nAttack range +1" +
						"\nAttack damage +4" +
						"\nForget 'Move'" +
						"\nLearn 'Mortar'";} }
		
		public Fortify (Unit parent) {
			Name = "Fortify";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.MHP, 10));
			nextEffects.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.HP, 10));
			nextEffects.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.DEF, 1));
			EffectQueue.Add(nextEffects);
			
			Parent.Arsenal.Remove("Tread");
			Parent.Arsenal.Replace("Shoot", new Shoot(Parent, 4, 22));
			Parent.Arsenal.Replace("Fortify", new Mobilize(Parent));
			Parent.Arsenal.Add(new Mortar(Parent));
			Parent.Arsenal.Sort();
		}
	}
	public class Mobilize : Task {
		
		public override string Desc {get {return "Health -10/10" +
				"\nDefense -1" +
					"\nAttack range -1" +
						"\nAttack damage -4" +
						"\nLearn 'Move'" +
						"\nForget 'Mortar'";} }
		
		public Mobilize (Unit parent) {
			Name = "Mobilize";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.MHP, -10));
			nextEffects.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.HP, -10));
			nextEffects.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.DEF, -1));
			EffectQueue.Add(nextEffects);
			
			Parent.Arsenal.Add(new Tread(Parent));
			Parent.Arsenal.Replace("Shoot", new Shoot(Parent, 3, 18));
			Parent.Arsenal.Replace("Mobilize", new Fortify(Parent));
			Parent.Arsenal.Remove("Mortar");
			Parent.Arsenal.Sort();
		}
	}

	public class Land : Task {
		
		public override string Desc {get {return "Becomes trampling ground unit. " +
				"\nMove range -2 " +
					"\nDefense +2" +
						"\nForget 'Create Rook' " +
						"\nLearn 'Tail Whip'";} }
		
		public Land (Unit u) {
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
				if (t.Special.Is(ESpecial.DEST)) {return false;}
			}
			return true;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Token t;
			if (Parent.Body.Cell.Contains(EPlane.GND, out t)) {
				if (t.Special.Is(ESpecial.DEST)) {
					EffectQueue.Add(new Effects.Destruct(new Source(Parent), t));
				}
			}
			
			EffectQueue.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.DEF, 2));
			Parent.Plane.Set(EPlane.GND);
			
			Cell cell = Parent.Body.Cell;
			Parent.Body.Exit();
			Parent.Body.Enter(cell);
			
			Parent.Special.Set(new List<ESpecial> {ESpecial.UNIT, ESpecial.KING, ESpecial.TRAM});
			
			Parent.Arsenal.Replace("Move", new Move(Parent, 3));
			Parent.Arsenal.Replace("Land", new TakeFlight(Parent));
			Parent.Arsenal.Replace("Build Rook", new TailWhip(Parent));
			Parent.Arsenal.Sort();
			
			Parent.Display.Effect(EEffect.STATUP);
		}
	}
	public class TakeFlight : Task {
		
		public override string Desc {get {return "Becomes air unit. " +
				"\nMove range +2" +
					"\nDefense -2" +
						"\nForget 'Tail Whip'" +
						"\nLearn 'Create Rook'";} }
		
		public TakeFlight (Unit u) {
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
			EffectQueue.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.DEF, -2));
			Parent.Plane.Set(EPlane.AIR);
			Cell cell = Parent.Body.Cell;
			Parent.Body.Exit();
			Parent.Body.Enter(cell);
			
			Parent.Special.Set(new List<ESpecial> {ESpecial.UNIT, ESpecial.KING, ESpecial.TRAM});
			
			Parent.Arsenal.Replace("Move", new Move(Parent, 5));
			Parent.Arsenal.Replace("Take Flight", new Land(Parent));
			Parent.Arsenal.Replace("Tail Whip", new CreateROOK(Parent));
			Parent.Arsenal.Sort();
			
			Parent.Display.Effect(EEffect.STATUP);
		}
	}



}

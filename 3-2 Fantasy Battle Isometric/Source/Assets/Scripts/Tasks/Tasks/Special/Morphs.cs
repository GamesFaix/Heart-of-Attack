using UnityEngine; 
using System.Collections.Generic;

namespace HOA.Actions { 

	public class Fortify : Task {
		
		public override string desc {get {return "Health +10/10" +
				"\nDefense + 1" +
					"\nAttack range +1" +
						"\nAttack damage +4" +
						"\nForget 'Move'" +
						"\nLearn 'Mortar'";} }
		
		public Fortify (Unit parent) : base(parent) {
			name = "Fortify";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new Effects.AddStat(source, parent, EStat.MHP, 10));
			nextEffects.Add(new Effects.AddStat(source, parent, EStat.HP, 10));
			nextEffects.Add(new Effects.AddStat(source, parent, EStat.DEF, 1));
			EffectQueue.Add(nextEffects);
			
			parent.Arsenal.Remove("Tread");
			parent.Arsenal.Replace("Shoot", new Shoot(parent, 4, 22));
			parent.Arsenal.Replace("Fortify", new Mobilize(parent));
			parent.Arsenal.Add(new Mortar(parent));
			parent.Arsenal.Sort();
		}
	}
	public class Mobilize : Task {
		
		public override string desc {get {return "Health -10/10" +
				"\nDefense -1" +
					"\nAttack range -1" +
						"\nAttack damage -4" +
						"\nLearn 'Move'" +
						"\nForget 'Mortar'";} }
		
		public Mobilize (Unit parent) : base(parent) {
			name = "Mobilize";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new Effects.AddStat(source, parent, EStat.MHP, -10));
			nextEffects.Add(new Effects.AddStat(source, parent, EStat.HP, -10));
			nextEffects.Add(new Effects.AddStat(source, parent, EStat.DEF, -1));
			EffectQueue.Add(nextEffects);
			
			parent.Arsenal.Add(new Tread(parent));
			parent.Arsenal.Replace("Shoot", new Shoot(parent, 3, 18));
			parent.Arsenal.Replace("Mobilize", new Fortify(parent));
			parent.Arsenal.Remove("Mortar");
			parent.Arsenal.Sort();
		}
	}

	public class Land : Task {
		
		public override string desc {get {return "Becomes trampling ground unit. " +
				"\nMove range -2 " +
					"\nDefense +2" +
						"\nForget 'Create Rook' " +
						"\nLearn 'Tail Whip'";} }
		
		public Land (Unit parent) : base(parent) {
			name = "Land";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.Self();
		}
		
		public override bool Restrict () {
			Cell cell = parent.Body.Cell;
			TokenGroup ground = (cell.Occupants/Plane.Ground);
			if (ground.Count == 0) {return false;}
			else if (ground.Count > 0) {
				if (ground[0].TokenType.destructible) {return false;}
			}
			return true;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell cell = parent.Body.Cell;
			TokenGroup ground = (cell.Occupants/Plane.Ground);

			if (ground.Count > 0) {
				if (ground[0].TokenType.destructible) {
					EffectQueue.Add(new Effects.Destruct(source, ground[0]));
				}
			}
			
			EffectQueue.Add(new Effects.AddStat(source, parent, EStat.DEF, 2));
			parent.Plane = Plane.Ground;
			TokenType type = parent.TokenType;
			type.trample = true;
			parent.TokenType = type;

			parent.Body.Exit();
			parent.Body.Enter(cell);

			parent.Arsenal.Replace("Move", new Move(parent, 3));
			parent.Arsenal.Replace("Land", new TakeFlight(parent));
			parent.Arsenal.Replace("Build Rook", new TailWhip(parent));
			parent.Arsenal.Sort();
			
			parent.Display.Effect(EEffect.STATUP);
		}
	}
	public class TakeFlight : Task {
		
		public override string desc {get {return "Becomes air unit. " +
				"\nMove range +2" +
					"\nDefense -2" +
						"\nForget 'Tail Whip'" +
						"\nLearn 'Create Rook'";} }
		
		public TakeFlight (Unit parent) : base(parent) {
			name = "Take Flight";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.Self();
			
		}
		public override bool Restrict () {
			Cell cell = parent.Body.Cell;
			if ((cell.Occupants/Plane.Air).Count > 0) {return true;}
			return false;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.AddStat(source, parent, EStat.DEF, -2));
			parent.Plane = Plane.Air;
			TokenType type = parent.TokenType;
			type.trample = false;
			parent.TokenType = type;

			Cell cell = parent.Body.Cell;
			parent.Body.Exit();
			parent.Body.Enter(cell);
			
			parent.Arsenal.Replace("Move", new Move(parent, 5));
			parent.Arsenal.Replace("Take Flight", new Land(parent));
			parent.Arsenal.Replace("Tail Whip", new CreateROOK(parent));
			parent.Arsenal.Sort();
			
			parent.Display.Effect(EEffect.STATUP);
		}
	}



}

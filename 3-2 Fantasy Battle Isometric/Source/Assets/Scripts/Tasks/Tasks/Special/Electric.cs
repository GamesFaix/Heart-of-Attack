using UnityEngine; 

namespace HOA.Actions { 

	public class Shock : Task {
		
		public override string desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget is stunned for "+stun+" turns.";} }
		
		int damage = 10;
		int stun = 5;
		
		public Shock (Unit parent) : base(parent) {
			name = "Shock";
			weight = 3;
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Shock(source, (Unit)targets[0], damage, stun));
		}
	}
	
	public class Discharge : Task {
		
		public override string desc {get {return "Do "+damage+" damage to self, neighbors, and cellmates.  " +
				"\nAll damaged units are stunned for "+stun+" turns.";} }
		
		int damage = 10;
		int stun = 5;
		
		public Discharge (Unit parent) : base(parent) {
			name = "Discharge";
			weight = 4;
			price = new Price(1,2);
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup cellMates = parent.Body.Cell.Occupants;
			TokenGroup neighbors = parent.Body.Cell.Neighbors().Occupants;
			foreach (Token t in neighbors) {cellMates.Add(t);}
			cellMates = cellMates.units;
			
			EffectGroup nextEffects = new EffectGroup();
			foreach (Token t in cellMates) {
				nextEffects.Add(new Effects.Shock(source, (Unit)t, damage, stun));
			}
			EffectQueue.Add(nextEffects);
		}
	}
}

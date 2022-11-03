using UnityEngine; 

namespace HOA.Actions { 

	public class Shock : Task {
		
		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget is stunned for "+stun+" turns.";} }
		
		int damage = 10;
		int stun = 5;
		
		public Shock (Unit parent) {
			Name = "Shock";
			Weight = 3;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Melee());
			Parent = parent;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Shock(new Source(Parent), (Unit)targets[0], damage, stun));
		}
	}
	
	public class Discharge : Task {
		
		public override string Desc {get {return "Do "+damage+" damage to self, neighbors, and cellmates.  " +
				"\nAll damaged units are stunned for "+stun+" turns.";} }
		
		int damage = 10;
		int stun = 5;
		
		public Discharge (Unit parent) {
			Name = "Discharge";
			Weight = 4;
			Price = new Price(1,2);
			NewAim(HOA.Aim.Self());
			Parent = parent;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup cellMates = Parent.Body.Cell.Occupants;
			TokenGroup neighbors = Parent.Body.Cell.Neighbors().Occupants;
			foreach (Token t in neighbors) {cellMates.Add(t);}
			cellMates = cellMates.OnlyType(ESpecial.UNIT);
			
			EffectGroup nextEffects = new EffectGroup();
			foreach (Token t in cellMates) {
				nextEffects.Add(new Effects.Shock(new Source(Parent), (Unit)t, damage, stun));
			}
			EffectQueue.Add(nextEffects);
		}
	}
}

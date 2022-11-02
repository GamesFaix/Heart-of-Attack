using System.Collections.Generic;

namespace HOA{
	public class Piecemaker : Unit {
		public Piecemaker(Source s, bool template=false){
			NewLabel(EToken.PIEC, s, false, template);
			BuildGround();
			
			NewHealth(35,3);
			NewWatch(1); 
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 10));
			arsenal.Add(new ACreate(new Price(1,1), this, EToken.APER, Aim.CreateArc(2)));
			arsenal.Add(new APiecHeal(new Price(0,2), this, 10));
			arsenal.Sort();
			
		}		
		public override string Notes () {return "";}
	}

	public class APiecHeal : Action {
		
		int magnitude;
		
		public APiecHeal (Price p, Unit u, int n) {
			weight = 4;
			actor = u;
			price = p;
			AddAim(new Aim(EAim.ARC, EClass.UNIT, 2));
			magnitude = n;
			
			name = "Heal";
			desc = "Target unit gains "+magnitude+" health.\n(Can target self.)";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			AEffects.AddStat(new Source(actor), (Unit)targets[0], EStat.HP, magnitude);
			Targeter.Reset();
		}
	}
}

using System.Collections.Generic;

namespace HOA{
	public class GrizzlyElder : Unit {
		public GrizzlyElder(Source s, bool template=false){
			NewLabel(EToken.GRIZ, s, false, template);
			BuildGround();
			
			NewHealth(25);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ALeech(Price.Cheap, this, Aim.Melee(), 7));
			arsenal.Add(new ACreate(new Price(1,1), this, EToken.TREE));
			arsenal.Add(new AGrizHeal(new Price(1,1), this, 10));
			arsenal.Sort();
			
		}		
		public override string Notes () {return "";}
	}

	public class AGrizHeal : Action {
		
		int magnitude;
		
		public AGrizHeal (Price p, Unit u, int n) {
			weight = 4;
			actor = u;
			price = p;
			AddAim (new Aim(EAim.NEIGHBOR, EClass.UNIT));
			aim[0].TeamOnly = true;
			aim[0].IncludeSelf = false;
			magnitude = n;
			
			name = "Heal";
			desc = "Target teammate gains "+magnitude+" health.\n(Cannot target self.)";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			InputBuffer.Submit(new RAddStat(new Source(actor), (Unit)targets[0], EStat.HP, magnitude));

		}
	}
}
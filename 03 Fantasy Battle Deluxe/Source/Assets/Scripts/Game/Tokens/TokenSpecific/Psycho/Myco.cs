using System.Collections.Generic;

namespace HOA{
	public class Mycolonist : Unit {
		public Mycolonist(Source s, bool template=false){
			NewLabel(EToken.MYCO, s, false, template);
			BuildGround();
			
			NewHealth(40);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Add(new ACorrode("Spore cloud", new Price(1,0), this, Aim.Arc(2), 12));
			arsenal.Add(new ADonate(new Price(1,0), this, Aim.Melee(), 6));
			arsenal.Add(new AMycoSeed(new Price(1,1), this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
	
	public class AMycoSeed : Action {
		
		Cell cell;
		Token template;
		
		public AMycoSeed (Price p, Unit par) {
			weight = 5;
			price = p;
			actor = par;
			AddAim(new Aim (EAim.ARC, EClass.DEST, 2));
			
			name = "Seed";
			desc = "Replace target non-Remains destructible with Lichenthrope.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			AEffects.Replace(new Source(actor), (Token)targets[0], EToken.LICH);
			Targeter.Reset();
		}
	}
}
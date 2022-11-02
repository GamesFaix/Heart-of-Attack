
namespace HOA{
	public class Mycolonist : Unit {
		public Mycolonist(Source s, bool template=false){
			NewLabel(TTYPE.MYCO, s, false, template);
			BuildGround();
			
			NewHealth(40);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			Aim corrAim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 2);
			arsenal.Add(new ACorrode("Spore cloud", new Price(1,0), this, corrAim, 12));
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
			aim = new Aim (AIMTYPE.ARC, TARGET.TOKEN, TTAR.DEST, 2);
			
			name = "Seed";
			desc = "Replace target non-Remains destructible with Lichenthrope.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RReplace(new Source(actor), default(Token), TTYPE.LICH));
			}
		}
	}
}
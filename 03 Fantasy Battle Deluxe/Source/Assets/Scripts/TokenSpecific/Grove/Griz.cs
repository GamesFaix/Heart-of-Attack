namespace HOA{
	public class GrizzlyElder : Unit {
		public GrizzlyElder(Source s, bool template=false){
			NewLabel(TTYPE.GRIZ, s, false, template);
			BuildGround();
			
			NewHealth(25);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ALeech(Price.Cheap, this, Aim.Melee(), 7));
			arsenal.Add(new ACreate(new Price(1,1), this, TTYPE.TREE));
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
			aim = new Aim(AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.UNIT);
			aim.TeamOnly = true;
			aim.IncludeSelf = false;
			magnitude = n;
			
			name = "Heal";
			desc = "Target teammate gains "+magnitude+" health.\n(Cannot target self.)";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RAddStat(new Source(actor), default(Token), STAT.HP, magnitude));
			}
		}
	}
}
namespace HOA{
	public class Conflagragon : Unit {
		public Conflagragon(Source s, bool template=false){
			NewLabel(TTYPE.CONF, s, false, template);
			BuildAir();
			OnDeath = TTYPE.ASHE;
			
			NewHealth(40);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(6)));
			arsenal.Add(new AAttack("Melee", new Price(0,1), this, Aim.Melee(), 12));
			Aim fireAim = new Aim (AIMTYPE.LINE, TARGET.TOKEN, TTAR.UNITDEST, 3);
			arsenal.Add(new AAttackFir("Firebreathing", new Price(2,0), this, fireAim, 10));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}
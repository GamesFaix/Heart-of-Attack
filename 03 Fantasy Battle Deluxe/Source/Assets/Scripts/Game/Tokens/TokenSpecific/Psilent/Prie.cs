namespace HOA{
	public class PriestOfNaja : Unit {
		public PriestOfNaja(Source s, bool template=false){
			NewLabel(EToken.PRIE, s, false, template);
			BuildGround();
			
			NewHealth(50,2);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 15));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}
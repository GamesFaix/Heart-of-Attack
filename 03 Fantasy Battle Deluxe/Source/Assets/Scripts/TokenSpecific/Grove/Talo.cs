namespace HOA{
	public class TalonedScout : Unit {
		public TalonedScout(Source s, bool template=false){
			NewLabel(TTYPE.TALO, s, false, template);
			BuildAir();
			
			NewHealth(45);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(6)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 10));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}
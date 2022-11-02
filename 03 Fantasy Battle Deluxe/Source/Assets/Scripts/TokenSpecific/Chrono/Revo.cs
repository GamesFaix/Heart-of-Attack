
namespace HOA{
	
	public class RevolvingTom : Unit {
		public RevolvingTom(Source s, bool template=false){
			NewLabel(TTYPE.REVO, s, false, template);
			BuildGround();
			
			NewHealth(30);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AAttack("Shoot", Price.Cheap, this, Aim.Shoot(2), 6));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}
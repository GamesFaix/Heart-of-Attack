
namespace HOA{
	public class MartianManTrap : Unit {
		public MartianManTrap(Source s, bool template=false){
			NewLabel(TTYPE.MART, s, false, template);
			BuildTrample();
			
			NewHealth(70);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}
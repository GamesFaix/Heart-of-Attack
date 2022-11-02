
namespace HOA{
	public class Lichenthrope : Unit {
		public Lichenthrope(Source s, bool template=false){
			NewLabel(TTYPE.LICH, s, false, template);
			BuildGround();
			AddCorpseless();
			
			NewHealth(15);
			NewWatch(5);
			
			arsenal.Add(new AMove(this, Aim.MovePath(0)));
			arsenal.Add(new ALeech(Price.Cheap, this, Aim.Melee(), 5));
			arsenal.Add(new AEvolve(Price.Cheap, this, TTYPE.BEES));
			arsenal.Add(new AEvolve(new Price(1,2), this, TTYPE.MYCO));
			arsenal.Add(new AEvolve(new Price(2,3), this, TTYPE.MART));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}

namespace HOA {
	public class Mawth : Unit {
		public Mawth(Source s, bool template=false){
			NewLabel(EToken.MAWT, s, false, template);
			BuildAir();
			
			NewHealth(55);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MoveLine(4)));
			arsenal.Add(new ALaser("Laser shot", Price.Cheap, this, Aim.Shoot(3), 16));
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
}

namespace HOA{
	public class ArenaNonSensus : Unit {
		public ArenaNonSensus(Source s, bool template=false){
			NewLabel(EToken.AREN, s, false, template);
			BuildEth();
			
			NewHealth(55,3);
			NewWatch(2);
			
			arsenal.Add(new AMove(this, Aim.MovePath(2)));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}

namespace HOA{
	public class ArenaNonSensus : Unit {
		public ArenaNonSensus(Source s, bool template=false){
			NewLabel(EToken.AREN, s, false, template);
			BuildEth();
			ScaleMedium();
			NewHealth(55,3);
			NewWatch(2);
			
			arsenal.Add(new AMovePath(this, 2));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}
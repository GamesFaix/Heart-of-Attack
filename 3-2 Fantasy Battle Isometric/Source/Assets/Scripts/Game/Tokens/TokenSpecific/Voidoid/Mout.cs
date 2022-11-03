namespace HOA{
	public class MouthOfTheUnderworld : Unit {
		public MouthOfTheUnderworld(Source s, bool template=false){
			NewLabel(EToken.MOUT, s, false, template);
			BuildTrample();
			ScaleLarge();
			NewHealth(30);
			NewWatch(4);
			
			arsenal.Add(new ABurrow(this,3));
			arsenal.Add(new AMonoReanimate(new Price(0,1), this));
			arsenal.Add(new ALeech("Taste", Price.Cheap, this, Aim.Arc(3,2), 12));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}

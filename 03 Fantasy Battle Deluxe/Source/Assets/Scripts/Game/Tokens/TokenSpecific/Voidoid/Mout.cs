namespace HOA{
	public class MouthOfTheUnderworld : Unit {
		public MouthOfTheUnderworld(Source s, bool template=false){
			NewLabel(EToken.MOUT, s, false, template);
			BuildGround();
			
			NewHealth(30);
			NewWatch(4);
			
		//	arsenal.Add(new AMove(this, Aim.MovePath(1)));
			arsenal.Add(new AMonoReanimate(Price.Cheap, this));
			arsenal.Add(new ALeech(Price.Cheap, this, Aim.Arc(3,2), 12));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}

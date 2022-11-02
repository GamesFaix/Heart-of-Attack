namespace HOA{
	public class MouthOfTheUnderworld : Unit {
		public MouthOfTheUnderworld(Source s, bool template=false){
			NewLabel(TTYPE.MOUT, s, false, template);
			BuildGround();
			
			NewHealth(30);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MovePath(1)));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}
}

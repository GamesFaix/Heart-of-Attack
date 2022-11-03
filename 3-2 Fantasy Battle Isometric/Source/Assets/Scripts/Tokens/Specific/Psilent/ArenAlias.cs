namespace HOA { 

	public class ArenaNonSensusAlias : Alias {
		public ArenaNonSensusAlias (Token parent){
			Parent = parent;
			Body = new Body(this);
			ScaleMedium();
		}		
	}
}

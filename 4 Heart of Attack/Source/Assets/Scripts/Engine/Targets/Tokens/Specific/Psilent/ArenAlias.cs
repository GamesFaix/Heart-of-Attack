namespace HOA { 

	public class ArenaAlias : Alias {
		public ArenaAlias (Token parent){
			Parent = parent;
			Body = new Body(this);
			ScaleMedium();
		}		
	}
}

namespace HOA {

	public abstract class Request {
		public Source source;
		public virtual void Grant() {}
	}
	public abstract class RCellSelect : Request {
		public Cell cell;
		protected void Reset () {GUISelectors.Reset();}
	}
	public abstract class RInstanceSelect : Request {
		public Token instance;
		protected void Reset () {GUISelectors.Reset();}
	}

}
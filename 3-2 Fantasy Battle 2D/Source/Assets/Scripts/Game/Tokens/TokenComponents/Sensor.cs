
namespace HOA {
	
	public abstract class Sensor {
		protected Token parent;
		protected Cell cell;

		public Cell Cell {get {return cell;} }
		public Token Parent {get {return parent;} }
		
		public abstract void Enter (Cell c);
		public abstract void Exit ();

		public abstract void OtherEnter (Token t);
		public abstract void OtherExit (Token t);
		
		public void Delete () {
			Exit();
			cell.RemoveSensor(this);
		}

		public abstract override string ToString ();
		
	}
}
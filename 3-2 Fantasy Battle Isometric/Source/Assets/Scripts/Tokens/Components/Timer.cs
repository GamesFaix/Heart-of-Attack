namespace HOA {

	public abstract class Timer {

		public string Name {get; protected set;}
		public string Desc {get; protected set;}

		public Source Source {get; protected set;}
		public Unit Parent {get; protected set;}

		public int Turns {get; protected set;}

		public void Tick () {
			Turns--;
			if (Turns == 0) {
				Activate();
				if (Turns == 0) {Parent.timers.Remove(this);}
			}
		}

		public abstract void Activate ();

		public abstract void Display (Panel p, float iconSize);
	}
}
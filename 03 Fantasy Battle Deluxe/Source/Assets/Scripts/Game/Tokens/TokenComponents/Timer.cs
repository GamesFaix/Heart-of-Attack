

namespace HOA {
	public class Timer {

		protected string name;
		public string Name {get {return name;} }

		protected string desc;
		public string Desc {get {return desc;} }


		protected Unit parent;

		protected int turns;
		public int Turns {get {return turns;} }

		public void Tick () {
			turns--;
			if (turns == 0) {
				Activate();
				if (turns == 0) {parent.timers.Remove(this);}
			}
		}

		public virtual void Activate () {}





	}





}
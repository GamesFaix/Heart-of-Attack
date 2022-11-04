using UnityEngine; 

namespace HOA { 

	public abstract class Target {
	
        public virtual TargetClass TargetClass {get; set;}

		public TargetDisplay Display {get; set;}

        bool legal;
		public virtual bool Legal {
			get {return legal;} 
			set {
				legal = value;
				Display.Legal = legal;
			}
		}

        public Target()
        {
            TargetClass = TargetClass.None();
        }

        public bool IsToken(out Token t)
        {
            t = default(Token);
            if (this is Token)
            {
                t = (Token)this;
                return true;
            }
            return false;
        }

        public bool IsCell(out Cell c)
        {
            c = default(Cell);
            if (this is Cell)
            {
                c = (Cell)this;
                return true;
            }
            return false;
        }
	}
}

using System.Collections.Generic;

namespace HOA {
	
	public class TargetGroup : Group<Target>, IEnumerable<Target>{
		public TargetGroup (int capacity=8) {list = new List<Target>(capacity);}
		public TargetGroup (Target t, int capacity=8) {list = new List<Target>(capacity){t};}
		public TargetGroup (IEnumerable<Target> t) {list = new List<Target>(t);}
        
        public void Legalize (bool b=true) {foreach (Target t in list) {t.Legal = b;} }

        public void Add (Cell c) {list.Add(c);} 
		public void Add (IEnumerable<Cell> cg) {foreach (Cell c in cg) {list.Add(c);} }
		public void Add (Token t) {list.Add(t);}
		public void Add (IEnumerable<Token> tg) {foreach (Token t in tg) {list.Add(t);} }

		public void Remove (Cell c) {
			if (list.Contains(c)) {list.Remove(c);}
		}
		public void Remove (IEnumerable<Cell> cg) {
			foreach (Cell c in cg) {
				if (list.Contains(c)) {list.Remove(c);}
			}
		}

		public void Remove (Token t) {
			if (list.Contains(t)) {list.Remove(t);}
		}
		public void Remove (IEnumerable<Token> tg) {
			foreach (Token t in tg) {
				if (list.Contains(t)) {list.Remove(t);}
			}
		}

        public TargetGroup Copy()
        {
            TargetGroup copy = new TargetGroup(Count);
            foreach (Target t in this)
                copy.Add(t);
            return copy;
        }

        public Target[] ToArray()
        {
            Target[] array = new Target[Count];
            for (int i=0; i<Count; i++) 
                array[i] = this[i];
            return array;
        }

        private TargetGroup Filter(TargetFilter filter)
        {
            TargetGroup rejected = Copy();
            TargetGroup accepted = new TargetGroup();

            foreach (FilterTest test in filter.Tests)
            {
                foreach (Target target in rejected)
                {
                    if (test(target)) 
                    {
                        accepted.Add(target);
                        rejected.Mark(target); 
                    }
                }
                rejected.RemoveMarked();
            }
            return accepted;
        }

        public static TargetGroup operator -(TargetGroup a, TargetFilter b) { return a.Filter(b); }

        public TargetGroup Occupants
        {
            get
            {
                TargetGroup occupants = new TargetGroup();
                foreach (Target target in this)
                {
                    if (target is Cell)
                        occupants.Add(((Cell)target).Occupants);
                }
                return occupants;
            }
        }
        public TargetGroup Occupied
        {
            get
            {
                TargetGroup occupied = new TargetGroup();
                foreach (Target target in this)
                {
                    if (target is Token)
                        occupied.Add(((Token)target).Body.Cell);
                }
                return occupied;
            }
        }
    }
}

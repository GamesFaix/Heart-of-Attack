using UnityEngine; 

namespace HOA { 

    public class TargetSet : ListSet<Target> 
    {
        public void Add(CellSet cells)
        {
            foreach (Cell c in cells)
                Add(c as Target);
        }

        public void Add(TokenSet tokens)
        {
            foreach (Token t in tokens)
                Add(t as Target);
        }

        public TargetSet Copy()
        {
            TargetSet copy = new TargetSet();
            foreach (Target t in this) copy.Add(t);
            return copy;
        }

        private TargetSet Filter(TargetFilter filter)
        {
            TargetSet rejected = Copy();
            TargetSet accepted = new TargetSet();

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

        public static TargetSet operator -(TargetSet a, TargetFilter b) { return a.Filter(b); }

        public void Legalize (bool b = true) {
            foreach (Target t in this) 
                t.Legal = b;
        }

    }
}

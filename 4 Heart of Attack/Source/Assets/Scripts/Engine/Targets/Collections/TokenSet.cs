using UnityEngine;
using System.Collections.Generic;

namespace HOA { 

    public class TokenSet : ListSet<Token>, IEnumerable<Token>
    {
        public CellSet Occupied
        {
            get
            {
                CellSet occupied = new CellSet();
                foreach (Token t in list)
                    occupied.Add(t.Body.Cell);
                return occupied;
            }
        }

        public void Add(IEnumerable<Token> collection)
        {
            foreach (Token t in collection)
                Add(t);
        }
    
    
        public static implicit operator TargetSet(TokenSet tokens)
        {
            TargetSet targets = new TargetSet();
            foreach (Token t in tokens)
                targets.Add(t);
            return targets;
        }

        public TokenSet Copy()
        {
            TokenSet copy = new TokenSet();
            foreach (Token t in this) copy.Add(t);
            return copy;
        }

        private TokenSet Filter(TargetFilter filter)
        {
            TokenSet rejected = Copy();
            TokenSet accepted = new TokenSet();

            foreach (FilterTest test in filter.Tests)
            {
                foreach (Token t in rejected)
                {
                    if (test(t))
                    {
                        accepted.Add(t);
                        rejected.Mark(t);
                    }
                }
                rejected.RemoveMarked();
            }
            return accepted;
        }

        public static TokenSet operator -(TokenSet a, TargetFilter b) { return a.Filter(b); }

        public void Legalize(bool b = false)
        {
            foreach (Token t in this)
                t.Legal = b;
        }
    }
}

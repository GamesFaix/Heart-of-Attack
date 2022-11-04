using System.Collections.Generic;
using HOA.Tokens;
using System;
using HOA.Collections;

namespace HOA
{
    /// <summary>
    /// ListSet^Token^ with filtering and copying options.
    /// </summary>
    public class TokenSet : ListSet<Token>, IEnumerable<Token>
    {
        #region //Constructors

        /// <summary>
        /// Extends ListSet constructor. No additional functionality.
        /// </summary>
        public TokenSet(int capacity=4) : base(capacity) { }
        /// <summary>
        /// Extends ListSet constructor. No additional functionality.
        /// </summary>
        public TokenSet(Token t) : base(t) { }
        /// <summary>
        /// Extends ListSet constructor. No additional functionality.
        /// </summary>
        public TokenSet(IEnumerable<Token> t) : base(t) { }

        #endregion
        
        public CellSet Occupied
        {
            get
            {
                CellSet occupied = new CellSet();
                foreach (Token t in list)
                    occupied.Add(t.Cell);
                return occupied;
            }
        }
        
        public TokenSet Copy()
        {
            TokenSet copy = new TokenSet();
            foreach (Token t in this) copy.Add(t);
            return copy;
        }

        private TokenSet Filter(EntityFilter filter)
        {
            TokenSet rejected = Copy();
            TokenSet accepted = new TokenSet();

            foreach (Predicate<IEntity> test in filter.Tests)
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

        public static TokenSet operator -(TokenSet a, EntityFilter b) { return a.Filter(b); }

        public void Legalize(bool b = false)
        {
            foreach (Token t in this)
                t.Legal = b;
        }

        public static TokenSet operator +(TokenSet a, Token b) 
        {
            TokenSet set = a.Copy();
            set.Add(b);
            return set;
        }
        public static TokenSet operator -(TokenSet a, Token b) 
        {
            TokenSet set = a.Copy();
            set.Remove(b);
            return set;
        }

        public static explicit operator EntitySet(TokenSet tokens)
        {
            EntitySet e = new EntitySet();
            foreach (Token t in tokens)
                e.Add(t);
            return e;
        }
    }
}

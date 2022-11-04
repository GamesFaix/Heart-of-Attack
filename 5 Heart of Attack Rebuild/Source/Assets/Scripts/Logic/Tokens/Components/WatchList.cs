using System.Collections.Generic;

namespace HOA.Tokens
{

    public class WatchList : TokenComponent
    {
        Dictionary<Token, object> d;

        public WatchList(Token thisToken)
            : base(thisToken)
        {
            d = new Dictionary<Token, object>(0);
        }

        public void Add(Token t, object o) { d.Add(t, o); }
        public void Remove(Token t) { d.Remove(t); }
        public void Clear() { d.Clear(); }

        public object this[Token t] { get { return d[t]; } }

        public override string ToString() { return ThisToken + "'s WatchList"; }
    }
}

using System.Collections.Generic;
using System.Collections;

namespace HOA.Tokens
{

    public class TrackList : TokenComponent, IEnumerable<Token>
    {
        Dictionary<Token, object> d;

        public TrackList(Token self)
            : base(self)
        {
            d = new Dictionary<Token, object>(0);
        }

        public void Add(Token t, object o) { d.Add(t, o); }
        public void Remove(Token t) { d.Remove(t); }
        public void Clear() { d.Clear(); }

        public object this[Token t] { get { return d[t]; } }

        public bool Empty { get { return d.Count < 1; } }

        public override string ToString() { return self + "'s TrackList"; }

        public IEnumerator<Token> GetEnumerator() { return GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return d.GetEnumerator(); }
    }
}

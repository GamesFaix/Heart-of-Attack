using System.Collections.Generic;
using System.Collections;

namespace HOA.Tokens
{
    public abstract partial class Token
    {

        internal class TrackList : IEnumerable<Token>
        {
            readonly Token self;
            readonly Dictionary<Token, object> tracked;

            public TrackList(Token self)
            {
                this.self = self;
                tracked = new Dictionary<Token, object>(0);
            }

            public void Add(Token t, object o) { tracked.Add(t, o); }
            public void Remove(Token t) { tracked.Remove(t); }
            public void Clear() { tracked.Clear(); }

            public object this[Token t] { get { return tracked[t]; } }

            public bool Empty { get { return tracked.Count < 1; } }

            public override string ToString() { return self + "'s TrackList"; }

            public IEnumerator<Token> GetEnumerator() { return GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() { return tracked.GetEnumerator(); }
        }
    }
}

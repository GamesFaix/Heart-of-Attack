using UnityEngine;

namespace HOA
{

    public abstract class TokenComponent
    {
        public Token ThisToken { get; protected set; }
        public Unit ThisUnit { get { return ThisToken as Unit; } }
        public abstract override string ToString();

        public TokenComponent(Token thisToken)
        {
            ThisToken = thisToken;
        }
    }
}

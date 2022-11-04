namespace HOA.Tokens
{

    public abstract class TokenComponent
    {
        public Token self { get; private set; }
        public Unit selfUnit { get { return self as Unit; } }
        public abstract override string ToString();

        public TokenComponent(Token self)
        {
            this.self = self;
        }
    }
}

using UnityEngine; 

namespace HOA { 

    public class TokenRecord {

        public Token Token { get; protected set; }
        public Unit Unit
        {
            get { return ( (Token is Unit) ? (Unit)Token : default(Unit));}
        }

        public TokenRecord(Token token)
        {
            Token = token;
        }

    }

    public class CarapaceShieldRecord : TokenRecord
    {
        public HealthCaraShield Shield { get; private set; }
       
        public CarapaceShieldRecord(Token token, HealthCaraShield shield)
            : base(token)
        {
            Shield = shield;
        }
    }

    public class WebRecord : TokenRecord
    {
        public int RangeLost { get; private set; }
        public WebRecord(Token token, int rangeLost)
            : base(token)
        {
            RangeLost = rangeLost;
        }
    }
}

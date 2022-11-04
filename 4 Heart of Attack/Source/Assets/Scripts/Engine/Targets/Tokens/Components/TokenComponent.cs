using UnityEngine; 

namespace HOA { 

    public abstract class TokenComponent : IInspectable{

        public Token Parent { get; protected set; }
        
        public TokenComponent(Token parent)
        {
            Parent = parent;
        }

        public abstract void Draw(Panel panel);

        public override abstract string ToString();
    }
}

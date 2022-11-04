using UnityEngine; 

namespace HOA { 

    public abstract class TokenComponent : IInspectable{

        public Token Parent { get; protected set; }
        
        public abstract void Draw(Panel panel);

        public TokenComponent(Token parent)
        {
            Parent = parent;
        }
    
    
    }
}

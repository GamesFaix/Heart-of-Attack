using System;

namespace HOA.Tokens
{
    public abstract partial class Token : ISourced, ISourceRestricted
    {
        public Source source 
        { 
            get; 
            private set; 
        }
        
        public Type[] validSources 
        {
            get 
            { 
                return new Type[3] 
                { 
                    typeof(Player), 
                    typeof(Effects.Effect), 
                    typeof(Source.ForcedSource) 
                }; 
            } 
        }
        
        public bool IsValidSource(object obj) 
        { 
            return Source.IsValid(validSources, obj); 
        }
    }
}
using System;
using System.Collections.Generic;
using HOA.Collections;

namespace HOA.Effects
{
    public delegate IEffect EffectBuilder(object source, EffectArgs args);

    public partial class Effect : IEffect, ISourced, ISourceRestricted
    {
        public string Name { get; private set; }
        public override string ToString() { return Name; }
        public EffectArgs args;
        public Action<EffectArgs> action;

        public Action Process 
        { 
            get 
            {
                Log.Debug("Processing {0}.", Name);
                return () => action(args);
            } 
        }
        
        
        public Sequence Sequence { get; set; }

        private Effect(object source, string name, EffectArgs args)
        {
            if (!IsValidSource(source))
                throw new InvalidSourceException();
            this.source = new Source(source);

            if (name == "" || args == null)
                throw new ArgumentNullException();
            
            Name = name;
            this.args = args;
        }

        #region Sources

        public Source source { get; private set; }
        public Type[] validSources
        {
            get
            {
                return new Type[10]
                {
                    typeof(Abilities.Ability), 
                    typeof(Effect),
                    typeof(Sequence),
                    typeof(Set<Effect>),
                    typeof(Tokens.Timer), 
                    typeof(Tokens.Sensor),
                    typeof(Tokens.Token),
                    typeof(Tokens.Unit),
                    typeof(Tokens.Obstacle),
                    typeof(Tokens.Terrain)
                };
            }
        }
        public bool IsValidSource(object obj) { return Source.IsValid(validSources, obj); }

        #endregion

    }
}

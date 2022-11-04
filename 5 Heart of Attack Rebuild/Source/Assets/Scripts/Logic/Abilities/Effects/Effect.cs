using System;
using System.Collections.Generic;

namespace HOA.Abilities
{
    public delegate IEffect EffectConstructor (object source, EffectArgs args);

    public partial class Effect : ClosedAction<EffectArgs>, IEffect, ISourced, ISourceRestricted
    {
        public string Name { get; private set; }
        public override string ToString() { return Name; }

        public Action Process { get { return Invoke; } }
        
        
        public EffectSequence Sequence { get; set; }

        private Effect(object source, string name, EffectArgs args)
            : base (args)
        {
            if (!IsValidSource(source))
                throw new InvalidSourceException();
            this.source = new Source(source);

            if (name == "" || args == null)
                throw new ArgumentNullException();
            Name = name;
        }

        #region Sources

        public Source source { get; private set; }
        public Type[] validSources
        {
            get
            {
                return new Type[10]
                {
                    typeof(Ability), 
                    typeof(Effect),
                    typeof(EffectSequence),
                    typeof(Set<Effect>),
                    typeof(Tokens.Timer), 
                    typeof(Tokens.Sensor),
                    typeof(Token),
                    typeof(Unit),
                    typeof(Obstacle),
                    typeof(Terrain)
                };
            }
        }
        public bool IsValidSource(object obj) { return Source.IsValid(validSources, obj); }

        #endregion

    }
}

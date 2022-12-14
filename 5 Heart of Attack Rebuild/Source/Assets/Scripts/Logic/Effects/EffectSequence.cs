using System;
using System.Collections.Generic;
using HOA.Collections;

namespace HOA.Effects
{
    public partial class Sequence : NestedList<Effect>, IEffect, ISourced, ISourceRestricted
    {
        #region Properties

        public string Name { get; private set; }
        public EffectArgs Args { get; private set; }
        
        #endregion

        #region Constructors

        private Sequence(object source, string name, EffectArgs args)
        {
            if (!IsValidSource(source))
                throw new InvalidSourceException();
            this.source = new Source(source); 
            
            if (name == "" || args == null)
                throw new ArgumentNullException();
            Name = name;
            Args = args;
            mainList = new List<IList<Effect>>();
        }

        private Sequence(object source, string name, EffectArgs args, Effect e)
            : this(source, name, args)
        {
            AddToEnd(e);
        }

        private Sequence(object source, string name, EffectArgs args, EffectSet e)
            : this(source, name, args)
        {
            AddToEnd(e);
        }

        #endregion

        public override string ToString() { return Name; }

        public Action Process
        {
            get
            {
                return () => 
                {
                    if (mainList.Count > 0)
                    {
                        ((EffectSet)first).Process();
                        mainList.Remove(first);
                    }
                };
            }
        }

        public override void AddToEnd(Effect e)
        {
            e.Sequence = this;
            AddToEnd(e);
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

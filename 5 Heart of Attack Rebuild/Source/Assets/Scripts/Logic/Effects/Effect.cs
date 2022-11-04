using System;
using System.Collections.Generic;

namespace HOA.Ef
{
    public delegate IEffect Builder (object source, Args args);

    public partial class Effect : IEffect, ISourced, ISourceRestricted
    {
        public string Name { get; private set; }
        public override string ToString() { return Name; }
        public Args args;
        public Action<Args> action;

        public Action Process 
        { 
            get 
            {
                Log.Debug("Processing {0}.", Name);
                return () => action(args);
            } 
        }
        
        
        public Sequence Sequence { get; set; }

        private Effect(object source, string name, Args args)
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
                    typeof(Ab.Ability), 
                    typeof(Effect),
                    typeof(Ef.Sequence),
                    typeof(Set<Effect>),
                    typeof(To.Timer), 
                    typeof(To.Sensor),
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

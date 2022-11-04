using System;
using System.Collections.Generic;


namespace HOA.Abilities
{
    public partial class EffectSequence : NestedList<Effect>, IEffect
    {
        #region Properties

        public string Name { get; private set; }
        public IEffectUser User { get; private set; }
        public EffectArgs Args { get; private set; }
        
        #endregion

        #region Constructors

        private EffectSequence(string name, IEffectUser user, EffectArgs args)
        {
            if (name == "" || user == null || args == null)
                throw new ArgumentNullException();
            Name = name;
            User = user;
            Args = args;
            mainList = new List<IList<Effect>>();
        }

        private EffectSequence(string name, IEffectUser user, EffectArgs args, Effect e)
            : this(name, user, args)
        {
            AddToEnd(e);
        }

        private EffectSequence(string name, IEffectUser user, EffectArgs args, EffectSet e)
            : this(name, user, args)
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




    }
}

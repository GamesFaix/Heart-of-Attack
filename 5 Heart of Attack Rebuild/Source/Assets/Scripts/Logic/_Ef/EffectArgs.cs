using System;
using System.Collections.Generic;
using System.Linq;
using HOA.To;
using HOA.Fargo;

namespace HOA.Ef
{
    public class EffectArgs : ClosureArgs
    {
        #region Properties

        ArgTable<FT, IEntity> targets;
        ArgTable<FN, sbyte> nums;
        ArgTable<FX, string> labels;
        ArgTable<FO, bool> options;
        ArgTable<FE, Ef.Builder> effects;

        public Set<IEntity> targetBatch { get; private set; }
        public Func<string> desc { get; private set; }
        public Species species { get; private set; }
        public Plane plane { get; private set; }
        public Player player { get; private set; }
        public Ab.AbilityClosure ability { get; private set; }
        public TokenComponent component { get; set; }

        #endregion

        #region Constructors

        private EffectArgs()
        {
            targetBatch = null;
            targets = ArgTable.Target();
            nums = ArgTable.Num();
            labels = ArgTable.Text();
            options = ArgTable.Option();
            effects = ArgTable.Effect();
            species = Species.None;
            plane = Plane.None;
            desc = null;
            player = null;
            ability = null;
            component = null;
        }

        public EffectArgs(params Arg<FT, IEntity>[] targets) 
        : this()
        {
            foreach (Arg<FT, IEntity> arg in targets)
                this.targets.Add(arg);
        }

        public EffectArgs(Arg<FT, IEntity> target, params Arg<FN, sbyte>[] values)
            : this(new Arg<FT, IEntity>[1] {target})
        { 
            foreach (Arg<FN, sbyte> arg in values)
                nums.Add(arg); 
        }

        public EffectArgs(Arg<FT, IEntity> target, Arg<FN, sbyte> value, Arg<FX, string> label)
            : this(target, value)
        { labels.Add(label); }

        public EffectArgs(Set<IEntity> targetBatch, Arg<FN, sbyte> value)
            : this()
        {
            nums.Add(value);
            this.targetBatch = targetBatch;
        }

        public EffectArgs(Arg<FT, IEntity> target, Arg<FN, sbyte> value, Arg<FO, bool> option)
            : this(target, value)
        { options.Add(option); }

        public EffectArgs(Arg<FT, IEntity> target, Ab.AbilityClosure ability)
            : this(target)
        { this.ability = ability; }

        public EffectArgs(Arg<FT, IEntity> target, TokenComponent component)
            : this(target)
        { this.component = component; }

        public EffectArgs(Arg<FT, IEntity> target, Species species, Arg<FE, Ef.Builder> effect = null)
            : this(target)
        { 
            this.species = species;
            if (effect != null)
                effects.Add(effect);
        }

        public EffectArgs(Arg<FT, IEntity> target, Player player)
            : this(target)
        { this.player = player; }
        
        #endregion

        #region Indexers

        public IEntity this[FT fTarget] { get { return targets[fTarget]; } }
        public sbyte this[FN fNum] { get { return nums[fNum]; } }
        public string this[FX fText] { get { return labels[fText]; } }
        public bool this[FO fOption] { get { return options[fOption]; } }
        public Ef.Builder this[FE fEffect] { get { return effects[fEffect]; } }

        #endregion

        public bool Contains(FT fTarget) { return targets.Contains(fTarget); }
        public bool Contains(FN fNum) { return nums.Contains(fNum); }
        public bool Contains(FX fText) { return labels.Contains(fText); }
        public bool Contains(FO fOption) { return options.Contains(fOption); }
        public bool Contains(FE fEffect) { return effects.Contains(fEffect); }
    }
}
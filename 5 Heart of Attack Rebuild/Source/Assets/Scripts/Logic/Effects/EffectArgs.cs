using System;
using System.Collections.Generic;
using System.Linq;
using HOA.Tokens;
using HOA.Args;
using HOA.Collections;
using Farg = HOA.Args.Arg;

namespace HOA.Effects
{
    public class EffectArgs
    {
        #region Properties

        ArgTable<RT, IEntity> targets;
        ArgTable<RN, sbyte> nums;
        ArgTable<RL, string> labels;
        ArgTable<RO, bool> options;
        ArgTable<RE, EffectBuilder> effects;

        public Set<IEntity> targetBatch { get; private set; }
        public Func<string> desc { get; private set; }
        public Species species { get; private set; }
        public Plane plane { get; private set; }
        public Player player { get; private set; }
        public Abilities.AbilityClosure ability { get; private set; }
        public Timer timer { get; set; }

        #endregion

        #region Constructors

        private EffectArgs()
        {
            targetBatch = null;
            targets = ArgTable.Target();
            nums = Args.ArgTable.Num();
            labels = Args.ArgTable.Text();
            options = Args.ArgTable.Option();
            effects = ArgTable.Effect();
            species = Species.None;
            plane = Plane.None;
            desc = null;
            player = null;
            ability = null;
            timer = null;
        }

        public EffectArgs(params Arg<RT, IEntity>[] targets) 
        : this()
        {
            foreach (Arg<RT, IEntity> arg in targets)
                this.targets.Add(arg);
        }

        public EffectArgs(Arg<RT, IEntity> target, params Arg<RN, sbyte>[] values)
            : this(new Arg<RT, IEntity>[1] {target})
        { 
            foreach (Arg<RN, sbyte> arg in values)
                nums.Add(arg); 
        }

        public EffectArgs(Arg<RT, IEntity> target, Arg<RN, sbyte> value, Arg<RL, string> label)
            : this(target, value)
        { labels.Add(label); }

        public EffectArgs(Set<IEntity> targetBatch, Arg<RN, sbyte> value)
            : this()
        {
            nums.Add(value);
            this.targetBatch = targetBatch;
        }

        public EffectArgs(Arg<RT, IEntity> target, Arg<RN, sbyte> value, Arg<RO, bool> option)
            : this(target, value)
        { options.Add(option); }

        public EffectArgs(Arg<RT, IEntity> target, Abilities.AbilityClosure ability)
            : this(target)
        { this.ability = ability; }

        public EffectArgs(Arg<RT, IEntity> target, Timer timer)
            : this(target)
        { this.timer = timer; }

        public EffectArgs(Arg<RT, IEntity> target, Species species, Arg<RE, EffectBuilder> effect = null)
            : this(target)
        { 
            this.species = species;
            if (effect != null)
                effects.Add(effect);
        }

        public EffectArgs(Arg<RT, IEntity> target, Player player)
            : this(target)
        { this.player = player; }
        
        #endregion

        #region Indexers

        public IEntity this[RT target] { get { return targets[target]; } }
        public sbyte this[RN number] { get { return nums[number]; } }
        public string this[RL label] { get { return labels[label]; } }
        public bool this[RO option] { get { return options[option]; } }
        public EffectBuilder this[RE effect]
        {
            get
            {
                return effects.Contains(effect) ? effects[effect] : null;
            }
        }

        #endregion

        public bool Contains(RT fTarget) { return targets.Contains(fTarget); }
        public bool Contains(RN fNum) { return nums.Contains(fNum); }
        public bool Contains(RL fText) { return labels.Contains(fText); }
        public bool Contains(RO fOption) { return options.Contains(fOption); }
        public bool Contains(RE fEffect) { return effects.Contains(fEffect); }
    }
}
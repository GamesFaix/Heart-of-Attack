using System;
using System.Collections.Generic;
using System.Linq;
using HOA.Tokens;


namespace HOA.Abilities
{
    public class EffectArgs
    {
        #region Properties

        public Func<string> desc { get; private set; }
        public Set<IEntity> targets { get; private set; }
        public int[] values { get; private set; }
        public bool[] options { get; private set; }
        //
        public Species species { get; private set; }
        public Stats stat { get; private set; }
        public Plane plane { get; private set; }
        public Player player { get; private set; }
        public Ability ability { get; private set; }
        public TokenComponent component { get; private set; }

        public Set<Cell> cells { get { return new Set<Cell>(targets.OfType<Cell>()); } }
        public Set<Token> tokens { get { return new Set<Token>(targets.OfType<Token>()); } }
        public Cell cell { get { return targets.SingleOrDefault<IEntity>(Filter.Cell.ToFunc()) as Cell; } }
        public Token token { get { return targets.SingleOrDefault<IEntity>(Filter.Token.ToFunc()) as Token; } }
        public Unit unit { get { return targets.SingleOrDefault<IEntity>(Filter.Unit.ToFunc()) as Unit; } }


        public int value { get { return values.Single<int>(); } }
        public bool option { get { return options.Single<bool>(); } }
        #endregion

        #region Constructors

        public EffectArgs(Func<string> desc, Set<IEntity> targets, int[] values, bool[] options,
            Species species, Stats stat, Plane plane = Plane.None, Player player = null)
        {
            this.desc = desc;
            this.targets = targets;
            this.values = values;
            this.options = options;
            this.species = species;
            this.stat = stat;
            this.plane = plane;
            this.player = player;
        }

        public EffectArgs(IEntity target) { targets = new Set<IEntity>(1){target}; }

        public EffectArgs(IEntity target, int value)
            : this(target)
        {
            values = new int[1] { value };
        }

        public EffectArgs(params IEntity[] targets)
        {
            this.targets = new Set<IEntity>(targets);
        }

        public EffectArgs(Set<IEntity> targets, int[] values)
        {
            this.targets = targets;
            this.values = values;
        }

        public EffectArgs(IEntity target, int value, bool option)
        : this(target, value)
        {
            options = new bool[1] { option };
        }

        public EffectArgs(IEntity target, int value, Stats stat)
            : this(target, value)
        {
            this.stat = stat;
        }

        public EffectArgs(IEntity target, Ability ability)
            : this(target)
        {
            this.ability = ability;
        }

        public EffectArgs(IEntity target, TokenComponent component)
            : this(target)
        {
            this.component = component;
        }

        public EffectArgs(IEntity target, Species species)
            : this(target)
        {
            this.species = species;
        }

        #endregion

        public EffectArgs Copy()
        {
            return new EffectArgs(desc, targets, values, options, 
                species, stat, plane, player);
        }

    }
}
using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA.Abilities
{
    public class EffectArgs
    {
        #region Properties

        public Func<string> desc { get; private set; }
        public EntitySet targets { get; private set; }
        public int[] values { get; private set; }
        public bool[] options { get; private set; }
        //
        public Species species { get; private set; }
        public Stats stat { get; private set; }
        public Plane plane { get; private set; }
        public Player player { get; private set; }
        public Ability ability { get; private set; }
        public TokenComponent component { get; private set; }

        public CellSet cells { get { return targets.Cells; } }
        public TokenSet tokens { get { return targets.Tokens; } }


        public Cell cell 
        { 
            get 
            {
                if (cells.Count == 1)
                    return cells[0];
                else if (cells.Count > 1)
                    throw new AmbiguousReferenceException();
                else
                    throw new NullReferenceException();
            } 
        }

        public Token token
        {
            get
            {
                if (tokens.Count == 1)
                    return tokens[0];
                else if (tokens.Count > 1)
                    throw new AmbiguousReferenceException();
                else
                    throw new NullReferenceException();
            }
        }
        public Unit unit
        {
            get
            {
                TokenSet set = tokens - EntityFilter.Unit;
                if (set.Count == 1)
                    return set[0] as Unit;
                else if (set.Count > 1)
                    throw new AmbiguousReferenceException();
                else
                    throw new NullReferenceException();
            }
        }

        public int value
        {
            get
            {
                if (values.Length == 1)
                    return values[0];
                else if (values.Length > 1)
                    throw new AmbiguousReferenceException();
                else
                    throw new NullReferenceException();
            }
        }
        public bool option
        {
            get
            {
                if (options.Length == 1)
                    return options[0];
                else if (options.Length > 1)
                    throw new AmbiguousReferenceException();
                else
                    throw new NullReferenceException();
            }
        }

        #endregion

        #region Constructors

        public EffectArgs(Func<string> desc, EntitySet targets, int[] values, bool[] options,
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

        public EffectArgs(IEntity target) { targets = new EntitySet(target, 1); }

        public EffectArgs(IEntity target, int value)
            : this(target)
        {
            values = new int[1] { value };
        }

        public EffectArgs(params IEntity[] targets)
        {
            this.targets = new EntitySet(targets);
        }

        public EffectArgs(EntitySet targets, int[] values)
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
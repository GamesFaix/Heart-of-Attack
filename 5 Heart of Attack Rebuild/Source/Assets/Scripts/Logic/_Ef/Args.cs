using System;
using System.Collections.Generic;
using System.Linq;
using HOA.To;


namespace HOA.Ef
{
    public class Args : ClosureArgs
    {
        #region Properties

        public Func<string> desc { get; private set; }
        public Set<IEntity> targets { get; private set; }
        public Dictionary<string, sbyte> val { get; private set; }
        public Dictionary<string, bool> opt { get; private set; }
        public Dictionary<string, string> str { get; private set; }

        public Species species { get; private set; }
        public Plane plane { get; private set; }
        public Player player { get; private set; }
        public Ab.Closure ability { get; private set; }
        public Builder builder { get; private set; }
        public TokenComponent component { get; set; }

        public Set<Cell> cells { get { return new Set<Cell>(targets.OfType<Cell>()); } }
        public Set<Token> tokens { get { return new Set<Token>(targets.OfType<Token>()); } }
        public Cell cell { get { return targets.SingleOrDefault<IEntity>(Filter.Cell.ToFunc()) as Cell; } }
        public Token token { get { return targets.SingleOrDefault<IEntity>(Filter.Token.ToFunc()) as Token; } }
        public Unit unit { get { return targets.SingleOrDefault<IEntity>(Filter.Unit.ToFunc()) as Unit; } }


        #endregion

        #region Constructors

        public Args(Func<string> desc, Set<IEntity> targets, 
            Dictionary<string, sbyte> values, 
            Dictionary<string, bool> options,
            Species species, Dictionary<string, string> labels, Plane plane = Plane.None, Player player = null)
        {
            this.desc = desc;
            this.targets = targets;
            val = values;
            opt = options;
            this.species = species;
            str = labels;
            this.plane = plane;
            this.player = player;
        }

        public Args() { }

        public Args(params IEntity[] targets) { this.targets = new Set<IEntity>(targets); }
        public Args(Set<IEntity> targets) { this.targets = new Set<IEntity>(targets); }

        public Args(Set<IEntity> targets, Dictionary<string, sbyte> val)
            : this(targets)
        { this.val = val; }

        public Args(IEntity target, string valueName, sbyte value)
            : this(target)
        {
            val = new Dictionary<string, sbyte>();
            val.Add(valueName, value);
        }

        public Args(IEntity target, string valueName, St.Stat value)
            : this(target)
        {
            val = new Dictionary<string, sbyte>();
            val.Add(valueName, (sbyte)value);
        }

        public Args(IEntity target, string valueName, sbyte value, string labelName, string label)
            : this(target, valueName, value)
        {
            str = new Dictionary<string, string>();
            str.Add(labelName, label);
        }

        public Args(IEntity target, string valueName, sbyte value, string optionName, bool option)
            : this(target, valueName, value)
        {
            opt = new Dictionary<string, bool>();
            opt.Add(optionName, option);
        }

        public Args(IEntity target, Ab.Closure ability)
            : this((Set<IEntity>)target)
        { this.ability = ability; }
        
        public Args(IEntity target, TokenComponent component)
            : this((Set<IEntity>)target)
        { this.component = component; }

        public Args(IEntity target, Species species)
            : this((Set<IEntity>)target)
        { this.species = species; }

        public Args(IEntity target, Player player)
            : this((Set<IEntity>)target)
        { this.player = player; }

        public Args(IEntity target, Species species, Builder builder)
            : this(target, species)
        { this.builder = builder; }
        
        #endregion

        public Args Copy()
        {
            return new Args(desc, targets, val, opt, 
                species, str, plane, player);
        }

    }
}
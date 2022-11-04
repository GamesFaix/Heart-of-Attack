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
        public int[] values { get; private set; }
        public bool[] options { get; private set; }
        //
        public Species species { get; private set; }
        public Stats stat { get; private set; }
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


        public int value { get { return values.Single<int>(); } }
        public bool option { get { return options.Single<bool>(); } }
        #endregion

        #region Constructors

        public Args(Func<string> desc, Set<IEntity> targets, int[] values, bool[] options,
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

        public Args(IEntity target) { targets = new Set<IEntity>(1){target}; }

        public Args(IEntity target, int value)
            : this(target)
        { values = new int[1] { value }; }

        public Args(params IEntity[] targets)
        {this.targets = new Set<IEntity>(targets);}

        public Args(Set<IEntity> targets, int[] values)
        {
            this.targets = targets;
            this.values = values;
        }

        public Args(IEntity target, int value, bool option)
            : this(target, value)
        { options = new bool[1] { option }; }

        public Args(IEntity target, int value, Stats stat)
            : this(target, value)
        { this.stat = stat; }

        public Args(IEntity target, Ab.Closure ability)
            : this(target)
        { this.ability = ability; }

        public Args(IEntity target, TokenComponent component)
            : this(target)
        { this.component = component; }

        public Args(IEntity target, Species species)
            : this(target)
        { this.species = species; }

        public Args(IEntity target, Player player)
            : this(target)
        { this.player = player; }

        public Args(IEntity target, Species species, Builder builder)
            : this(target, species)
        { this.builder = builder; }

        #endregion

        public Args Copy()
        {
            return new Args(desc, targets, values, options, 
                species, stat, plane, player);
        }

    }
}
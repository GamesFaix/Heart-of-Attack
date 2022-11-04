using System;
using System.Collections.Generic;

namespace HOA.Ab
{
	public class Closure : Closure<AbilityArgs>, 
        IComparable<Closure>, IEquatable<Closure>
    {
        public Ability ability { get { return functor as Ability; } }

        
        public Source source { get; private set; }
        public Token sourceToken { get { return source.Last<Token>(); } }
        public Unit sourceUnit { get { return source.Last<Unit>(); } }
        public Player sourcePlayer { get { return source.Last<Player>(); } }
        
        public bool usedThisTurn { get; private set; }

        
        public string name { get { return ability.name; } }
        public Rank rank { get { return ability.rank; } }
        public Price price { get { return args.price; } }
        public Description desc { get; private set; }

        public Closure(object source, Ability ability, AbilityArgs args, Description desc = null)
            : base(ability, args)
        {
            this.source = new Source(source);
            usedThisTurn = false;
            this.desc = desc;
          //  Log.Debug("Ab.Closure created. {0}: {1} ({2}): {3}", args.user, name, rank, args.price);
        }

        public bool Usable(out string message)
        {
            return ability.Usable(this, out message);
        }

        public void Update()
        {
            ability.Update(ability, args);
        }

        public void Charge()
        {
            Unit u;
            if (source.Last<Unit>(out u))
            {
                usedThisTurn = true;
                u.Charge(args.price);
                Log.Debug("{0} charged to {1}.", args.price, u);
            }
        }

        public void Reset()
        {
            usedThisTurn = false;
        }

        public void Execute(NestedList<IEntity> targets)
        {
            Log.Debug("Executing {0} ({1}).", name, args);
            if (sourceUnit != null)
                Charge();
            ability.Execute(this, targets);
        }

        public IEnumerator<AimStage> GetEnumerator()
        {
            return ability.Aims.GetEnumerator();
        }

        #region IComparable & IEquatable

        /// <summary> Compare by Rank, then Price, then Name </summary>
        public int CompareTo(Closure other)
        {
            if (rank < other.rank)
                return -1;
            else if (rank > other.rank)
                return 1;
            else
            {
                int i = price.CompareTo(other.price);
                if (i != 0)
                    return i;
                else
                    return (name.CompareTo(other.name));
            }
        }

        public bool Equals(Closure other)
        {
            if (other as object == null)
                return false;
            return (name == other.name && ability == other.ability);
        }

        public override bool Equals(object other)
        {
            return (other is Closure && (other as Closure).Equals(this));
        }

        public override int GetHashCode()
        {
            Log.Debug("No custom implementation.");
            return base.GetHashCode();
        }

        public static bool operator ==(Closure a, Closure b) { return a.Equals(b); }
        public static bool operator !=(Closure a, Closure b) { return !a.Equals(b); }

        #endregion
	}
}
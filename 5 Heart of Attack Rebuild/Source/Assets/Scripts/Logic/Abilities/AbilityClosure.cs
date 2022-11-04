using System;
using System.Collections.Generic;
using HOA.Collections;
using HOA.Args;
using HOA.Stats;
using Token = HOA.Tokens.Token;
using Unit = HOA.Tokens.Unit;

namespace HOA.Abilities
{
    public class AbilityClosure : IComparable<AbilityClosure>, IEquatable<AbilityClosure>
    {
        public readonly Source source;
        public readonly Ability ability;
        public readonly AbilityArgs args;

        public Token sourceToken { get { return source.Last<Token>(); } }
        public Unit sourceUnit { get { return source.Last<Unit>(); } }
        public Player sourcePlayer { get { return source.Last<Player>(); } }
        
        public bool usedThisTurn { get; private set; }

        public string name { get { return ability.name; } }
        public AbilityRank rank { get { return ability.rank; } }
        public Price price
        {
            get
            {
                Twin price = args[RS.Price] as Twin;
                return new Price(price[0], price[1]);
            }
        }
         
        public Description desc { get; private set; }

        public AbilityClosure(object source, Ability ability, AbilityArgs args, Description desc = null)
        {
            this.source = new Source(source);
            this.ability = ability;
            this.args = args;
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
            ability.Update(this);
        }

        public void Charge()
        {
            Unit u;
            if (source.Last<Unit>(out u))
            {
                usedThisTurn = true;
                u.Charge(price);
                Log.Debug("{0} charged to {1}.", price, u);
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
        public int CompareTo(AbilityClosure other)
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

        public bool Equals(AbilityClosure other)
        {
            if (other as object == null)
                return false;
            return (name == other.name && ability == other.ability);
        }

        public override bool Equals(object other)
        {
            return (other is AbilityClosure && (other as AbilityClosure).Equals(this));
        }

        public override int GetHashCode()
        {
            Log.Debug("No custom implementation.");
            return base.GetHashCode();
        }

        public static bool operator ==(AbilityClosure a, AbilityClosure b) { return a.Equals(b); }
        public static bool operator !=(AbilityClosure a, AbilityClosure b) { return !a.Equals(b); }

        #endregion
	}
}
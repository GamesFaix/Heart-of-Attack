using System;
using System.Collections;
using System.Collections.Generic;


namespace HOA.Abilities
{
    public delegate bool UsableTest(out string message);
    
    public partial class Ability : ClosedFunc<AbilityArgs, IEnumerator>, 
        IComparable<Ability>, IEquatable<Ability>
    {
        #region ISourced

        Source source;
        Token sourceToken { get { return source.Last<Token>(); } }
        Unit sourceUnit { get { return source.Last<Unit>(); } }
        Player sourcePlayer { get { return source.Last<Player>(); } }

        Type[] validSources { get { return new Type[1] { typeof(Token) }; } }
        bool IsValidSource(object obj) { return Source.IsValid(validSources, obj); }

        #endregion


        #region Properties

        public string Name { get; private set; }
        public Description Desc;
        public Rank Rank { get; private set; }
        public Price Price { get; private set; }
       
        public AbilityArgs Args { get; private set; }
        public AimPlan Aims { get; private set; }
        
        public bool UsedThisTurn { get; private set; }
        public UsableTest Usable { get; private set; }

        private Action PreEffects;
        private Action<NestedList<IEntity>> MainEffects;
        private Action PostEffects;

        public Action Adjust;
        public Action Unadjust;
        
        #endregion 

        private Ability(object source, string name, Rank rank, Price price, AbilityArgs args)
            : base (args)
        {
            if (!IsValidSource(source))
                throw new InvalidSourceException();
            this.source = new Source(source);
            Name = name;
            Rank = rank;
            Price = price;
            Args = args;

            Desc = Scribe.Write("[Ability description]");

            UsedThisTurn = false;
            Usable += UserInQueue;
            Usable += UserIsTop;
            Usable += Unused;
            Usable += Affordable;
            Usable += AlreadyProcessing;
            
            Aims = new AimPlan(this);

            PreEffects = () => { Charge(); };
            MainEffects = (targets) => { };
            PostEffects = () => { };

            Adjust = () => { };
            Unadjust = () => { };
        }

        public void Execute (NestedList<IEntity> targets)
        {
            PreEffects();
            MainEffects(targets);
            PostEffects();
        }

        public void Reset() { UsedThisTurn = false; }

        public void Charge()
        {
            Unit u;
            if (source.Last<Unit>(out u))
            {
                UsedThisTurn = true;
                u.Charge(Price);
            }
        }

        #region IComparable & IEquatable

        /// <summary> Compare by Rank, then Price, then Name </summary>
        public int CompareTo(Ability other)
        {
            if (Rank < other.Rank) 
                return -1;
            else if (Rank > other.Rank) 
                return 1;
            else
            {
                int i = Price.CompareTo(other.Price);
                if (i != 0) 
                    return i;
                else 
                    return (Name.CompareTo(other.Name));
            }
        }

        public bool Equals(Ability other)
        {
            if (Name != other.Name)
                return false;
            return Args == other.Args;
        }

        public override bool Equals(object other)
        {
            return (other is Ability && (other as Ability).Equals(this));
        }

        public override int GetHashCode() 
        {
            Debug.Log("No custom implementation.");
            return base.GetHashCode(); 
        }

        public static bool operator ==(Ability a, Ability b) { return a.Equals(b); }
        public static bool operator !=(Ability a, Ability b) { return !a.Equals(b); }

        #endregion

        #region Usable tests

        private bool UserIsTop(out string message) 
        {
            bool b = (source.Last<Unit>() == Session.Active.Queue.Top);
            message = (b ? "" : "It is not currently " + source + "'s turn.");
            return b; 
        }

        private bool UserInQueue(out string message)
        {
            bool b = (Session.Active.Queue.Contains(source.Last<Unit>()));
            message = (b ? "" : source + " is not in the TurnQueue.");
            return b;
        }

        private bool Unused(out string message)
        {
            message = (!UsedThisTurn ? "" : Name + " has already been used this turn.");
            return !UsedThisTurn;
        }

        private bool Affordable(out string message)
        {
            Unit u;
            bool b = (source.Last<Unit>(out u) && u.CanAfford(Price));
            message = (b ? "" : source + " cannot afford " + Name + ".");
            return b;
        }
        private bool AlreadyProcessing(out string message)
        {
            message = (!EffectQueue.Active ? "" : "Another action is currently in progress.");
            return !EffectQueue.Active;
        }

        private bool AirClear(out string message)
        {
            bool b = (sourceToken.Cell.occupants
                / Filter.Plane(Tokens.Plane.Air, true))
                .Count < 1;
            message = (b ? "" : "A token occupies the required air space.");
            return b;
        }

        private bool GroundClear(out string message)
        {
            bool b = (sourceToken.Cell.occupants
                / Filter.Plane(Tokens.Plane.Ground, true))
                .Count < 1;
            message = (b ? "" : "A token occupies the required ground space.");
            return b;
        }

        #endregion

    }
}
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

        Type[] validSources { get { return new Type[2] { typeof(Unit), typeof(Source.ForcedSource) }; } }
        bool IsValidSource(object obj) { return Source.IsValid(validSources, obj); }

        #endregion


        #region Properties
        public AbilityArgs args { get; private set; }
        public string name { get { return args.name; } }
        public Description desc {
            get { return args.desc; }
            set { args.desc = value; }
        }
        public Rank rank { get { return args.rank; } }
        public Price price { get { return args.price; } }
        public int value { get { return args.value; } }
        public int[] values { get { return args.values; } }

        public AimPlan Aims { get; private set; }
        
        public bool UsedThisTurn { get; private set; }
        public UsableTest Usable { get; private set; }

        private Action PreEffects, PostEffects;
        private Action<NestedList<IEntity>> MainEffects;

        public Action Adjust, Unadjust;
        
        #endregion 

        private Ability(object source, AbilityArgs args=null)
            : base (args)
        {
            if (!IsValidSource(source))
                throw new InvalidSourceException(source.GetType() + " " + source);
            this.source = new Source(source);
            this.args = args;

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
            Debug.Log("Executing {0} ({1}).", args.name, args);
            PreEffects();
            MainEffects(targets);
            PostEffects();
        }

        public void Reset() 
        { 
            UsedThisTurn = false;
            Unadjust();
        }

        public void Charge()
        {
            Unit u;
            if (source.Last<Unit>(out u))
            {
                UsedThisTurn = true;
                u.Charge(args.price);
                Debug.Log("{0} charged to {1}.", args.price, u);
            }
        }

        #region IComparable & IEquatable

        /// <summary> Compare by Rank, then Price, then Name </summary>
        public int CompareTo(Ability other)
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

        public bool Equals(Ability other)
        {
            if (other as object == null)
                return false;
            if (name != other.name)
                return false;
            return args == other.args;
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
            message = (!UsedThisTurn ? "" : name + " has already been used this turn.");
            return !UsedThisTurn;
        }

        private bool Affordable(out string message)
        {
            Unit u;
            bool b = (source.Last<Unit>(out u) && u.CanAfford(price));
            message = (b ? "" : source + " cannot afford " + name + ".");
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

        public override string ToString() { return name; }
    }
}
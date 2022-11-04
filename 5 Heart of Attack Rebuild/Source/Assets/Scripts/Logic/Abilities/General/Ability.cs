using System;
using System.Collections.Generic;


namespace HOA.Abilities
{
    public delegate bool UsableTest(out string message);
    
    public partial class Ability : IComparable<Ability>, IEquatable<Ability>, IEffectUser
    {
        #region Properties
        
        public IAbilityUser User { get; private set; }
        
        public string Name { get; private set; }
        public Description Desc;
        public Rank Rank { get; private set; }
        public AbilityArgs Args { get; private set; }


        public bool UsedThisTurn { get; private set; }
        public UsableTest Usable { get; private set; }

        public Price Price { get; private set; }
        public List<Aim> Aims { get; private set; }


        private Action PreEffects;
        private Action<NestedList<IEntity>> MainEffects;
        private Action PostEffects;

        public Action Adjust;
        public Action Unadjust;
        
        #endregion 

        private Ability(IAbilityUser user, string name, Rank rank, Price price, AbilityArgs args)
        {
            User = user;
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
            
            Aims = new List<Aim>();

            PreEffects = () => { Charge(); };
            MainEffects = (targets) => { };
            PostEffects = () => { };

            Adjust = () => { };
            Unadjust = () => { };
        }

        public void Execute(NestedList<IEntity> targets)
        {
            PreEffects();
            MainEffects(targets);
            PostEffects();
        }

        public void Reset() { UsedThisTurn = false; }

        public void Charge()
        {
            if (User is Unit)
            {
                UsedThisTurn = true;
                (User as Unit).Charge(Price);
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
            bool b = (User == Session.Active.Queue.Top);
            message = (b ? "" : "It is not currently " + User + "'s turn.");
            return b; 
        }

        private bool UserInQueue(out string message)
        {
            bool b = (Session.Active.Queue.Contains(User as Unit));
            message = (b ? "" : User + " is not in the TurnQueue.");
            return b;
        }

        private bool Unused(out string message)
        {
            message = (!UsedThisTurn ? "" : Name + " has already been used this turn.");
            return !UsedThisTurn;
        }

        private bool Affordable(out string message)
        {
            bool b = (User is Unit && (User as Unit).CanAfford(Price));
            message = (b ? "" : User + " cannot afford " + Name + ".");
            return b;
        }
        private bool AlreadyProcessing(out string message)
        {
            message = (!EffectQueue.Active ? "" : "Another action is currently in progress.");
            return !EffectQueue.Active;
        }

        #endregion

        public Ability ToAbility() { return this; }
        public IAbilityUser ToAbilityUser() { return User; }
        public Tokens.ITokenCreator ToTokenCreator()
        {
            if (ToAbilityUser() != null)
            {
                Token t = ToAbilityUser().ToToken();
                if (t != null)
                    return t.Owner;
            }
            return null;
        }
    }
}
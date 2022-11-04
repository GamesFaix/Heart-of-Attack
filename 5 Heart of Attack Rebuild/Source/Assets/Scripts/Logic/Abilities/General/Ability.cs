using System;
using System.Collections.Generic;
using HOA.Collections;

namespace HOA.Abilities
{

    public enum Rank {
        None = 0,
        Move,
        Focus,
        Attack,
        Special,
        Create
    }

    public delegate bool UsableTest(out string message);
        

    public class Ability : IComparable<Ability>, IEffectUser
    {
        #region Properties
        
        public IAbilityUser User { get; private set; }
        
        public string Name { get; private set; }
        public Func<string> Desc;
        public Rank Rank { get; private set; }

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

        private Ability(string name, Rank rank, Price price)
        {
            Name = name;
            Desc = () => { return "[Ability description]"; };
            Rank = rank;

            UsedThisTurn = false;
            Usable += UserInQueue;
            Usable += UserIsTop;
            Usable += Unused;
            Usable += Affordable;
            Usable += AlreadyProcessing;
            
            Price = price;
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

        public int CompareTo(Ability other)
        {
            try
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
            catch
            {
                return 0;
            }
        }

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
    }
}
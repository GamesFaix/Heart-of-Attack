using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA
{
    public static class Filter
    {
        static Predicate<IEntity> alwaysFalse = (t) => { return false; };
        public static Predicate<IEntity> False { get { return alwaysFalse; } }

        static Predicate<IEntity> cell = (t) => { return (t is Cell); };
        public static Predicate<IEntity> Cell { get { return cell; } }

        static Predicate<IEntity> token = (t) => { return (t is Token); };
        public static Predicate<IEntity> Token { get { return token; } }
        
        static Predicate<IEntity> unit = (t) => { return (t is Unit); };
        public static Predicate<IEntity> Unit { get { return unit; } }
        
        static Predicate<IEntity> ob = (t) => { return (t is Obstacle); };
        public static Predicate<IEntity> Ob { get { return ob; } }
        
        static Predicate<IEntity> terrain = (t) => { return (t is Terrain); };
        public static Predicate<IEntity> Terrain { get { return terrain; } }
        
        static Predicate<IEntity> king = Rank(Tokens.Rank.King, true);
        public static Predicate<IEntity> King { get { return token + king; } }
        
        static Predicate<IEntity> heart = (t) => { return ((Token)t).heart; };
        public static Predicate<IEntity> Heart { get { return token + heart; } }
        
        static Predicate<IEntity> destructible = (t) => { return ((Token)t).destructible; };
        public static Predicate<IEntity> Destructible { get { return token + destructible; } }
        
        static Predicate<IEntity> corpse = (t) => { return ((Token)t).corpse; };
        static Predicate<IEntity> nonCorpse = (t) => { return !((Token)t).corpse; };
        public static Predicate<IEntity> Corpse { get { return token + corpse; } }

        public static Predicate<IEntity> DestNotCorpse { get { return token + destructible + nonCorpse; } }

        
        static Predicate<IEntity> trample = (t) => { return ((Token)t).trample; };
        public static Predicate<IEntity> Trample { get { return token + trample; } }
        
        static Predicate<IEntity> unitDest = (t) =>
        {
            if (t is Unit) 
                return true;
            if (((Token)t).destructible) 
                return true;
            return false;
        };
        public static Predicate<IEntity> UnitDest { get { return token + unitDest; } }

        static Predicate<IEntity> legal = (t) => { return (t.Legal); };
        public static Predicate<IEntity> Legal { get { return legal; } }

        public static Predicate<IEntity> Owner(Player owner, bool b)
        {
            return (t) =>
            {
                return ((((Token)t).Owner == owner) == b);
            };
        }
        public static Predicate<IEntity> Plane(Plane plane, bool b)
        {
            return (t) =>
            {
                Token token = (Token)t;
                bool match = ((token.plane & plane) == plane);
                return (match == b);
            };
        }
        public static Predicate<IEntity> Rank(Tokens.Rank rank, bool b)
        {
            return (t) =>
            {
                Unit u = t as Unit;
                return (u.rank == rank) == b;
            };
        }

        public static Predicate<IEntity> Species(Species species, bool b)
        {
            return (t) =>
            {
                return ((((Token)t).Species == species) == b);
            };
        }

        public static Predicate<IEntity> identity(IEntity entity, bool b)
        {
            return (t) =>
            {
                return ((t == entity) == b);
            };
        }
        


        public static Predicate<IEntity> Occupiable(Token token)
        {
            return (t) =>
            {
                return (t is Cell && token.CanEnter((Cell)t));
            };
        }

    }
}

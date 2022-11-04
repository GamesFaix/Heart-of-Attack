using System;
using System.Collections.Generic;
using HOA.Tokens;
using Pred = System.Predicate<HOA.IEntity>;

namespace HOA
{
    public static class Filter
    {
        private static readonly Pred
            alwaysFalse,
            cell,
            token,
            unit,
            ob,
            terrain,
            king,
            heart,
            destructible,
            trample,
            corpse,
            notCorpse,
            unitDest,
            legal;

        public static Pred False { get { return alwaysFalse; } }
        public static Pred Cell { get { return cell; } }
        public static Pred Token { get { return token; } }
        public static Pred Unit { get { return unit; } }
        public static Pred Ob { get { return ob; } }
        public static Pred Terrain { get { return terrain; } }
        public static Pred King { get { return unit + king; } }
        public static Pred Heart { get { return token + heart; } }
        public static Pred Destructible { get { return token + destructible; } }
        public static Pred Corpse { get { return token + corpse; } }
        public static Pred DestNotCorpse { get { return token + destructible + notCorpse; } }
        public static Pred Trample { get { return token + trample; } }
        public static Pred UnitDest { get { return token + unitDest; } }
        public static Pred Legal { get { return legal; } }

        static Filter()
        {
            alwaysFalse = (t) => { return false; };
            cell = (t) => { return (t is Board.Cell); };
            token = (t) => { return (t is Token); };
            unit = (t) => { return (t is Unit); };
            ob = (t) => { return (t is Obstacle); };
            terrain = (t) => { return (t is Terrain); };
            king = Rank(UnitRank.King, true);
            heart = (t) => { return ((Token)t).heart; };
            destructible = (t) => { return ((Token)t).destructible; };
            unitDest = (t) =>
            {
                if (t is Unit) 
                    return true;
                if (((Token)t).destructible) 
                    return true;
                return false;
            };
            trample = (t) => { return ((Token)t).trample; };
            corpse = (t) => { return ((Token)t).corpse; };
            notCorpse = (t) => { return !((Token)t).corpse; };
            legal = (t) => { return (t.Legal); };
        
        
        }

       

        public static Pred Owner(Player owner, bool b)
        {
            return (t) =>
            {
                return ((((Token)t).owner == owner) == b);
            };
        }
        

        public static Pred Plane(Plane plane, bool b)
        {
            return (t) =>
            {
                Token token = (Token)t;
                bool match = ((token.plane & plane) == plane);
                return (match == b);
            };
        }

        public static Pred Rank(UnitRank rank, bool b)
        {
            return (t) =>
            {
                return ((t as Unit).rank == rank) == b;
            };
        }

        public static Pred Species(Species species, bool b)
        {
            return (t) =>
            {
                return ((((Token)t).species == species) == b);
            };
        }

        public static Pred identity(IEntity entity, bool b)
        {
            return (t) =>
            {
                return ((t == entity) == b);
            };
        }

        public static Pred Occupiable(Token token)
        {
            return (t) =>
            {
                return (t is Board.Cell && token.CanEnter((Board.Cell)t));
            };
        }

    }
}

using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA
{
    public static class EntityPredicates
    {

        public static readonly Predicate<IEntity> Cell = (t) => { return (t is Cell); };
        public static readonly Predicate<IEntity> Token = (t) => { return (t is Token); };
        public static readonly Predicate<IEntity> Unit = (t) => { return (t is Unit); };
        public static readonly Predicate<IEntity> Ob = (t) => { return (t is Token && !(t is Unit)); };
        public static readonly Predicate<IEntity> King = (t) =>
        { return (t is Unit && ((Unit)t).rank == Rank.King); };
        public static readonly Predicate<IEntity> Heart = (t) => 
        { return (t is Token ? ((Token)t).heart : false); };
        public static readonly Predicate<IEntity> Destructible = (t) =>
        { return (t is Token ? ((Token)t).destructible : false); };
        public static readonly Predicate<IEntity> Corpse = (t) =>
        { return (t is Token ? ((Token)t).corpse : false); };
        public static readonly Predicate<IEntity> Trample = (t) =>
        { return (t is Token ? ((Token)t).trample : false); };
        public static readonly Predicate<IEntity> UnitDest = (t) =>
        {
            if (t is Unit) return true;
            if (t is Token && ((Token)t).destructible) return true;
            return false;
        };
        public static readonly Predicate<IEntity> Legal = (t) => { return (t.Legal); };

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
        public static Predicate<IEntity> Species(Species species, bool b)
        {
            return (t) =>
            {
                return ((((Token)t).Species == species) == b);
            };
        }
        public static Predicate<IEntity> Indentity(IEntity entity, bool b)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOA 
{
    public delegate bool FilterTest(Target t);

    public static class FilterTests
    {

        public static readonly FilterTest Cell = (t) => { return (t is Cell); };
        public static readonly FilterTest Token = (t) => { return (t is Token); };
        public static readonly FilterTest Unit  = (t) => { return (t is Unit); };
        public static readonly FilterTest Ob  = (t) => { return (t is Obstacle); };
        public static readonly FilterTest King = (t) => { return (t is King); };
        public static readonly FilterTest Heart = (t) => { return (t is Heart); };


        public static readonly FilterTest Destructible  = (t) => 
        { return (t is Token ? ((Token)t).Body.Destructible : false); };
        public static readonly FilterTest Corpse  = (t) => 
        { return (t is Token ? ((Token)t).Body.Corpse : false); };
        public static readonly FilterTest Trample = (t) => 
        { return (t is Token ? ((Token)t).Body.Trample : false); };
        public static readonly FilterTest UnitDest = (t) =>
        {
            if (t is Unit) return true;
            if (t is Token && ((Token)t).Body.Destructible) return true;
            return false;
        };
        public static readonly FilterTest Legal = (t) => { return (t.Legal); };

        public static FilterTest Owner(Player owner, bool b)
        {
            return (t) =>
            {
                return ( ( ((Token)t).Owner == owner ) == b);
            };
        }
        public static FilterTest Plane(Plane plane, bool b)
        {
            return (t) =>
            {
                Token token = (Token)t;
                bool match = ((token.Plane & plane) == plane);
                return (match == b);
            };
        }
        public static FilterTest Species(Species token, bool b)
        {
            return (t) =>
            {
                return ((((Token)t).ID.Species == token) == b);
            };
        }
        public static FilterTest SpecificTarget(Target Target, bool b)
        {
            return (t) =>
            {
                return ((t == Target) == b);
            };
        }

        public static FilterTest Occupiable (Token token)
        {
            return (t) => 
            { 
                return (t is Cell && token.Body.CanEnter((Cell)t)); 
            };
        }

    }
}

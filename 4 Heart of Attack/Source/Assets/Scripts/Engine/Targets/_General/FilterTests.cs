using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOA 
{
    public delegate bool FilterTest(Target t);

    public static class FilterTests
    {

        public static readonly FilterTest Cell = (t) => { return (t.TargetClass[TargetClasses.Cell]); };
        public static readonly FilterTest Token = (t) => { return (t.TargetClass[TargetClasses.Token]); };
        public static readonly FilterTest Unit  = (t) => { return (t.TargetClass[TargetClasses.Unit]); };
        public static readonly FilterTest Ob  = (t) => { return (t.TargetClass[TargetClasses.Ob]); };
        public static readonly FilterTest Dest  = (t) => { return (t.TargetClass[TargetClasses.Dest]); };
        public static readonly FilterTest Corpse  = (t) => { return (t.TargetClass[TargetClasses.Corpse]); };
        public static readonly FilterTest King  = (t) => { return (t.TargetClass[TargetClasses.King]); };
        public static readonly FilterTest Heart  = (t) => { return (t.TargetClass[TargetClasses.Heart]); };
        public static readonly FilterTest Tram = (t) => { return (t.TargetClass[TargetClasses.Tram]); };
        public static readonly FilterTest UnitDest = (t) => { return (t.TargetClass[TargetClasses.Unit] || t.TargetClass[TargetClasses.Dest]); };
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
                return match = b;
            };
        }
        public static FilterTest EToken(EToken token, bool b)
        {
            return (t) =>
            {
                return ((((Token)t).ID.Code == token) == b);
            };
        }
        public static FilterTest SpecificTarget(Target target, bool b)
        {
            return (t) =>
            {
                return ((t == target) == b);
            };
        }
        public static FilterTest TargetClass(TargetClasses tc, bool b)
        {
            return (t) =>
            {
                return (t.TargetClass[tc] == b);
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

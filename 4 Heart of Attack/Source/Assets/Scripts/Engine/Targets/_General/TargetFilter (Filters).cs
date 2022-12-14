namespace HOA
{
    public partial class TargetFilter
    {
        public static TargetFilter None { get { return new TargetFilter(); } }

        public static TargetFilter Cell { get { return new TargetFilter(FilterTests.Cell); } }
        public static TargetFilter Token { get { return new TargetFilter(FilterTests.Token); } }
        public static TargetFilter Unit { get { return new TargetFilter(FilterTests.Unit); } }
        public static TargetFilter Ob { get { return new TargetFilter(FilterTests.Ob); } }
        public static TargetFilter Dest { get { return new TargetFilter(FilterTests.Destructible); } }
        public static TargetFilter Corpse { get { return new TargetFilter(FilterTests.Corpse); } }
        public static TargetFilter King { get { return new TargetFilter(FilterTests.King); } }
        public static TargetFilter Heart { get { return new TargetFilter(FilterTests.Heart); } }
        public static TargetFilter UnitDest { get { return new TargetFilter(FilterTests.UnitDest); } }
        public static TargetFilter Tram { get { return new TargetFilter(FilterTests.Trample); } }
        
        public static TargetFilter Legal { get { return new TargetFilter(FilterTests.Legal); } }
        public static TargetFilter Occupiable(Token token) { return Cell + FilterTests.Occupiable(token); }

        public static TargetFilter Owner(Player player, bool b) { return Token + FilterTests.Owner(player, b); }
        public static TargetFilter Species(Species token, bool b) { return Token + FilterTests.Species(token, b); }
        public static TargetFilter Plane(Plane plane, bool b) { return Token + FilterTests.Plane(plane, b); }
        public static TargetFilter SpecificTarget(Target Target, bool b) { return Token + FilterTests.SpecificTarget(Target, b); }
        
    }
}

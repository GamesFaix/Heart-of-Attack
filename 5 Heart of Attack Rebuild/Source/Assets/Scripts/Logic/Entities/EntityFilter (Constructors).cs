namespace HOA
{
    public partial class EntityFilter
    {
        public static EntityFilter None { get { return new EntityFilter(); } }

        public static EntityFilter Cell { get { return new EntityFilter(EntityPredicates.Cell); } }
        public static EntityFilter Token { get { return new EntityFilter(EntityPredicates.Token); } }
        public static EntityFilter Unit { get { return new EntityFilter(EntityPredicates.Unit); } }
        public static EntityFilter Ob { get { return new EntityFilter(EntityPredicates.Ob); } }
        public static EntityFilter Dest { get { return new EntityFilter(EntityPredicates.Destructible); } }
        public static EntityFilter Corpse { get { return new EntityFilter(EntityPredicates.Corpse); } }
        public static EntityFilter King { get { return new EntityFilter(EntityPredicates.King); } }
        public static EntityFilter Heart { get { return new EntityFilter(EntityPredicates.Heart); } }
        public static EntityFilter UnitDest { get { return new EntityFilter(EntityPredicates.UnitDest); } }
        public static EntityFilter Tram { get { return new EntityFilter(EntityPredicates.Trample); } }

        public static EntityFilter Legal { get { return new EntityFilter(EntityPredicates.Legal); } }
        public static EntityFilter Occupiable(Token token) { return Cell + EntityPredicates.Occupiable(token); }

        public static EntityFilter Owner(Player player, bool b) { return Token + EntityPredicates.Owner(player, b); }
        public static EntityFilter Species(Tokens.Species token, bool b) { return Token + EntityPredicates.Species(token, b); }
        public static EntityFilter Plane(Tokens.Plane plane, bool b) { return Token + EntityPredicates.Plane(plane, b); }
        public static EntityFilter Indentity(IEntity entity, bool b) { return new EntityFilter(EntityPredicates.Indentity(entity, b)); }

    }
}

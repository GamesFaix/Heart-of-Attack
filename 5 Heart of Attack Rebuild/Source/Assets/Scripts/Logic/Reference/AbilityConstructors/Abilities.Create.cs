using HOA.Tokens;

namespace HOA.Abilities
{
    /*
    public partial class Ability
    {
        public static Ability Animate(IAbilityUser user)
        {
            AbilityArgs args = new AbilityArgs(Species.Metaterrainean);
            Ability a = new Ability(user, "Animate Metaterrainean", Rank.Create, new Price(1, 2), args);
            a.Desc = () => { return "Replace Target non-remains destructible with Metaterrainean."; };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Dest));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Replace(new Source(a.Parent), (Token)Targets[0], Species.Metaterrainean));
           
            return a;
        }
        public static Ability Arise(Unit parent)
        {
            Ability a = new Ability(parent, "Arise", 4, new Price(2, 0), Species.Ashes);
            a.Desc = () =>
            {
                return "Transform " + a.Parent + " into a Conflagragon." +
                "\n(New Conflagragon starts with " + a.Parent + "'s health.)";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                int hp = ((Unit)a.Parent).HP;
                EffectQueue.Add(Effect.DestroyUnit(new Source(a.Parent), a.Parent));
                Token newToken =
                    TokenFactory.Create(new Source(a.Parent), Species.Conflagragon, a.Parent.Body.Cell, false);
                EffectQueue.Add(Effect.SetStat(new Source(newToken), (Unit)newToken, Stats.Health, hp));
            };

            a.Restrict = () =>
            {
                Set<IEntity> group = a.Parent.Body.Cell.Occupants;
                group -= TargetFilter.Plane(Plane.Air, true);
                return (group.Count > 0);

            };

            
            return a;
        }
        public static Ability Create(Unit parent, Price price, Species species)
        {
            Ability a = new Ability(parent, "", 5, price, species);
            a.Template = TokenRegistry.Templates[a.Species];
            a.Name = "Create " + a.Template.ID.Name;
            a.Desc = () => { return "Create " + a.Template.ID.Name + " in Target cell."; };
            a.Aims.Add(Aim.CreateNeighbor());
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)Targets[0], species));
           
            return a;
        }
        public static Ability CreateArc(Unit parent, Price price, Species species, int range, int minRange = 0)
        {
            Ability a = new Ability(parent, "", 5, price, species);
            a.Template = TokenRegistry.Templates[a.Species];
            a.Name = "Create " + a.Template.ID.Name;
            a.Desc = () => { return "Create " + a.Template.ID.Name + " in Target cell."; };
            a.Aims.Add(Aim.CreateArc(minRange, range));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)Targets[0], species));
            
            return a;
        }
        public static Ability CreateAren(Unit parent)
        {
            Ability a = new Ability(parent, "Create Arena", 5, new Price(1, 1), Species.Arena);
            a.Desc = () => { return "Create Arena in Target cell."; };
            a.Aims.Add(Aim.CreateAren());
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)Targets[0], Species.Arena));
           
            return a;
        }
        public static Ability CreateLich(Unit parent)
        {
            Ability a = new Ability(parent, "Create Lichenthropes", 5, Price.Cheap, Species.Lichenthrope);
            a.Desc = () => { return "Create Lichenthropes in up to two Target cells."; };
            a.Aims.Add(Aim.CreateNeighbor());
            a.Aims.Add(Aim.CreateNeighbor());
            a.MainEffects = Targets =>
            {
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)Targets[0], Species.Lichenthrope));
                if (Targets[1] != null)
                    EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)Targets[1], Species.Lichenthrope));
                Targeter.Reset();
            };
            a.PostEffects = () => { return; };
            
            return a;
        }
        public static Ability CreateRook(Unit parent)
        {
            Ability a = new Ability(parent, "Create Rook", 5, new Price(1, 1), Species.Rook);
            a.Desc = () => { return "Create Rook in " + a.Parent + "'s cell."; };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                Set<IEntity> group = a.Parent.Body.Cell.Occupants;
                group -= TargetFilter.Plane(Plane.Ground, true);
                if (group.Count > 0)
                {
                    a.Charge();
                    TokenFactory.Create(new Source(a.Parent), Species.Rook, a.Parent.Body.Cell);
                }
            };
           
            return a;
        }
        public static Ability Evolve(Unit parent, Price price, Species species)
        {
            Ability a = new Ability(parent, "", 4, price, species);
            a.Template = TokenRegistry.Templates[a.Species];
            a.Name = "Evolve to " + a.Template.ID.Name;
            a.Desc = () =>
            {
                return "Transform " + a.Parent + " into a " + a.Template.ID.Name + ".  " +
                "\n(New " + a.Template.ID.Name + " is added to the end of the Queue and does not retain any of " + a.Parent + "'s attributes.)";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Replace(new Source(a.Parent), a.Parent, a.Species));
            
            return a;
        }
        public static Ability Exhume(Unit parent)
        {
            Ability a = new Ability(parent, "Exhume", 5, Price.Free, Species.Corpse);
            a.Desc = () => { return "Create Corpse in Target cell."; };
            a.Aims.Add(Aim.Free(TargetFilter.Cell, EPurp.CREATE));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)Targets[0], Species.Corpse));
            
            return a;
        }
        public static Ability Recycle(Unit parent, Price price)
        {
            Ability a = new Ability(parent, "Recycle", 5, price, Species.Recyclops);
            a.Desc = () => { return "Replace Target remains with Recyclops."; };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Corpse));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Replace(new Source(a.Parent), (Token)Targets[0], Species.Recyclops));
            
            return a;
        }
    }
     * */
}
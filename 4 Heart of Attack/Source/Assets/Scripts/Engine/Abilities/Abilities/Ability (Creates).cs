using UnityEngine; 

namespace HOA { 

    public partial class Ability{

        public static Ability Animate(Unit parent)
        {
            Ability a = new Ability(parent, "Animate Metaterrainean", 5, new Price(1, 2), EToken.META);
            a.Desc = () => { return "Replace target non-remains destructible with Metaterrainean."; };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Dest));
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.Replace(new Source(a.Parent), (Token)targets[0], EToken.META));
            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability Arise(Unit parent)
        {
            Ability a = new Ability(parent, "Arise", 4, new Price(2, 0), EToken.ASHE);
            a.Desc = () =>
            {
                return "Transform " + a.Parent + " into a Conflagragon." +
                "\n(New Conflagragon starts with " + a.Parent + "'s health.)";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = targets =>
            {
                int hp = ((Unit)a.Parent).HP;
                a.Parent.Die(new Source(a.Parent), false, false);
                Token newToken;
                TokenFactory.Create(new Source(a.Parent), EToken.CONF, a.Parent.Body.Cell, out newToken, false);
                ((Unit)newToken).SetStat(new Source(a.Parent), Stats.Health, hp);
            };

            a.Restrict = () =>
            { return a.Parent.Body.Cell.Contains(Plane.Air); };

            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability Create(Unit parent, Price price, EToken tokenType)
        {
            Ability a = new Ability(parent, "", 5, price, tokenType);
            a.Template = TokenFactory.Template(a.TokenType);
            a.Name = "Create " + a.Template.ID.Name;
            a.Desc = () => { return "Create " + a.Template.ID.Name + " in target cell."; };
            a.Aims.Add(Aim.CreateNeighbor());
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)targets[0], tokenType));
            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability CreateArc(Unit parent, Price price, EToken tokenType, int range, int minRange = 0)
        {
            Ability a = new Ability(parent, "", 5, price, tokenType);
            a.Template = TokenFactory.Template(tokenType);
            a.Name = "Create " + a.Template.ID.Name;
            a.Desc = () => { return "Create " + a.Template.ID.Name + " in target cell."; };
            a.Aims.Add(Aim.CreateArc(minRange, range));
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)targets[0], tokenType));
            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability CreateAren(Unit parent)
        {
            Ability a = new Ability(parent, "Create Arena", 5, new Price(1, 1), EToken.AREN);
            a.Desc = () => { return "Create Arena in target cell."; };
            a.Aims.Add(Aim.CreateAren());
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)targets[0], EToken.AREN));
            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability CreateLich(Unit parent)
        {
            Ability a = new Ability(parent, "Create Lichenthropes", 5, Price.Cheap, EToken.LICH);
            a.Desc = () => { return "Create Lichenthropes in up to two target cells."; };
            a.Aims.Add(Aim.CreateNeighbor());
            a.Aims.Add(Aim.CreateNeighbor());
            a.MainEffects = targets =>
            {
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)targets[0], EToken.LICH));
                if (targets[1] != null)
                    EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)targets[1], EToken.LICH));
                Targeter.Reset();
            };
            a.PostEffects = () => { return; };
            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability CreateRook(Unit parent)
        {
            Ability a = new Ability(parent, "Create Rook", 5, new Price(1, 1), EToken.ROOK);
            a.Desc = () => { return "Create Rook in " + a.Parent + "'s cell."; };
            a.Aims.Add(Aim.Self());
            a.MainEffects = targets =>
            {
                if (!a.Parent.Body.Cell.Contains(Plane.Ground))
                {
                    a.Charge();
                    TokenFactory.Create(new Source(a.Parent), EToken.ROOK, a.Parent.Body.Cell);
                }
            };
            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability Evolve(Unit parent, Price price, EToken tokenType)
        {
            Ability a = new Ability(parent, "", 4, price, tokenType);
            a.Template = TokenFactory.Template(a.TokenType);
            a.Name = "Evolve to " + a.Template.ID.Name;
            a.Desc = () =>
            {
                return "Transform " + a.Parent + " into a " + a.Template.ID.Name + ".  " +
                "\n(New " + a.Template.ID.Name + " is added to the end of the Queue and does not retain any of " + a.Parent + "'s attributes.)";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.Replace(new Source(a.Parent), a.Parent, a.TokenType));
            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability Exhume(Unit parent)
        {
            Ability a = new Ability(parent, "Exhume", 5, Price.Free, EToken.CORP);
            a.Desc = () => { return "Create Corpse in target cell."; };
            a.Aims.Add(Aim.Free(TargetFilter.Cell, EPurp.CREATE));
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)targets[0], EToken.CORP));
            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability Recycle(Unit parent, Price price)
        {
            Ability a = new Ability(parent, "Recycle", 5, price, EToken.RECY);
            a.Desc = () => { return "Replace target remains with Recyclops."; };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Corpse));
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.Replace(new Source(a.Parent), (Token)targets[0], EToken.RECY));
            a.DrawSpecial = (p) =>
            {
                a.Template.DisplayThumbNameTemplate(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        

    }
}

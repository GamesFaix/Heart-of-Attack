using UnityEngine; 

namespace HOA { 

    public partial class Ability 
    {
        public static Ability Creep(Unit parent)
        {
            Ability a = new Ability(parent, "Creep", 1, Price.Cheap);
            a.multiMove = true;
            a.Desc = () => { return "Range +1 per focus."; };
            a.Aims.Add(Aim.MovePath(1));
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(Effect.Move(new Source(a.Parent), a.Parent, (Cell)target));
            };
            a.Adjust = () => a.Aims[0].Range = a.Aims[0].Range + a.Parent.FP;
            a.Unadjust = () => a.Aims.Add(Aim.MovePath(1));

            a.DrawAims = (p) =>
            {
                Aim actual = Aim.MovePath(a.Aims[0].Range + a.Parent.FP);
                actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
                float descH = (p.H - (p.LineH * 2)) / p.H;
                a.DrawSpecial(new Panel(p.TallWideBox(descH), p.LineH, p.s));
            };
            return a;
        }
        public static Ability Dart(Unit parent, int range)
        {
            Ability a = new Ability(parent, "Dart", 1, Price.Cheap, 0, range);
            a.Desc = () => { return "Move " + a.Parent + " to target cell."; };
            a.Aims.Add(Aim.MoveLine(a.Modifier));
            a.MainEffects = targets =>
            {
                Cell start = a.Parent.Body.Cell;
                Cell endCell = (Cell)targets[0];
                TargetGroup line = new TargetGroup();
                int2 dir = Direction.FromCells(a.Parent.Body.Cell, endCell);
                int length = Mathf.Max(
                    Mathf.Abs(start.X - endCell.X),
                    Mathf.Abs(start.Y - endCell.Y));
                Cell c = a.Parent.Body.Cell;
                for (int i = 0; i < length; i++)
                {
                    index2 index = c.Index + dir;
                    c = Game.Board.Cell(index);
                    line.Add(c);
                }
                foreach (Cell point in line)
                    EffectQueue.Add(Effect.Move(new Source(a.Parent), a.Parent, point));
            };
            return a;
        }
        public static Ability Defile(Unit parent)
        {
            Ability a = new Ability(parent, "Defile", 4, new Price(0, 1));
            a.teleport = true;
            a.Aims.Add(Aim.AttackArc(TargetFilter.Corpse, 0, 5));
            a.Aims.Add(Aim.MoveArc(0, 5));
            a.Desc = () => { return "Move target remains to target cell."; };
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.TeleportStart(new Source(a.Parent), (Token)targets[0], (Cell)targets[1]));
            return a;
        }
        public static Ability Dislocate(Unit parent)
        {
            Ability a = new Ability(parent, "Dislocate", 4, new Price(1, 1));
            a.teleport = true;
            TargetFilter f = TargetFilter.Unit
                + FilterTests.Owner(a.Parent.Owner, false)
                + FilterTests.TargetClass(TargetClasses.King, false);
            a.Aims.Add(Aim.AttackArc(f, 0, 5));
            a.Aims.Add(Aim.MoveArc(0, 5));
            a.Desc = () => { return "Move target enemy (exluding Attack Kings) to target cell."; };
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.TeleportStart(new Source(a.Parent), (Unit)targets[0], (Cell)targets[1]));
            return a;
        }
        public static Ability Move(Unit parent, int range)
        {
            Ability a = new Ability(parent, "Move", 1, Price.Cheap, 0, range);
            a.Desc = () => { return "Move " + a.Parent + " to target cell."; };
            a.Aims.Add(Aim.MovePath(a.Modifier));
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(Effect.Move(new Source(a.Parent), a.Parent, (Cell)target));
            };
            return a;
        }
        public static Ability Rebuild(Unit parent)
        {
            Ability a = new Ability(parent, "Rebuild", 1, new Price(0, 2));
            a.multiMove = true;
            a.Desc = () => { return "Move " + a.Parent + " to target cell."; };
            a.Aims.Add(Aim.MovePath(2));
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(Effect.Move(new Source(a.Parent), a.Parent, (Cell)target));
            };
            return a;
        }
        public static Ability Sprint(Unit parent)
        {
            Ability a = new Ability(parent, "Sprint", 4, Price.Free);
            a.multiMove = true;
            a.Desc = () =>
            {
                return "Move " + a.Parent + " to target cell.  " +
                "\nRange +1 per focus (up to +6). " +
                "\n" + a.Parent + " loses all focus.";
            };
            a.Aims.Add(Aim.MovePath(0));
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(Effect.Move(new Source(a.Parent), a.Parent, (Cell)target));
                a.Parent.SetStat(new Source(a.Parent), Stats.Focus, 0);
            };
            a.Adjust = () => a.Aims[0].Range = Mathf.Min(a.Parent.FP, 6);
            a.Unadjust = () => a.Aims.Add(Aim.MovePath(0));

            a.DrawAims = (p) =>
            {
                Aim actual = Aim.MovePath(Mathf.Min(6, a.Parent.FP));
                actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
                float descH = (p.H - (p.LineH * 2)) / p.H;
                a.DrawSpecial(new Panel(p.TallWideBox(descH), p.LineH, p.s));
            };
            return a;
        }
        public static Ability Tread(Unit parent)
        {
            Ability a = new Ability(parent, "Tread", 1, Price.Cheap);
            a.multiMove = true;
            a.Desc = () => { return "Move " + a.Parent + " to target cell."; };
            a.Aims.Add(Aim.MovePath(3));
            a.MainEffects = targets =>
            {
                foreach (Target target in targets)
                    EffectQueue.Add(Effect.Move(new Source(a.Parent), a.Parent, (Cell)target));
            };
            a.Adjust = () => a.Aims[0].Range = Mathf.Max(0, a.Aims[0].Range - a.Parent.FP);
            a.Unadjust = () => a.Aims.Add(Aim.MovePath(3));

            a.DrawAims = (p) =>
            {
                Aim actual = Aim.MovePath(Mathf.Max(0, a.Aims[0].Range - a.Parent.FP));
                actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
                float descH = (p.H - (p.LineH * 2)) / p.H;
                a.DrawSpecial(new Panel(p.TallWideBox(descH), p.LineH, p.s));
            };
            return a;
        }
        public static Ability Warp(Unit parent)
        {
            Ability a = new Ability(parent, "Warp", 4, new Price(1, 1));
            a.teleport = true;
            a.Desc = () => { return "Move target teammate (including self) to target cell."; };
            TargetFilter f = TargetFilter.Unit
                   + FilterTests.Owner(a.Parent.Owner, true)
                   + FilterTests.SpecificTarget(a.Parent, true);
            a.Aims.Add(Aim.AttackArc(f, 0, 5));
            a.Aims.Add(Aim.MoveArc(0, 5));
            a.MainEffects = targets =>
                EffectQueue.Add(Effect.TeleportStart(new Source(a.Parent), (Unit)targets[0], (Cell)targets[1]));
            return a;
        }
    }
}

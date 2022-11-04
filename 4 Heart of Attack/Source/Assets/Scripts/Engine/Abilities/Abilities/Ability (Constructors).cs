using UnityEngine;
using HOA.Textures;

namespace HOA
{

    public partial class Ability
    {

        #region //A-F

        public static Ability ArcticGust(Unit parent)
        {
            Ability a = new Ability(parent, "Arctic Gust", 4, new Price(1, 1), 15);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage Target Unit." +
                "\nTarget's Move range -2 until end of its next turn." +
                "\nTarget's neighbors and cellmates' Move range -1 until end of their next turn." +
                "\n(" + a.Parent.ID.Name + "'s Move range is not affected.)";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), Target, a.Damage));
                if (Target.Arsenal.Move != default(Ability))
                {
                    Ability move = Target.Arsenal.Move;
                    Aim aim = move.Aims[0];
                    aim.Range -= 2;
                    Target.timers.Add(Timer.ArcticGust(new Source(a.Parent), Target, 2, move));
                }
                TokenSet neighborUnits = Target.Body.Neighbors() - TargetFilter.Unit;
                foreach (Unit u in neighborUnits)
                {
                    if (u != a.Parent
                        && (u.Arsenal.Move != default(Ability)))
                    {
                        Ability move = u.Arsenal.Move;
                        Aim aim = move.Aims[0];
                        aim.Range -= 1;

                        u.timers.Add(Timer.ArcticGust(new Source(a.Parent), u, 1, move));
                        AVEffect.StatDown.Play(a.Parent);
                    }
                }
            };
            return a;
        }
        
        public static Ability BloodAltar(Unit parent)
        {
            Ability a = new Ability(parent, "Blood Altar", 4, Price.Cheap, 0, 4);
            a.Desc = () =>
            {
                return "Destroy neighboring teammate." +
                "\nInitiative +" + a.Modifier + " for next 2 turns.";
            };
            TargetFilter f = TargetFilter.Unit
                + FilterTests.Owner(a.Parent.Owner, true);
            a.Aims.Add(Aim.AttackNeighbor(f));
            a.MainEffects = Targets =>
            {
                EffectSet e = new EffectSet();
                e.Add(Effect.DestroyUnit(new Source(a.Parent), (Token)Targets[0]));
                e.Add(Effect.AddStat(new Source(a.Parent), (Unit)a.Parent, Stats.Initiative, a.Modifier));
                a.Parent.timers.Add(Timer.Altaration(new Source(a.Parent), a.Parent));
            };
            return a;
        }
        public static Ability Bombard(Unit parent)
        {
            Ability a = new Ability(parent, "Bombard", 4, new Price(2, 0), 10, 4);
            a.multiTarget = true;
            a.recursiveMove = true;
            a.Desc = () =>
            {
                return
                "Once per Focus (Max: 3), move upto " + a.Modifier + " cells in a line and " +
                "deal " + a.Damage + " explosive damage at that cell." +
                "\n(" + a.Parent + " receives no damage.)" +
                "\nLose all Focus.";
            };
            a.Aims.Add(Aim.MoveLine(a.Modifier));
            a.MainEffects = Targets =>
            {
                a.Parent.SetStat(new Source(a.Parent), Stats.Focus, 0);
                Cell start = a.Parent.Body.Cell;
                for (int i = 0; i < Targets.Count; i++)
                {
                    Cell endCell = (Cell)Targets[i];
                    Debug.Log("creating line from " + start + " to " + endCell);
                    CellSet line = new CellSet();
                    int2 dir = Direction.FromCells(start, endCell);

                    int length = Mathf.Max(
                        Mathf.Abs(start.X - endCell.X),
                        Mathf.Abs(start.Y - endCell.Y)
                        );
                    Cell c = start;
                    for (int j = 0; j < length; j++)
                    {
                        index2 index = c.Index + dir;
                        c = Game.Board.Cell(index);
                        line.Add(c);
                    }

                    foreach (Cell point in line)
                        EffectQueue.Add(Effect.Move(new Source(a.Parent), a.Parent, point));
                    EffectQueue.Add(Effect.ExplosionSequence(new Source(a.Parent), endCell, 10, true));
                    start = endCell;
                }
            };

            a.Adjust = () =>
            {
                int shots = Mathf.Min(a.Parent.FP, 3);
                for (int i = 2; i <= shots; i++)
                    a.Aims.Add(Aim.MoveLine(a.Modifier));
            };
            a.Unadjust = () =>
                a.Aims.Add(Aim.MoveLine(a.Modifier));
            a.DrawSpecial = (p) =>
            {
                int shots = Mathf.Min(3, a.Parent.FP);
                for (int i = 2; i <= shots; i++)
                    a.Aims[0].Draw(p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;

        }
        public static Ability Burrow(Unit parent)
        {
            Ability a = new Ability(parent, "Burrow", 1, Price.Cheap);
            a.Desc = () => { return "Move " + a.Parent + " to Target cell."; };
            a.Aims.Add(Aim.MoveArc(0, 3));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.BurrowStart(new Source(a.Parent), a.Parent, (Cell)Targets[0]));
            return a;
        }
        public static Ability Burst(Unit parent)
        {
            Ability a = new Ability(parent, "Burst", 4, Price.Cheap, 12);
            a.Desc = () =>
            {
                return "Destroy " + a.Parent + "." +
                "\nDo " + a.Damage + " damage to cellmates and neighbors. " +
                "\nDamaged units take " + ((int)Mathf.Floor(a.Damage * 0.5f)) + " corrosion counters. " +
                "\n(If a unit has corrosion counters, at the beginning of its turn " +
                "it takes damage equal to the number of counters, " +
                "then removes half the counters (rounded up).)";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                TokenSet victims = a.Parent.Body.Neighbors(true) - TargetFilter.Unit;
                EffectSet nextEffects = new EffectSet();
                nextEffects.Add(Effect.DestroyUnit(new Source(a.Parent), a.Parent));
                foreach (Unit u in victims)
                    nextEffects.Add(Effect.CorrodeInitial(new Source(a.Parent), u, a.Damage));
                EffectQueue.Add(nextEffects);
            };
            a.DrawSpecial = (p) =>
            {
                GUI.Label(p.LineBox, "All neighbors: ");
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.COR);
                GUI.Box(box, Icons.COR(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
                p.NextLine();
                GUI.Label(p.LineBox, "Destroy " + a.Parent + ".");
            };
            return a;
        }
        
        public static Ability Cannibalize(Unit parent)
        {
            Ability a = new Ability(parent, "Cannibalize", 4, Price.Cheap);
            a.Desc = () =>
            {
                return "Destroy Target remains." +
                "\nHealth +10/10";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Corpse));
            a.MainEffects = Targets =>
            {
                Token t = (Token)Targets[0];
                EffectSet e = new EffectSet();
                e.Add(Effect.DestroyUnit(new Source(a.Parent), t));
                e.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.MaxHealth, 10));
                e.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.Health, 10));
                EffectQueue.Add(e);
            };
            return a;
        }
        public static Ability Cannon(Unit parent, Price price, int damage)
        {
            Ability a = new Ability(parent, "Cannon", 3, price, damage);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit.  " +
                "\nMax range +1 per focus (up to +3).";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Unit, 2, 3));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            a.Adjust = () => a.Aims[0].Range += Mathf.Min(a.Parent.FP, 3);
            a.Unadjust = () => a.Aims[0].Range -= Mathf.Min(a.Parent.FP, 3);
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.DAMAGE);
                GUI.Box(box, Icons.DMG(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
                p.NextLine();
                GUI.Label(p.Box(0.9f), "Max Range +1 per Focus (up to +3).");
            };
            return a;
        }
        public static Ability Cocktail(Unit parent)
        {
            Ability a = new Ability(parent, "Cocktail", 3, new Price(1, 2), 20);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit. " +
                "\nTarget's neighbors and cellmates take 50% damage (rounded down).  " +
                "\nDestroy all destructible tokens that would take damage.";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.UnitDest, 0, 3));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.FireInitial(new Source(a.Parent), (Token)Targets[0], a.Damage));
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.FIR);
                GUI.Box(box, Icons.FIR(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
            };
            return a;
        }
        
        public static Ability DeathField(Unit parent)
        {
            Ability a = new Ability(parent, "Death Field", 4, new Price(1, 1), 5, 2);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to all units within " + a.Modifier + " cells of " + a.Parent.ID.Name + ". " +
                "\n" + a.Parent.ID.Name + " gains Health equal to damage successfully dealt.";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                Cell start = a.Parent.Body.Cell;
                int startX = start.X - a.Modifier;
                int endX = start.X + a.Modifier;
                int startY = start.Y - a.Modifier;
                int endY = start.Y + a.Modifier;
                CellSet cells = new CellSet();
                Cell cell;
                for (int i = startX; i <= endX; i++)
                    for (int j = startY; j <= endY; j++)
                        if (Game.Board.HasCell(i, j, out cell))
                            cells.Add(cell);
                TokenSet affected = cells.Occupants - TargetFilter.Unit;
                affected.Remove(a.Parent);
                foreach (Unit u in affected)
                    EffectQueue.Add(Effect.Leech(new Source(a.Parent), u, a.Damage));
            };
            return a;
        }
        public static Ability Detonate(Unit parent)
        {
            Ability a = new Ability(parent, "Detonate", 4, new Price(1, 1));
            a.Desc = () => { return "Destroy all mines on team."; };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                TokenSet mines = a.Parent.Owner.Tokens - TargetFilter.Species(Species.Mine, true);
                EffectSet e = new EffectSet();
                foreach (Token mine in mines)
                    e.Add(Effect.DestroyObstacle(new Source(a.Parent), mine));
                EffectQueue.Add(e);
            };
            return a;
        }
        public static Ability Discharge(Unit parent)
        {
            Ability a = new Ability(parent, "Discharge", 4, new Price(1, 2), 10, 5);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to self, neighbors, and cellmates.  " +
                "\nAll damaged units are stunned for " + a.Modifier + " turns.";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                CellSet cells = a.Parent.Body.Cell.Neighbors(true);
                TokenSet units = cells.Occupants - TargetFilter.Unit;
                EffectSet nextEffects = new EffectSet();
                foreach (Unit u in units)
                    nextEffects.Add(Effect.Shock(new Source(a.Parent), u, a.Damage, a.Modifier));
                EffectQueue.Add(nextEffects);
            };
            return a;
        }
        public static Ability Donate(Unit parent)
        {
            Ability a = new Ability(parent, "Donate", 4, Price.Cheap, 6);
            a.Desc = () =>
            {
                return "Target unit gains " + a.Damage + " health. " +
                "\n" + a.Parent + " takes damage equal to health successfully gained.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Donate(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            return a;
        }

        public static Ability End(Unit parent)
        {
            Ability a = new Ability(parent, "End turn", 0, Price.Free);
            a.Desc = () => { return "End current turn."; };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Advance(new Source(a.Parent), true));
            return a;
        }
        public static Ability Engorge(Unit parent)
        {
            Ability a = new Ability(parent, "Engorge", 4, new Price(1, 1), 12);
            a.Desc = () =>
            {
                return "Destroy neighboring non-Remains destructible." +
                "\n" + a.Parent + " gains " + a.Damage + " health.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Dest));
            a.MainEffects = Targets =>
            {
                EffectQueue.Add(Effect.DestroyObstacle(new Source(a.Parent), (Token)Targets[0]));
                EffectQueue.Add(Effect.AddStat(new Source(a.Parent), parent, Stats.Health, a.Damage));
            };
            return a;
        }

        public static Ability FatalBlow(Unit parent)
        {
            Ability a = new Ability(parent, "Fatal Blow", 4, new Price(1, 1), 15);
            a.Desc = () =>
            {
                return "Destroy " + a.Parent + "." +
                "\nDo " + a.Damage + " damage to Target unit. " +
                "\nTarget takes " + ((int)Mathf.Floor(a.Damage * 0.5f)) + " corrosion counters. " +
                "\n(If a unit has corrosion counters, at the beginning of its turn " +
                "it takes damage equal to the number of counters, " +
                "then removes half the counters (rounded up).)";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                EffectQueue.Add(Effect.CorrodeInitial(new Source(a.Parent), Target, a.Damage));
                EffectQueue.Add(Effect.DestroyUnit(new Source(a.Parent), a.Parent));
            };
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.COR);
                GUI.Box(box, Icons.COR(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
                p.NextLine();
                GUI.Label(p.LineBox, "Destroy " + a.Parent + ".");
            };
            return a;
        }
        public static Ability Feast(Unit parent)
        {
            Ability a = new Ability(parent, "Feast", 3, Price.Cheap, 12);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit. " +
                "\nGain health equal to damage successfully dealt.";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Unit, 2, 3));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Leech(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            return a;
        }
        public static Ability Feed(Unit parent)
        {
            Ability a = new Ability(parent, "Feed", 3, Price.Cheap, 5);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit. " +
                "\nGain health equal to damage successfully dealt.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Leech(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            return a;
        }
        public static Ability FireBreath(Unit parent)
        {
            Ability a = new Ability(parent, "Fire Breath", 3, new Price(2, 0), 10);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit. " +
                "\nTarget's neighbors and cellmates take 50% damage (rounded down).  " +
                "\nDestroy all destructible tokens that would take damage.";
            };
            a.Aims.Add(Aim.AttackLine(TargetFilter.UnitDest, 3));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.FireInitial(new Source(a.Parent), (Token)Targets[0], a.Damage));
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.FIR);
                GUI.Box(box, Icons.FIR(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
            };
            return a;
        }
        public static Ability Flail(Unit parent)
        {
            Ability a = new Ability(parent, "Flail", 3, Price.Cheap, 8);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit.  " +
                "\nRange +1 per focus (Up to +3).  " +
                "\n" + a.Parent + " loses all focus.";
            };
            a.Aims.Add(Aim.AttackPath(TargetFilter.Unit, 1));
            a.MainEffects = Targets =>
            {
                a.Parent.SetStat(new Source(a.Parent), Stats.Focus, 0, false);
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
                a.Unadjust();
            };
            a.Adjust = () => a.Aims[0].Range += Mathf.Min(a.Parent.FP, 3);
            a.Unadjust = () => a.Aims[0].Range = 1;
            a.DrawAims = (p) =>
            {
                Aim actual = Aim.AttackPath(TargetFilter.Unit, a.Aims[0].Range + Mathf.Min(3, a.Parent.FP));
                actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
                float descH = (p.H - (p.LineH * 2)) / p.H;
                a.DrawSpecial(new Panel(p.TallWideBox(descH), p.LineH, p.s));
            };
            return a;
        }
        public static Ability Fling(Unit parent)
        {
            Ability a = new Ability(parent, "Fling", 3, new Price(1, 1), 16);
            a.Desc = () => { return "Do " + a.Damage + " damage to Target unit."; };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Unit, 0, 3));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            return a;
        }
        public static Ability Focus(Unit parent)
        {
            Ability a = new Ability(parent, "Focus", 2, Price.Cheap);
            a.Desc = () => { return "Focus +1."; };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.Focus, 1));
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.FP);
                GUI.Box(box, Icons.Stats[Stats.Focus], p.s);
                p.NudgeX();
                GUI.Label(p.Box(40), "+1", p.s);
            };
            return a;
        }
        public static Ability Fortify(Unit parent)
        {
            Ability a = new Ability(parent, "Fortify", 4, new Price(1, 1));
            a.Desc = () =>
            {
                return "Health +10/10" +
                "\nDefense + 1" +
                "\nAttack range +1" +
                "\nAttack damage +4" +
                "\nForget 'Move'" +
                "\nLearn 'Mortar'";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                EffectSet nextEffects = new EffectSet();
                nextEffects.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.MaxHealth, 10));
                nextEffects.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.Health, 10));
                nextEffects.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.Defense, 1));
                EffectQueue.Add(nextEffects);

                a.Parent.Arsenal.Remove("Tread");
                a.Parent.Arsenal.Replace("Shoot", Ability.Shoot(a.Parent, 4, 22));
                a.Parent.Arsenal.Replace("Fortify", Ability.Mobilize(a.Parent));
                a.Parent.Arsenal.Add(Ability.Mortar(a.Parent));
                a.Parent.Arsenal.Sort();
            };
            return a;
        }

        #endregion

        #region //G-M

        public static Ability GammaBurst(Unit parent)
        {
            Ability a = new Ability(parent, "Gamma Burst", 4, new Price(2, 1), 16);
            a.Desc = () => { return "Do " + a.Damage + " damage to all units in Target direction"; };
            a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 20));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.LaserLine(new Source(a.Parent), (Unit)Targets[0], a.Damage, 1));
            return a;
        }
        public static Ability Grow(Unit parent)
        {
            Ability a = new Ability(parent, "Grow", 2, Price.Cheap);
            a.Desc = () =>
            {
                return "Switch cells with Target Destructible. " +
                "\nRange +1 per focus.  " +
                "\n" + a.Parent + " +1 Focus.";
            };
            a.Aims.Add(Aim.AttackPath(TargetFilter.Dest, 1));
            a.MainEffects = Targets =>
            {
                a.Parent.AddStat(new Source(a.Parent), Stats.Focus, 1, false);
                Token t = (Token)Targets[0];
                a.Parent.Body.Swap(t);
                a.Unadjust();
            };
            a.Adjust = () => a.Aims[0].Range += a.Parent.FP;
            a.Unadjust = () => a.Aims[0].Range = 1;
            a.DrawAims = (p) =>
            {
                Aim actual = Aim.MovePath(a.Aims[0].Range + a.Parent.FP);
                actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
                float descH = (p.H - (p.LineH * 2)) / p.H;
                a.DrawSpecial(new Panel(p.TallWideBox(descH), p.LineH, p.s));
            };
            return a;
        }

        public static Ability HourSaviour(Unit parent)
        {
            Ability a = new Ability(parent, "Hour Saviour", 4, new Price(0, 2));
            a.Desc = () => { return "Target Unit shifts to the bottom of the Queue"; };
            a.Aims.Add(Aim.Free(TargetFilter.Unit, EPurp.ATTACK));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                int last = TurnQueue.Count - 1;
                int current = TurnQueue.IndexOf(Target);
                int magnitude = 0 - (last - current);
                EffectQueue.Add(Effect.Shift(new Source(a.Parent), Target, magnitude));
            };
            return a;
        }

        public static Ability IceBlast(Unit parent)
        {
            Ability a = new Ability(parent, "Ice Blast", 4, new Price(1, 1), 20);
            a.Desc = () => { return "Target Unit takes " + a.Damage + " damage and loses 2 Initiative for 2 turns."; };
            a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 2));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), Target, a.Damage));
                EffectQueue.Add(Effect.AddStat(new Source(a.Parent), Target, Stats.Initiative, -2));
                Target.timers.Add(Timer.IceBlast(new Source(a.Parent), Target));
            };
            return a;
        }


        public static Ability Land(Unit parent)
        {
            Ability a = new Ability(parent, "Land", 4, new Price(1, 1));
            a.Desc = () =>
            {
                return "Becomes trampling ground unit. " +
                "\nMove range -2 " +
                "\nDefense +2" +
                "\nForget 'Create Rook' " +
                "\nLearn 'Tail Whip'";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                TokenSet tokens = a.Parent.Body.Cell.Occupants;
                tokens -= TargetFilter.Plane(Plane.Ground, true);
                foreach (Token t in tokens)
                    if (t.Body.Destructible)
                        EffectQueue.Add(Effect.DestroyObstacle(new Source(a.Parent), t));
                EffectQueue.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.Defense, 2));
                a.Parent.Plane = Plane.Ground;

                Cell cell = a.Parent.Body.Cell;
                a.Parent.Body.Exit();
                a.Parent.Body.Enter(cell);

                a.Parent.Body.Trample = true;

                a.Parent.Arsenal.Replace("Move", Ability.Move(a.Parent, 3));
                a.Parent.Arsenal.Replace("Land", Ability.TakeFlight(a.Parent));
                a.Parent.Arsenal.Replace("Build Rook", Ability.TailWhip(a.Parent));
                a.Parent.Arsenal.Sort();
            };

            a.Restrict = () =>
            {
                TokenSet tokens = a.Parent.Body.Cell.Occupants;
                tokens -= TargetFilter.Plane(Plane.Ground, true);
                if (tokens.Count < 1) return false;
                if (tokens.Count == 1)
                    foreach (Token t in tokens)   
                        if (t.Body.Destructible) return false;
                return true;
            };
            return a;
        }
        public static Ability LaserShot(Unit parent)
        {
            Ability a = new Ability(parent, "Laser Shot", 3, Price.Cheap, 12);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to all units in Target cell." +
                "\nIf there are no obstacles in Target cell, do reduce damage 50% (rounded up) " +
                "and damage all units in the next occupied cell in the same direction.  " +
                "Repeat until damage is 1 or an obstacle is hit.";
            };
            a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 3));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.LaserLine(new Source(a.Parent), (Unit)Targets[0], a.Damage, 50));
            return a;
        }
        public static Ability LaserSpin(Unit parent)
        {
            Ability a = new Ability(parent, "Laser Spin", 4, new Price(1, 1), 10);
            a.Desc = () =>
            {
                return "Select Target Unit and a direction (clockwise or counterclockwise)." +
                "\nDo " + a.Damage + " damage to Target unit and its cellmates," +
                "\nHalf damage (rounded down) to Units in the next cell in the direction." +
                "\nHalf of that damage to Units in the next cell, and so on," +
                "\nuntil damage is less than 1 or a cell contains a non-Sunken Obstacle.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.Aims.Add(Aim.Radial(TargetFilter.Cell));
            a.MainEffects = Targets =>
            {
                Cell center = a.Parent.Body.Cell;
                Unit first = (Unit)Targets[0];
                Cell start = first.Body.Cell;
                Cell next = (Cell)Targets[1];
                NeighborMatrix neighbors = new NeighborMatrix(center);
                CellSet ring = neighbors.Ring(start, next);
                EffectQueue.Add(Effect.LaserInitial(new Source(a.Parent), ring, a.Damage, 50));
            };
            return a;
        }
        public static Ability Lob(Unit parent, int range, int damage)
        {
            Ability a = new Ability(parent, "Lob", 3, Price.Cheap, damage, range);
            a.Desc = () => { return "Do " + a.Damage + " damage to Target unit."; };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Unit, 0, a.Modifier));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.DAMAGE);
                GUI.Box(box, Icons.DMG(), p.s);
                p.NudgeX();
                GUI.Label(p.Box(30), a.Damage + "", p.s);
            };
            return a;
        }
        
        public static Ability Maul(Unit parent)
        {
            Ability a = new Ability(parent, "Maul", 3, new Price(0, 1), 12);
            a.Desc = () => { return "Do " + a.Damage + " damage to Target unit."; };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            return a;
        }
        public static Ability MinuteWaltz(Unit parent)
        {
            Ability a = new Ability(parent, "Minute Waltz", 4, new Price(1, 1));
            a.Desc = () =>
            {
                return "Shuffle the Queue." +
                "\n(End " + a.Parent.ID.Name + "'s turn.)";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                EffectQueue.Add(Effect.Shuffle(new Source(a.Parent)));
                EffectQueue.Add(Effect.Advance(new Source(a.Parent), false));
            };
            return a;
        }
        public static Ability MneumonicPlague(Unit parent)
        {
            Ability a = new Ability(parent, "Mneumonic Plague", 3, Price.Cheap, 7);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to all enemy cellmates. " +
                "\nGain health equal to damage successfully dealt.";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                TokenSet units = a.Parent.Body.CellMates
                    - TargetFilter.Unit
                    - TargetFilter.Owner(a.Parent.Owner, false);
                EffectSet effects = new EffectSet();
                foreach (Unit u in units)
                    effects.Add(Effect.Leech(new Source(a.Parent), u, a.Damage));
                EffectQueue.Add(effects);
            };
            return a;
        }
        public static Ability Mobilize(Unit parent)
        {
            Ability a = new Ability(parent, "Mobilize", 4, new Price(1, 1));
            a.Desc = () =>
            {
                return "Health -10/10" +
                "\nDefense -1" +
                "\nAttack range -1" +
                "\nAttack damage -4" +
                "\nLearn 'Move'" +
                "\nForget 'Mortar'";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                EffectSet nextEffects = new EffectSet();
                nextEffects.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.MaxHealth, -10));
                nextEffects.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.Health, -10));
                nextEffects.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.Defense, -1));
                EffectQueue.Add(nextEffects);

                a.Parent.Arsenal.Add(Ability.Tread(a.Parent));
                a.Parent.Arsenal.Replace("Shoot", Ability.Shoot(a.Parent, 3, 18));
                a.Parent.Arsenal.Replace("Mobilize", Ability.Fortify(a.Parent));
                a.Parent.Arsenal.Remove("Mortar");
                a.Parent.Arsenal.Sort();
            };
            return a;
        }
        public static Ability Mortar(Unit parent)
        {
            Ability a = new Ability(parent, "Mortar", 4, new Price(2, 1), 18);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to all units in Target cell. " +
                "\nAll units in neighboring cells take 50% damage (rounded down). " +
                "\nDamage continues to spread outward with 50% reduction until 1. " +
                "\nDestroy all destructible tokens that would take damage." +
                "\nRange +1 per Focus (up to 3)";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Cell, 2, 3));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.ExplosionSequence(new Source(a.Parent), (Cell)Targets[0], a.Damage, false));
            a.Adjust = () => a.Aims[0].Range += Mathf.Min(a.Parent.FP, 3);
            a.Unadjust = () => a.Aims[0].Range -= Mathf.Min(a.Parent.FP, 3);

            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.EXP);
                GUI.Box(box, Icons.EXP(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
                p.NextLine();
                GUI.Label(p.LineBox, "Max Range +1 per Focus (up to +3).");
            };
            return a;
        }
    
        #endregion

        #region //N-R

        public static Ability Oasis(Unit parent)
        {
            Ability a = new Ability(parent, "Oasis", 3, Price.Cheap, 7);
            a.Desc = () =>
            {
                return "All friendly cellmates +" + a.Damage + " health. " +
                "\nLose health equal to health successfully given.";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                TokenSet units = a.Parent.Body.CellMates
                    - TargetFilter.Unit
                    - TargetFilter.Owner(a.Parent.Owner, true);
                EffectSet effects = new EffectSet();
                foreach (Unit u in units)
                    effects.Add(Effect.Donate(new Source(a.Parent), u, a.Damage));
                EffectQueue.Add(effects);
            };
            return a;
        }

        public static Ability Petrify(Unit parent)
        {
            Ability a = new Ability(parent, "Petrify", 4, new Price(1, 1), 15);
            a.Desc = () => { return "Target Unit takes " + a.Damage + " damage and cannot move on its next turn."; };
            a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 2));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), Target, a.Damage));
                if (Target.Arsenal.Move != default(Ability))
                {
                    Ability move = Target.Arsenal.Move;
                    Target.timers.Add(Timer.Petrified(new Source(a.Parent), Target, move));
                    Target.Arsenal.Remove(move);
                }
            };
            return a;
        }
        public static Ability Pierce(Unit parent, Price price, int damage)
        {
            Ability a = new Ability(parent, "Pierce", 4, price, damage);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit (ignore defense).  " +
                "\nMax range +1 per focus (up to +3).";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Unit, 2, 3));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Pierce(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            a.Adjust = () => a.Aims[0].Range += Mathf.Min(a.Parent.FP, 3);
            a.Unadjust = () => a.Aims[0].Range -= Mathf.Min(a.Parent.FP, 3);
            return a;
        }
        public static Ability PlantGrenade(Unit parent)
        {
            Ability a = new Ability(parent, "Plant Grenade", 4, Price.Cheap, 10);
            a.Desc = () =>
            {
                return "At the end of Target Unit's next turn, do " + a.Damage + " damage to all units in its cell. " +
                "\nAll units in neighboring cells take 50% damage (rounded down). " +
                "\nDamage continues to spread outward with 50% reduction until 1. " +
                "\nDestroy all destructible tokens that would take damage.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                Target.timers.Add(Timer.ActiveGrenade(new Source(a.Parent), Target));
            };
            a.DrawSpecial = (p) =>
            {
                GUI.Label(p.LineBox, "Attach timer to Target:");
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.EXP);
                GUI.Box(box, Icons.EXP(), p.s);
                p.NudgeX();
                GUI.Label(p.Box(0.9f), a.Damage + " at end of Target's next turn.");
            };
            return a;
        }
        public static Ability PsiBeam(Unit parent)
        {
            Ability a = new Ability(parent, "Psi Beam", 4, new Price(1, 1), 12);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit." +
                "\nTarget loses all Focus.";
            };
            a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 3));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), Target, a.Damage));
                EffectQueue.Add(Effect.AddStat(new Source(a.Parent), Target, Stats.Focus, 0 - Target.FP));
            };
            return a;
        }
        
        public static Ability Quickdraw(Unit parent)
        {
            Ability a = new Ability(parent, "Quickdraw", 4, new Price(0, 1), 6);
            a.multiTarget = true;
            a.Desc = () =>
            {
                return "Once per Focus (Max: 5), select and deal " + a.Damage + " damage to Target unit." +
                "\n(You may choose the same Target multiple times.)" +
                "\nLose all Focus.";
            };
            a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 3));
            a.MainEffects = Targets =>
            {
                for (int i = 0; i < Targets.Count; i++)
                    EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[i], a.Damage));
                a.Parent.SetStat(new Source(a.Parent), Stats.Focus, 0);
            };

            a.Adjust = () =>
            {
                int shots = Mathf.Min(a.Parent.FP, 5);
                for (int i = 2; i <= shots; i++)
                    a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 3));
            };
            a.Unadjust = () =>
                a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 3));

            a.DrawSpecial = (p) =>
            {
                int shots = Mathf.Min(a.Parent.FP, 5);
                for (int i = 2; i <= shots; i++)
                    a.Aims[0].Draw(new Panel(p.LineBox, p.LineH, p.s));
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }

        public static Ability Rage(Unit parent, int damage)
        {
            Ability a = new Ability(parent, "Rage", 3, Price.Cheap, damage);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit. " +
                "\n" + a.Parent + " takes 50% damage (rounded down).";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Rage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.DAMAGE);
                GUI.Box(box, Icons.DMG(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
                p.NextLine();
                box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.DAMAGE);
                GUI.Box(box, Icons.DMG(), p.s);
                p.NudgeX();
                int selfDamage = (int)Mathf.Floor(a.Damage * 0.5f);
                GUI.Label(p.Box(0.9f), selfDamage + " to self.");
            };
            return a;
        }
        public static Ability Refract(Unit parent)
        {
            Ability a = new Ability(parent, "Refract", 4, new Price(1, 1), 10);
            a.Desc = () =>
            {
                return "50% chance of missing Target." +
                "\nDo " + a.Damage + " damage to all units in Target cell." +
                "\nIf there are no obstacles in Target cell, do reduce damage 50% (rounded up) " +
                "and damage all units in the next occupied cell in the same direction.  " +
                "Repeat until damage is 1 or an obstacle is hit.";
            };
            a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 3));
            a.MainEffects = Targets =>
            {
                int flip = DiceCoin.Throw(new Source(a.Parent), EDice.COIN);
                if (flip == 1)
                    EffectQueue.Add(Effect.LaserLine(new Source(a.Parent), (Unit)Targets[0], a.Damage, 50));
                else
                {
                    EffectQueue.Add(Effect.Miss(new Source(a.Parent), a.Parent));
                    GameLog.Out(a.Parent + " attempts to Refract and misses.");
                }
            };
            return a;
        }
        public static Ability Repair(Unit parent)
        {
            Ability a = new Ability(parent, "Repair", 4, new Price(0, 2), 10);
            a.Desc = () =>
            {
                return "Target unit gains " + a.Damage + " health." +
                "\n(Can Target self.)";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Unit, 0, 2));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.AddStat(new Source(a.Parent), (Unit)Targets[0], Stats.Health, a.Damage));
            return a;
        }

        #endregion

        #region //S-Z

        public static Ability SecondInCommand(Unit parent)
        {
            Ability a = new Ability(parent, "Second in Command", 4, new Price(0, 2));
            a.Desc = () =>
            {
                return "Target unit takes the next turn." +
                "\n(Cannot Target self.)";
            };
            TargetFilter f = TargetFilter.Unit + FilterTests.SpecificTarget(a.Parent, false);
            a.Aims.Add(Aim.Free(f, EPurp.ATTACK));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                int magnitude = TurnQueue.IndexOf(Target) - 1;
                EffectQueue.Add(Effect.Shift(new Source(a.Parent), Target, magnitude));
            };
            return a;
        }
        public static Ability Seed(Unit parent)
        {
            Ability a = new Ability(parent, "Seed", 5, new Price(1, 1), Species.Lichenthrope);
            a.Desc = () => { return "Replace Target non-Remains destructible with Lichenthrope."; };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Dest, 0, 2));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Replace(new Source(a.Parent), (Token)Targets[0], Species.Lichenthrope));
            a.DrawSpecial = (p) =>
            {
                InspectorInfo.InspectTemplateButton(a.Template, p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }
        public static Ability Shock(Unit parent)
        {
            Ability a = new Ability(parent, "Shock", 3, Price.Cheap, 10, 5);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit. " +
                "\nTarget is stunned for " + a.Modifier + " turns.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Shock(new Source(a.Parent), (Unit)Targets[0], a.Damage, a.Modifier));
            return a;

        }
        public static Ability Shoot(Unit parent, int range, int damage)
        {
            Ability a = new Ability(parent, "Shoot", 3, Price.Cheap, damage, range);
            a.Desc = () => { return "Do " + a.Damage + " damage to Target unit."; };
            a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, range));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.DAMAGE);
                GUI.Box(box, Icons.DMG(), p.s);
                p.NudgeX();
                GUI.Label(p.Box(30), a.Damage + "", p.s);
            };
            return a;
        }
        public static Ability Shove(Unit parent)
        {
            Ability a = new Ability(parent, "Shove", 4, new Price(1, 1), 12, 5);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit." +
                "\nKnockback " + a.Modifier + " (Move Target in a line away from " + a.Parent + ", up to " + a.Modifier + " cells.)" +
                "\nTarget takes 2 damage per cell knocked back.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.UnitDest));
            a.MainEffects = Targets =>
            {
                EffectSet e = new EffectSet();
                e.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
                e.Add(Effect.Knockback(new Source(a.Parent), (Unit)Targets[0], a.Modifier, 2));
                EffectQueue.Add(e);
            };
            return a;
        }
        public static Ability Slam(Unit parent)
        {
            Ability a = new Ability(parent, "Slam", 4, new Price(2, 0), 8);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit and each of its neighbors and cellmates.  " +
                "\nRange +1 per focus (up to +3).  " +
                "\n" + a.Parent + " loses all focus.";
            };
            a.Aims.Add(Aim.AttackPath(TargetFilter.Unit, 1));
            a.MainEffects = Targets =>
            {
                a.Parent.SetStat(new Source(a.Parent), Stats.Focus, 0, false);
                Unit Target = (Unit)Targets[0];
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), Target, a.Damage));
                TokenSet neighbors = Target.Body.Neighbors(true) - TargetFilter.Unit;
                EffectSet nextEffects = new EffectSet();
                foreach (Unit u2 in neighbors)
                    nextEffects.Add(Effect.Damage(new Source(a.Parent), u2, a.Damage));
                EffectQueue.Add(nextEffects);
                a.Unadjust();
            };
            a.Adjust = () => a.Aims[0].Range += Mathf.Min(a.Parent.FP, 3);
            a.Unadjust = () => a.Aims[0].Range = 1;
            a.DrawAims = (p) =>
            {

                Aim actual = Aim.AttackPath(TargetFilter.Unit, a.Aims[0].Range + Mathf.Min(3, a.Parent.FP));
                actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
                float descH = (p.H - (p.LineH * 2)) / p.H;
                a.DrawSpecial(new Panel(p.TallWideBox(descH), p.LineH, p.s));
            };
            return a;
        }
        public static Ability Sooth(Unit parent)
        {
            Ability a = new Ability(parent, "Sooth", 4, new Price(1, 1), 10);
            a.Desc = () =>
            {
                return "Target teammate gains " + a.Damage + " health." +
                "\n(Cannot Target self.)";
            };
            TargetFilter f = TargetFilter.Unit
                + FilterTests.Owner(a.Parent.Owner, true)
                + FilterTests.SpecificTarget(a.Parent, false);
            a.Aims.Add(Aim.AttackNeighbor(f));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.AddStat(new Source(a.Parent), (Unit)Targets[0], Stats.Health, a.Damage));
            return a;
        }
        public static Ability Sporatic(Unit parent)
        {
            Ability a = new Ability(parent, "Sporatic Emission", 3, Price.Cheap, 12);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit. " +
                "\nTarget recieves " + ((int)Mathf.Floor(a.Damage * 0.5f)) + " corrosion counters." +
                "\n(If a unit has corrosion counters, at the beginning of its turn " +
                "it takes damage equal to the number of counters, " +
                "then removes half the counters (rounded up).)";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Unit, 0, 2));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.CorrodeInitial(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            a.DrawSpecial = (Panel p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.COR);
                GUI.Box(box, Icons.COR(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
            };
            return a;
        }
        public static Ability Sting(Unit parent, int d)
        {
            Ability a = new Ability(parent, "Sting", 3, Price.Cheap);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit. " +
                "\nTarget recieves " + a.Modifier + " corrosion counters." +
                "\n(If a unit has corrosion counters, at the beginning of its turn " +
                "it takes damage equal to the number of counters, " +
                "then removes half the counters (rounded up).)";
            };
            a.Damage = d;
            a.Modifier = (int)Mathf.Floor(d * 0.5f);
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.CorrodeInitial(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.COR);
                GUI.Box(box, Icons.COR(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
            };
            return a;
        }
        public static Ability Strike(Unit parent, int d)
        {
            Ability a = new Ability(parent, "Strike", 3, Price.Cheap);
            a.Desc = () => { return "Do " + a.Damage + " damage to Target unit."; };
            a.Damage = d;
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.DAMAGE);
                GUI.Box(box, Icons.DMG(), p.s);
                p.NudgeX();
                GUI.Label(p.Box(30), a.Damage + "", p.s);
            };
            return a;
        }

        public static Ability TailWhip(Unit parent)
        {
            Ability a = new Ability(parent, "Tail Whip", 4, new Price(1, 1), 10);
            a.Desc = () =>
            {
                return "Select Target Unit and a direction (clockwise or counterclockwise)." +
                "\nDo " + a.Damage + " damage to Target unit and Units in its Cell," +
                "\nand destroy any Destructible tokens in its Cell." +
                "\nDo the same for each Cell in the chosen direciton" +
                "\nuntil a Cell contains a non-Sunken, non-Destructible Obstacle," +
                "\nor all 8 neighboring Cells are hit.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.UnitDest));
            a.Aims.Add(Aim.Radial(TargetFilter.Cell));
            a.MainEffects = Targets =>
            {
                Cell center = a.Parent.Body.Cell;
                Unit first = (Unit)Targets[0];
                Cell start = first.Body.Cell;
                Cell next = (Cell)Targets[1];

                NeighborMatrix neighbors = new NeighborMatrix(center);
                CellSet ring = neighbors.Ring(start, next);

                foreach (Cell cell in ring)
                {
                    TokenSet units = cell.Occupants - TargetFilter.Unit;
                    EffectSet effects = new EffectSet();
                    foreach (Unit u in units)
                        effects.Add(Effect.Damage(new Source(a.Parent), u, a.Damage));
                    TokenSet dests = cell.Occupants - TargetFilter.Dest;
                    foreach (Token t in dests)
                        effects.Add(Effect.DestroyObstacle(new Source(a.Parent), t));
                    EffectQueue.Add(effects);
                    TokenSet obstacles = cell.Occupants - TargetFilter.Ob;
                    bool stop = false;
                    foreach (Token t in obstacles)
                        if (t.Plane.ContainsAny(Plane.Tall)
                            && !t.Body.Destructible)
                            stop = true;
                    if (stop) break;
                }
            };
            return a;
        }
        public static Ability TakeFlight(Unit parent)
        {
            Ability a = new Ability(parent, "Take Flight", 4, new Price(1, 1));
            a.Desc = () =>
            {
                return "Becomes air unit. " +
                "\nMove range +2" +
                "\nDefense -2" +
                "\nForget 'Tail Whip'" +
                "\nLearn 'Create Rook'";
            };
            a.Aims.Add(Aim.Self());
            a.MainEffects = Targets =>
            {
                EffectQueue.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.Defense, -2));
                a.Parent.Plane = Plane.Air;
                Cell cell = a.Parent.Body.Cell;
                a.Parent.Body.Exit();
                a.Parent.Body.Enter(cell);

                a.Parent.Body.Trample = false;

                a.Parent.Arsenal.Replace("Move", Ability.Move(a.Parent, 5));
                a.Parent.Arsenal.Replace("Take Flight", Ability.Land(a.Parent));
                a.Parent.Arsenal.Replace("Tail Whip", Ability.CreateRook(a.Parent));
                a.Parent.Arsenal.Sort();
            };

            a.Restrict = () => 
            {
                TokenSet group = a.Parent.Body.Cell.Occupants;
                group -= TargetFilter.Plane(Plane.Air, true);
                return (group.Count > 0);
            };
            return a;
        }
        public static Ability ThrowGrenade(Unit parent)
        {
            Ability a = new Ability(parent, "ThrowGrenade", 3, new Price(1, 1), 10, 3);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to all units in Target cell. " +
                "\nAll units in neighboring cells take 50% damage (rounded down). " +
                "\nDamage continues to spread outward with 50% reduction until 1. " +
                "\nDestroy all destructible tokens that would take damage.";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Cell, 0, a.Modifier));
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.ExplosionSequence(new Source(a.Parent), (Cell)Targets[0], a.Damage, false));
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.EXP);
                GUI.Box(box, Icons.EXP(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
            };
            return a;
        }
        public static Ability ThrowTerrain(Unit parent)
        {
            Ability a = new Ability(parent, "Throw Terrain", 4, new Price(1, 1), 16);
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Dest));
            a.Aims.Add(Aim.AttackArc(TargetFilter.Unit, 0, 3));
            a.Desc = () =>
            {
                return "Destroy Target non-Remains destructible." +
                "\nDo " + a.Damage + " damage to Target unit.";
            };
            a.MainEffects = Targets =>
            {
                EffectQueue.Add(Effect.DestroyUnit(new Source(a.Parent), (Token)Targets[0]));
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[1], a.Damage));
            };
            return a;
        }
        public static Ability TimeBomb(Unit parent)
        {
            Ability a = new Ability(parent, "Time Bomb", 4, new Price(1, 1), 10);
            a.Desc = () =>
            {
                return "All Units in Target cell take " + a.Damage + " damage and lose 2 Initiative for 2 turns. " +
                "\nAll units in neighboring cells take 50% damage (rounded down) and lose 1 Initiative for 2 turns. " +
                "\nDamage continues to spread outward with 50% reduction until 1. " +
                "\nDestroy all destructible tokens that would take damage.";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Cell, 0, 2));
            a.MainEffects = Targets =>
            {
                Cell c = (Cell)Targets[0];
                EffectQueue.Add(Effect.ExplosionSequence(new Source(a.Parent), c, a.Damage, false));
                EffectSet e = new EffectSet();

                TokenSet affected = c.Occupants - TargetFilter.Unit;
                foreach (Unit u in affected)
                {
                    e.Add(Effect.AddStat(new Source(a.Parent), u, Stats.Initiative, -2));
                    u.timers.Add(Timer.TimeBomb(new Source(a.Parent), u, 2));
                }
                affected = c.Neighbors().Occupants - TargetFilter.Unit;
                foreach (Unit u in affected)
                {
                    e.Add(Effect.AddStat(new Source(a.Parent), u, Stats.Initiative, -1));
                    u.timers.Add(Timer.TimeBomb(new Source(a.Parent), u, 1));
                }
                EffectQueue.Add(e);
            };
            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.EXP);
                GUI.Box(box, Icons.EXP(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage.ToString(), p.s);

                p.NextLine();
                box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.IN);
                GUI.Box(box, Icons.Stats[Stats.Initiative]);
                p.NudgeX();
                GUI.Label(p.Box(0.9f), "-2: Units in Target Cell");

                p.NextLine();
                box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.IN);
                GUI.Box(box, Icons.Stats[Stats.Initiative]);
                p.NudgeX();
                GUI.Label(p.Box(0.9f), "-1: Units in Target Cell's neighbors");
            };
            return a;
        }
        public static Ability TimeMine(Unit parent)
        {
            Ability a = new Ability(parent, "Time Mine", 4, Price.Cheap);
            a.Desc = () =>
            {
                return "Destroy neighboring destructible." +
                "\nIf initative is less than 6, initiative +1.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Dest));
            a.MainEffects = Targets =>
            {
                Token t = (Token)Targets[0];
                Cell c = t.Body.Cell;
                EffectQueue.Add(Effect.DestroyObstacle(new Source(a.Parent), t));
                EffectSet nextEffects = new EffectSet();
                if (a.Parent.IN < 7)
                    nextEffects.Add(Effect.AddStat(new Source(a.Parent), a.Parent, Stats.Initiative, 1));
                if (a.Parent.Body.CanEnter(c))
                    nextEffects.Add(Effect.Move(new Source(a.Parent), a.Parent, c));
                if (nextEffects.Count > 0)
                    EffectQueue.Add(nextEffects);
            };
            return a;
        }
        public static Ability TimeSlam(Unit parent)
        {
            Ability a = new Ability(parent, "Time Slam", 4, Price.Cheap, 15);
            a.Desc = () =>
            {
                return "Target Unit takes " + a.Damage + " damage and loses 2 Initiative for 2 turns." +
                "\n" + a.Parent.ID.Name + " switches cells with Target, if legal.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                EffectSet e = new EffectSet();
                e.Add(Effect.Swap(new Source(a.Parent), a.Parent, Target));
                e.Add(Effect.Damage(new Source(a.Parent), Target, a.Damage));
                e.Add(Effect.AddStat(new Source(a.Parent), Target, Stats.Initiative, -2));
                EffectQueue.Add(e);
                Target.timers.Add(Timer.TimeSlam(new Source(a.Parent), Target));
            };
            return a;
        }
        public static Ability TouchOfDeath(Unit parent)
        {
            Ability a = new Ability(parent, "Touch of Death", 3, Price.Cheap, 16);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit." +
                "\nIf Target has less than 10 health after damage is dealt, destroy Target." +
                "\nBROKEN: If Target is destroyed and is not an Attack King, " +
                "\nit leaves no remains and you may place a Corpse in any cell.";
            };
            a.Aims.Add(Aim.AttackNeighbor(TargetFilter.Unit));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                int oldHP = Target.HP;
                int def = Target.DEF;
                int dmg = a.Damage - def;
                if (oldHP - dmg < 10) { dmg = oldHP; }
                if (dmg >= oldHP)
                {
                    EffectQueue.Add(Effect.DestroyUnit(new Source(a.Parent), Target));
                    Targeter.Start(Ability.Exhume(a.Parent));
                }
                else
                {
                    EffectQueue.Add(Effect.Damage(new Source(a.Parent), Target, a.Damage));
                    Targeter.Reset();
                }
            };
            a.PostEffects = () => { return; };
            return a;
        }

        public static Ability VineWhip(Unit parent)
        {
            Ability a = new Ability(parent, "Vine Whip", 4, new Price(1, 1), 18);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage Target Unit." +
                "\nRange +1 per focus." +
                "\nIf Target is killed and leaves Remains, switch cells with it's Remains.";
            };
            a.Aims.Add(Aim.AttackLine(TargetFilter.Unit, 2));
            a.MainEffects = Targets =>
            {
                Unit Target = (Unit)Targets[0];
                EffectQueue.Add(Effect.Damage(new Source(a.Parent), Target, a.Damage));
                TokenSet group = Target.Body.Cell.Occupants;
                group -= TargetFilter.Dest;
                if (group.Count == 1)
                    a.Parent.Body.Swap((Token)group[0]);
                a.Unadjust();
            };
            a.Adjust = () => a.Aims[0].Range += a.Parent.FP;
            a.Unadjust = () => a.Aims[0].Range = 2;
            a.DrawAims = (p) =>
            {
                Aim actual = Aim.MovePath(a.Aims[0].Range + a.Parent.FP);
                actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
                float descH = (p.H - (p.LineH * 2)) / p.H;
                a.DrawSpecial(new Panel(p.TallWideBox(descH), p.LineH, p.s));
            };
            return a;
        }
        public static Ability Volley(Unit parent)
        {
            Ability a = new Ability(parent, "Volley", 3, Price.Cheap, 12);
            a.Desc = () =>
            {
                return "Do " + a.Damage + " damage to Target unit." +
                "\nMay only be used if neighboring or sharing cell with non-Rook teammate." +
                "\nRange +1 per focus (up to 3).";
            };
            a.Aims.Add(Aim.AttackArc(TargetFilter.Unit, 2, 2));
            a.MainEffects = Targets =>
               EffectQueue.Add(Effect.Damage(new Source(a.Parent), (Unit)Targets[0], a.Damage));
            a.Adjust = () => a.Aims[0].Range += Mathf.Min(a.Parent.FP, 3);
            a.Unadjust = () => a.Aims[0].Range -= Mathf.Min(a.Parent.FP, 3);

            a.Restrict = () =>
            {
                TokenSet neighbors = a.Parent.Body.Neighbors(true)
                    - TargetFilter.Owner(a.Parent.Owner, true)
                    - TargetFilter.Species(Species.Rook, false);
                return (neighbors.Count < 1);
            };

            a.DrawSpecial = (p) =>
            {
                Rect box = p.IconBox;
                if (GUI.Button(box, "")) TipInspector.Inspect(ETip.DAMAGE);
                GUI.Box(box, Icons.DMG(), p.s);
                p.NudgeX();
                GUI.Box(p.Box(30), a.Damage + "", p.s);
                p.NextLine();
                GUI.Label(p.Box(0.9f), "Range +1 per Focus (up to +3).");
            };
            return a;
        }

        public static Ability WebShot(Unit parent)
        {
            Ability a = new Ability(parent, "Web Shot", 4, new Price(1, 1), 12, 0, Species.Web);
            a.Desc = () =>
            {
                return "Create Web in Target cell." +
                "\nAll Units in Target cell take " + a.Damage + " damage.";
            };
            a.Aims.Add(Aim.CreateArc(0, 3));
            a.MainEffects = Targets =>
            {
                Cell c = (Cell)Targets[0];
                EffectQueue.Add(Effect.Create(new Source(a.Parent), c, Species.Web));
                TokenSet occupants = c.Occupants - TargetFilter.Unit;
                foreach (Unit u in occupants)
                    EffectQueue.Add(Effect.Damage(new Source(a.Parent), u, a.Damage));
            };
            a.DrawSpecial = (p) =>
            {
                InspectorInfo.InspectTemplateButton(a.Template, p.LinePanel);
                float descH = (p.H - (p.LineH * 2)) / p.H;
                GUI.Label(p.TallWideBox(descH), a.Desc());
            };
            return a;
        }

        #endregion

       
        
       
        
        
        
        
        
        
        
       
        
        
        
        
        
        
        }
}

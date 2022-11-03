using UnityEngine;

namespace HOA.Actions { 

	public class StickyGrenade : Task {
		
		public override string Desc {get {return "At the end of target Unit's next turn, do "+damage+" damage to all units in its cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage.";} }
		
		int damage = 10;
		
		public StickyGrenade (Unit parent) {
			Name = "Plant";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,0);
			NewAim(Aim.AttackNeighbor(Special.Unit));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit target = (Unit)targets[0];
			target.timers.Add(new TStickyGrenade(target, Parent));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);

			GUI.Label(p.LineBox,"Attach timer to target:");
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.EXP);}
			GUI.Box(box,Icons.EXP(),p.s);
			p.NudgeX();
			GUI.Label(p.Box(0.9f),damage.ToString()+" at end of target's next turn.");
		}
	}

	public class BloodAltar : Task {
		
		public override string Desc {get {return "Destroy neighboring teammate." +
				"\nInitiative +4 for next 2 turns.";} }
		
		public BloodAltar (Unit par) {
			Name = "Blood Altar ";
			Weight = 4;
			Price = new Price(1,0);
			Parent = par;
			NewAim (Aim.AttackNeighbor(Special.Unit));
			Aims[0].TeamOnly = true;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Kill(new Source(Parent), (Token)targets[0]));
			
			Parent.AddStat (new Source(Parent), EStat.IN, 4);
			Parent.timers.Add(new TAltar(Parent));
		}
	}

	public class IceBlast : Task {
		
		public override string Desc {get {return "Target Unit takes "+damage+" damage and loses 2 Initiative for 2 turns.";} }
		
		int damage = 20;
		
		public IceBlast (Unit parent) {
			Name = "Ice Blast";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			NewAim(Aim.AttackLine(Special.Unit, 2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage (new Source(Parent), u, damage));
			
			u.AddStat (new Source(Parent), EStat.IN, -2);
			u.timers.Add(new TBlast(u, Parent));
		}
	}

	public class TimeSlam : Task {
		
		public override string Desc {get {return "Target Unit takes "+damage+" damage and loses 2 Initiative for 2 turns." +
				"\n"+Parent.ID.Name+" switches cells with target, if legal.";} }
		
		int damage = 15;
		
		public TimeSlam (Unit parent) {
			Parent = parent;
			Name = "Time Slam";
			Weight = 4;
			Price = new Price(1,0);
			NewAim(Aim.AttackNeighbor(Special.Unit));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			
			EffectGroup effects = new EffectGroup();
			
			effects.Add(new Effects.Swap(new Source(Parent), Parent, u));
			effects.Add(new Effects.Damage (new Source(Parent), u, damage));
			
			EffectQueue.Add(effects);
			EffectQueue.Add(new Effects.AddStat (new Source(Parent), u, EStat.IN, -2));
			
			u.timers.Add(new TSlam(u, Parent));
		}
	}
	
	
	public class TimeBomb : Task {
		
		public override string Desc {get {return "All Units in target cell take "+damage+" damage and lose 2 Initiative for 2 turns. " +
				"\nAll units in neighboring cells take 50% damage (rounded down) and lose 1 Initiative for 2 turns. " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage.";} }
		
		int damage = 10;
		
		public TimeBomb (Unit u) {
			Name = "Time Bomb";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
			NewAim(Aim.AttackArc(Special.Cell, 0,2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell c = (Cell)targets[0];
			EffectQueue.Add(new Effects.Explosion (new Source(Parent), c, damage));
			EffectGroup nextEffects = new EffectGroup();
			
			TokenGroup affected = c.Occupants.OnlyType(ESpecial.UNIT);
			foreach (Unit u in affected) {
				nextEffects.Add(new Effects.AddStat (new Source(Parent), u, EStat.IN, -2));
				u.timers.Add(new TBomb(u, Parent, 2));
			}
			affected = c.Neighbors().Occupants.OnlyType(ESpecial.UNIT);
			foreach (Unit u in affected) {
				nextEffects.Add(new Effects.AddStat (new Source(Parent), u, EStat.IN, -1));
				u.timers.Add(new TBomb(u, Parent, 1));
			}
			EffectQueue.Add(nextEffects);
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.EXP);}
			GUI.Box(box,Icons.EXP(),p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
			
			p.NextLine();
			box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.IN);}
			GUI.Box(box,Icons.Stat(EStat.IN));
			p.NudgeX();
			GUI.Label(p.Box(0.9f), "-2: Units in target Cell");

			p.NextLine();
			box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.IN);}
			GUI.Box(box,Icons.Stat(EStat.IN));
			p.NudgeX();
			GUI.Label(p.Box(0.9f), "-1: Units in target Cell's neighbors");

		}
	}

	public class ArcticGust : Task {
		
		public override string Desc {get {return "Do "+damage+" damage target Unit." +
				"\nTarget's Move range -2 until end of its next turn." +
					"\nTarget's neighbors and cellmates' Move range -1 until end of their next turn." +
						"\n("+Parent.ID.Name+"'s Move range is not affected.)";} }
		
		int damage = 15;
		
		public ArcticGust (Unit parent) {
			Parent = parent;
			Name = "Arctic Gust";
			Weight = 4;
			Price = new Price(1,1);
			NewAim(Aim.AttackNeighbor(Special.Unit));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage (new Source(Parent), u, damage));
			if (u.Arsenal.Move != default(Task)) {
				Task move = u.Arsenal.Move;
				Aim aim = move.Aims[0];
				aim.Range -= 2;
				u.timers.Add(new TFreeze(u, Parent, move, 2));
			}				
			
			TokenGroup neighborUnits = u.Body.Neighbors().OnlyType(ESpecial.UNIT);
			
			foreach (Token t in neighborUnits) {
				u = (Unit)t;
				if (u != Parent
				    && (u.Arsenal.Move != default(Task))) {
					Task move = u.Arsenal.Move;
					Aim aim = move.Aims[0];
					aim.Range -= 1;
					
					u.timers.Add(new TFreeze(u, Parent, move, 1));
					u.Display.Effect(EEffect.STATDOWN);
				}
			}
		}
	}

	public class Petrify : Task {
		
		int damage = 15;
		
		public override string Desc {get {return "Target Unit takes "+damage+" damage and cannot move on its next turn.";} }
		
		public Petrify (Unit u) {
			Name = "Petrify";
			Weight = 4;
			Parent = u;
			Price = new Price(1,1);
			NewAim(Aim.AttackLine(Special.Unit,2));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage (new Source(Parent), u, damage));
			if (u.Arsenal.Move != default(Task)) {
				Task move = u.Arsenal.Move;
				u.timers.Add(new TPetrify(u, Parent, move));
				u.Arsenal.Remove(move);
			}
		}
	}


}

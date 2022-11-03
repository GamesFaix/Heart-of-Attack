using UnityEngine;

namespace HOA.Actions { 

	public class StickyGrenade : Task {
		
		public override string desc {get {return "At the end of target Unit's next turn, do "+damage+" damage to all units in its cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage.";} }
		
		int damage = 10;
		
		public StickyGrenade (Unit parent) : base(parent) {
			name = "Plant";
			weight = 4;
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit target = (Unit)targets[0];
			target.timers.Add(new TStickyGrenade(target, parent));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);

			GUI.Label(p.LineBox,"Attach timer to target:");
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.EXP);}
			GUI.Box(box, Icons.Effects.explosive, p.s);
			p.NudgeX();
			GUI.Label(p.Box(0.9f),damage.ToString()+" at end of target's next turn.");
		}
	}

	public class BloodAltar : Task {
		
		public override string desc {get {return "Destroy neighboring teammate." +
				"\nInitiative +4 for next 2 turns.";} }
		
		public BloodAltar (Unit parent) : base(parent) {
			name = "Blood Altar ";
			weight = 4;
			aims += Aim.AttackNeighbor(Filters.TeamUnits);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Kill(source, (Token)targets[0]));
			
			parent.AddStat (source, EStat.IN, 4);
			parent.timers.Add(new TAltar(parent));
		}

	}

	public class IceBlast : Task {
		
		public override string desc {get {return "Target Unit takes "+damage+" damage and loses 2 Initiative for 2 turns.";} }
		
		int damage = 20;
		
		public IceBlast (Unit parent) : base(parent) {
			name = "Ice Blast";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackLine(Filters.Units, 2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage (source, u, damage));
			
			u.AddStat (source, EStat.IN, -2);
			u.timers.Add(new TBlast(u, parent));
		}
	}

	public class TimeSlam : Task {
		
		public override string desc {get {return "Target Unit takes "+damage+" damage and loses 2 Initiative for 2 turns." +
				"\n"+parent.ID.Name+" switches cells with target, if legal.";} }
		
		int damage = 15;
		
		public TimeSlam (Unit parent) : base(parent) {
			name = "Time Slam";
			weight = 4;
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			
			EffectGroup effects = new EffectGroup();
			
			effects.Add(new Effects.Swap(source, parent, u));
			effects.Add(new Effects.Damage (source, u, damage));
			
			EffectQueue.Add(effects);
			EffectQueue.Add(new Effects.AddStat (source, u, EStat.IN, -2));
			
			u.timers.Add(new TSlam(u, parent));
		}
	}
	
	
	public class TimeBomb : Task {
		
		public override string desc {get {return "All Units in target cell take "+damage+" damage and lose 2 Initiative for 2 turns. " +
				"\nAll units in neighboring cells take 50% damage (rounded down) and lose 1 Initiative for 2 turns. " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage.";} }
		
		int damage = 10;
		
		public TimeBomb (Unit parent) : base(parent) {
			name = "Time Bomb";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackArc(Filters.Cells, 0,2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell c = (Cell)targets[0];
			EffectQueue.Add(new Effects.Explosion (source, c, damage));
			EffectGroup nextEffects = new EffectGroup();
			
			TokenGroup affected = c.Occupants.units;
			foreach (Unit u in affected) {
				nextEffects.Add(new Effects.AddStat (source, u, EStat.IN, -2));
				u.timers.Add(new TBomb(u, parent, 2));
			}
			affected = c.Neighbors().Occupants.units;
			foreach (Unit u in affected) {
				nextEffects.Add(new Effects.AddStat (source, u, EStat.IN, -1));
				u.timers.Add(new TBomb(u, parent, 1));
			}
			EffectQueue.Add(nextEffects);
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.EXP);}
			GUI.Box(box, Icons.Effects.explosive, p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
			
			p.NextLine();
			box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.IN);}
			GUI.Box(box,Icons.Stats.initiative);
			p.NudgeX();
			GUI.Label(p.Box(0.9f), "-2: Units in target Cell");

			p.NextLine();
			box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.IN);}
			GUI.Box(box,Icons.Stats.initiative);
			p.NudgeX();
			GUI.Label(p.Box(0.9f), "-1: Units in target Cell's neighbors");

		}
	}

	public class ArcticGust : Task {
		
		public override string desc {get {return "Do "+damage+" damage target Unit." +
				"\nTarget's Move range -2 until end of its next turn." +
					"\nTarget's neighbors and cellmates' Move range -1 until end of their next turn." +
						"\n("+parent.ID.Name+"'s Move range is not affected.)";} }
		
		int damage = 15;
		
		public ArcticGust (Unit parent) : base(parent) {
			name = "Arctic Gust";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage (source, u, damage));
			if (u.Arsenal.Move != default(Task)) {
				Task move = u.Arsenal.Move;
				Aim aim = move.aims[0];
				aim.range -= 2;
				u.timers.Add(new TFreeze(u, parent, move, 2));
			}				
			
			TokenGroup neighborUnits = u.Body.Neighbors().units;
			
			foreach (Token t in neighborUnits) {
				u = (Unit)t;
				if (u != parent
				    && (u.Arsenal.Move != default(Task))) {
					Task move = u.Arsenal.Move;
					Aim aim = move.aims[0];
					aim.range -= 1;
					
					u.timers.Add(new TFreeze(u, parent, move, 1));
					u.Display.Effect(EEffect.STATDOWN);
				}
			}
		}
	}

	public class Petrify : Task {
		
		int damage = 15;
		
		public override string desc {get {return "Target Unit takes "+damage+" damage and cannot move on its next turn.";} }
		
		public Petrify (Unit parent) : base(parent) {
			name = "Petrify";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackLine(Filters.Units,2);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Damage (source, u, damage));
			if (u.Arsenal.Move != default(Task)) {
				Task move = u.Arsenal.Move;
				u.timers.Add(new TPetrify(u, parent, move));
				u.Arsenal.Remove(move);
			}
		}
	}


}

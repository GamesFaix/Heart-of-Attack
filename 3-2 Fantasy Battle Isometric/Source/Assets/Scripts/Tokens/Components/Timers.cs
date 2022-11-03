using UnityEngine; 
using System;

namespace HOA { 

	public class TCorrosion : Timer {
		int magnitude;

		public TCorrosion (Unit parent, Token sourceToken, int magnitude) {
			Parent = parent;
			Source = new Source(sourceToken);
			this.magnitude = magnitude;
			Name = "Corrosion";
			Desc = "Take "+magnitude+" damage at end of turn..."; 
			Turns = 1;

		}


		public override void Activate () {
			EffectQueue.Add(new ECorrode2 (new Source(Parent), Parent, magnitude));

			magnitude = (int)Math.Floor(magnitude*0.5f);
			if (magnitude > 0) {Turns++;}
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			p.NudgeY();
			GUI.Label(p.Box(250), magnitude.ToString()+" damage at end of turn. (50% next turn.)");
		}


	}


	public class TSlam : Timer {
		sbyte magnitude = (-2);

		public TSlam (Unit par, Token sourceToken) {
			Parent = par;
			Source = new Source(sourceToken);
			Turns = 2;
			Name = "Time Slammed";
			Desc = Parent.ToString()+" Initiative "+magnitude+" for 2 turns.";
		}
		
		public override void Activate () {
			Parent.AddStat(Source, EStat.IN, (-magnitude));
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);

			p.NudgeX();
			iconSize=20;
			p.NudgeY();
			GUI.Box(p.Box(iconSize), Icons.Stat(EStat.IN), p.s);
			GUI.Label(p.Box(iconSize), magnitude.ToString());
			p.NudgeX();
			p.NudgeX();
			GUI.Label(p.Box(100), "("+Turns+" turns left.)");
		}

	}

	public class TBomb : Timer {
		int magnitude;
		
		public TBomb (Unit par, Token sourceToken, int n) {
			Parent = par;
			Source = new Source(sourceToken);
			magnitude = n;
			Turns = 2;
			Name = "Time Bombed";
			Desc = Parent.ToString()+" Initiative -"+magnitude+" for 2 turns.";
		}
		
		public override void Activate () {
			Parent.AddStat(Source, EStat.IN, magnitude);
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			iconSize=20;
			p.NudgeY();
			GUI.Box(p.Box(iconSize), Icons.Stat(EStat.IN), p.s);
			GUI.Label(p.Box(iconSize), (0-magnitude).ToString());
			p.NudgeX();
			p.NudgeX();
			GUI.Label(p.Box(100), "("+Turns+" turns left.)");
		}
	}

	public class TFreeze : Timer {
		int magnitude;
		public TFreeze (Unit parent, Token sourceToken, Task m, int n) {
			Parent = parent;
			Turns = 1;
			magnitude = n;
			Name = "Arctic Gusted";
			Desc = Parent.ToString()+"'s Move range reduced until end of its next turn.";
		}
		
		public override void Activate () {
			
			if (Parent.Arsenal.Move != default(Task)) {
				Task move = Parent.Arsenal.Move;
				Aim aim = move.Aim[0];
				aim.Range += magnitude;
			}
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			iconSize=20;
			p.NudgeY();
			GUI.Label(p.Box(200), "Move range "+(0-magnitude).ToString()+". ("+Turns+" turns left.)");
		}
	}

	public class TBlast : Timer {

		int magnitude = (-2);

		public TBlast (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 2;
			Name = "Ice Blasted";
			Desc = Parent.ToString()+" Initiative "+magnitude+" for 2 turns.";
		}
		
		public override void Activate () {
			Parent.AddStat(Source, EStat.IN, (0-magnitude));
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			iconSize=20;
			p.NudgeY();
			GUI.Box(p.Box(iconSize), Icons.Stat(EStat.IN), p.s);
			GUI.Label(p.Box(iconSize), magnitude.ToString());
			p.NudgeX();
			p.NudgeX();
			GUI.Label(p.Box(100), "("+Turns+" turns left.)");
		}
	}

	public class TStickyGrenade : Timer {
		int magnitude = 10;

		public TStickyGrenade (Unit par, Token sourceToken) {
			Parent = par;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Active Grenade";
			Desc = "At the end of "+Parent.ToString()+" next turn, do "+magnitude+" damage to all units in its cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
				"\nDamage continues to spread outward with 50% reduction until 1. " +
				"\nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Activate () {
			EffectQueue.Add(new EExplosion(Source, Parent.Body.Cell, magnitude));
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			p.NudgeY();
			GUI.Label(p.Box(250), magnitude.ToString()+" explosive damage at end of next turn.");
		}

	}

	public class TLava : Timer {
		int magnitude = 7;

		public TLava (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Incinerating";
			Desc = "Do "+magnitude+" damage to "+Parent.ToString()+" at the end of its turn if sharing cell with "+Source.Token.ToString()+".";		
		}
		
		public override void Activate () {
			EffectQueue.Add(new EIncinerate(Source, Parent, magnitude));
			Turns++;
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			p.NudgeY();
			GUI.Label(p.Box(200), magnitude.ToString()+" damage at end of turn.");
		}
	}

	public class TCurse : Timer {
		int magnitude = 2;
		
		public TCurse (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Cursed";
			Desc = "Do "+magnitude+" damage to "+Parent.ToString()+" at the end of its turn if sharing cell with "+Source.Token.ToString()+".";		
		}
		
		public override void Activate () {
			EffectQueue.Add(new EDamage(Source, Parent, magnitude));
			Turns++;
		}
		
		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			p.NudgeY();
			GUI.Label(p.Box(200), magnitude.ToString()+" damage at end of turn.");
		}
	}

	public class TExhaust : Timer {
		int magnitude = 5;
		
		public TExhaust (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Exhausted";
			Desc = "Do "+magnitude+" damage to "+Parent.ToString()+" at the end of its turn if sharing cell with "+Source.Token.ToString()+".";		
		}
		
		public override void Activate () {
			EffectQueue.Add(new EIncinerate(Source, Parent, magnitude));
			Turns++;
		}
		
		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			p.NudgeY();
			GUI.Label(p.Box(200), magnitude.ToString()+" damage at end of turn.");
		}
	}

	public class TWater : Timer {
		int magnitude = 5;

		public TWater (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Waterlogged";
			Desc = "Do "+magnitude+" damage to "+Parent.ToString()+" at the end of its turn if sharing cell with "+Source.Token.ToString()+".";		
		}
		
		public override void Activate () {
			EffectQueue.Add(new EWaterlog(Source, Parent, magnitude));
			Turns++;
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			p.NudgeY();
			GUI.Label(p.Box(200), magnitude.ToString()+" damage at end of turn.");
		}

	}

	public class TTarg : Timer {
		int magnitude = 10;
		
		public TTarg (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Targeted";
			Desc = "Do "+magnitude+" explosive damage at "+Parent.ToString()+"'s cell at the end of its turn.";		
		}
		
		public override void Activate () {
			EffectQueue.Add(new EExplosion(Source, Parent.Body.Cell, magnitude));
			Turns++;
		}
		
		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			p.NudgeY();
			GUI.Label(p.Box(200), magnitude.ToString()+" explosive damage at end of turn.");
		}
		
	}

	public class TPetrify : Timer {
		Task move;
		public TPetrify (Unit parent, Token sourceToken, Task m) {
			Parent = parent;
			Turns = 1;
			move = m;
			Name = "Petrified";
			Desc = Parent.ToString()+" cannot move on its next turn.";
		}
		
		public override void Activate () {
			Parent.Arsenal.Add(move);
			Parent.Arsenal.Sort();
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			p.NudgeY();
			GUI.Label(p.Box(200), "Cannot move. ("+Turns+" turns left.)");
		}
	}

	public class TAltar : Timer {
		int magnitude = 4;

		public TAltar (Unit parent) {
			Parent = parent;
			Turns = 2;
			Name = "Blood Altaration";
			Desc = Parent.ToString()+" Initiative +"+magnitude+" for 2 turns.";
		}
		
		public override void Activate () {
			Parent.AddStat(new Source(Parent), EStat.IN, (0-magnitude));
		}

		public override void Display (Panel p, float iconSize) {
			GUI.Label(p.Box(iconSize), Icons.TIMER());
			p.NudgeY();
			GUI.Label(p.Box(100), Name);
			p.NudgeY(false);
			
			p.NudgeX();
			iconSize=20;
			p.NudgeY();
			GUI.Box(p.Box(iconSize), Icons.Stat(EStat.IN), p.s);
			GUI.Label(p.Box(iconSize), "+"+magnitude.ToString());
			p.NudgeX();
			p.NudgeX();
			GUI.Label(p.Box(100), "("+Turns+" turns left.)");
		}
	}
}

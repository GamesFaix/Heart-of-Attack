using UnityEngine; 
using System;

namespace HOA { 

	public class TCorrosion : Timer {
		int magnitude;

		public override string Desc {get {return magnitude+" damage at end of turn. (50% next turn.)";} }

		public TCorrosion (Unit parent, Token sourceToken, int magnitude) {
			Parent = parent;
			Source = new Source(sourceToken);
			this.magnitude = magnitude;
			Name = "Corrosion";
			Turns = 1;
		}

		public override void Activate () {
			EffectQueue.Add(new ECorrode2 (new Source(Parent), Parent, magnitude));

			magnitude = (int)Math.Floor(magnitude*0.5f);
			if (magnitude > 0) {Turns++;}
		}
	}

	public class TSlam : Timer {
		sbyte magnitude = (-2);

		public override string Desc {get {return "Initiative "+magnitude+". ("+Turns+" turns left.)";} }

		public TSlam (Unit par, Token sourceToken) {
			Parent = par;
			Source = new Source(sourceToken);
			Turns = 2;
			Name = "Time Slammed";
		}
		
		public override void Activate () {
			Parent.AddStat(Source, EStat.IN, (-magnitude));
		}
	}

	public class TBomb : Timer {
		int magnitude;

		public override string Desc {get {return "Initiative -"+magnitude+". ("+Turns+" turns left.)";} }

		public TBomb (Unit par, Token sourceToken, int n) {
			Parent = par;
			Source = new Source(sourceToken);
			magnitude = n;
			Turns = 2;
			Name = "Time Bombed";
		}
		
		public override void Activate () {
			Parent.AddStat(Source, EStat.IN, magnitude);
		}
	}

	public class TFreeze : Timer {
		int magnitude;

		public override string Desc {get {return "Move range "+(0-magnitude).ToString()+". ("+Turns+" turns left.)";} }

		public TFreeze (Unit parent, Token sourceToken, Task m, int n) {
			Parent = parent;
			Turns = 1;
			magnitude = n;
			Name = "Arctic Gusted";
		}
		
		public override void Activate () {
			
			if (Parent.Arsenal.Move != default(Task)) {
				Task move = Parent.Arsenal.Move;
				Aim aim = move.Aim[0];
				aim.Range += magnitude;
			}
		}
	}

	public class TBlast : Timer {

		int magnitude = (-2);

		public override string Desc {get {return "Initiative "+magnitude+". ("+Turns+" turns left.)";} }

		public TBlast (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 2;
			Name = "Ice Blasted";
		}
		
		public override void Activate () {
			Parent.AddStat(Source, EStat.IN, (0-magnitude));
		}
	}

	public class TStickyGrenade : Timer {
		int magnitude = 10;

		public override string Desc {get {return magnitude.ToString()+" explosive damage at end of "+Parent+"'s next turn.";} }

		public TStickyGrenade (Unit par, Token sourceToken) {
			Parent = par;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Active Grenade";
		}
		
		public override void Activate () {
			EffectQueue.Add(new EExplosion(Source, Parent.Body.Cell, magnitude));
		}
	}

	public class TLava : Timer {
		int magnitude = 7;

		public override string Desc {get {return magnitude.ToString()+" damage at end of "+Parent+"'s turn.";} }

		public TLava (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Incinerating";
		}
		
		public override void Activate () {
			EffectQueue.Add(new EIncinerate(Source, Parent, magnitude));
			Turns++;
		}
	}

	public class TCurse : Timer {
		int magnitude = 2;

		public override string Desc {get {return magnitude.ToString()+" damage at end of "+Parent+"'s turn.";} }

		public TCurse (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Cursed";
		}
		
		public override void Activate () {
			EffectQueue.Add(new EDamage(Source, Parent, magnitude));
			Turns++;
		}
	}

	public class TExhaust : Timer {
		int magnitude = 5;

		public override string Desc {get {return magnitude.ToString()+" damage at end of "+Parent+"'s turn.";} }

		public TExhaust (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Exhausted";
		}
		
		public override void Activate () {
			EffectQueue.Add(new EIncinerate(Source, Parent, magnitude));
			Turns++;
		}
	}

	public class TWater : Timer {
		int magnitude = 5;

		public override string Desc {get {return magnitude.ToString()+" damage at end of "+Parent+"'s turn.";} }

		public TWater (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Waterlogged";
		}
		
		public override void Activate () {
			EffectQueue.Add(new EWaterlog(Source, Parent, magnitude));
			Turns++;
		}
	}

	public class TTarg : Timer {
		int magnitude = 10;

		public override string Desc {get {return magnitude.ToString()+" explosive damage at end of "+Parent+"'s turn.";} }

		public TTarg (Unit parent, Token sourceToken) {
			Parent = parent;
			Source = new Source(sourceToken);
			Turns = 1;
			Name = "Targeted";
		}
		
		public override void Activate () {
			EffectQueue.Add(new EExplosion(Source, Parent.Body.Cell, magnitude));
			Turns++;
		}
	}

	public class TPetrify : Timer {
		Task move;

		public override string Desc {get {return "Cannot move. ("+Turns+" turns left.)";} }

		public TPetrify (Unit parent, Token sourceToken, Task m) {
			Parent = parent;
			Turns = 1;
			move = m;
			Name = "Petrified";
		}
		
		public override void Activate () {
			Parent.Arsenal.Add(move);
			Parent.Arsenal.Sort();
		}
	}

	public class TAltar : Timer {
		int magnitude = 4;

		public override string Desc {get {return "Initiative +"+magnitude+". ("+Turns+" turns left.)";} }

		public TAltar (Unit parent) {
			Parent = parent;
			Turns = 2;
			Name = "Blood Altaration";
		}
		
		public override void Activate () {
			Parent.AddStat(new Source(Parent), EStat.IN, (0-magnitude));
		}
	}
}

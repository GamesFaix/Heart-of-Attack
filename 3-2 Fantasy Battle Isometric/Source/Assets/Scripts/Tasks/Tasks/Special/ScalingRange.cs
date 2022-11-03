using UnityEngine;

namespace HOA.Actions { 

	public class Cannon : Task {
		
		int damage = 12;
		
		public override string desc {get {return "Do "+damage+" damage to target unit.  " +
				"\nMax range +1 per focus (up to +3).";} }
		
		public Cannon (Unit parent, Price price, int damage) : base(parent) {
			name = "Cannon";
			weight = 3;
			this.price = price;
			aims += Aim.AttackArc(Filters.Units, 2, 3);
			this.damage = damage;
		}
		
		public override void Adjust () {aims[0].range += Mathf.Min(parent.FP, 3);}
		public override void UnAdjust () {aims[0].range -= Mathf.Min(parent.FP, 3);}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage(source, (Unit)targets[0], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.DAMAGE);}
			GUI.Box(box, Icons.Effects.damage, p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
			p.NextLine();
			GUI.Label(p.Box(0.9f), "Max Range +1 per Focus (up to +3).");
		}
	}
	public class Pierce : Task {
		
		int damage = 12;
		
		public override string desc {get {return "Do "+damage+" damage to target unit (ignore defense).  " +
				"\nMax range +1 per focus (up to +3)."; } }
		
		public Pierce (Unit parent, Price price, int damage) : base(parent) {
			name = "Armor Pierce";
			weight = 4;
			this.price = price;
			aims += Aim.AttackArc(Filters.Units, 2, 3);
			this.damage = damage;
		}
		
		public override void Adjust () {aims[0].range += Mathf.Min(parent.FP, 3);}
		public override void UnAdjust () {aims[0].range -= Mathf.Min(parent.FP, 3);}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Pierce (source, (Unit)targets[0], damage));
		}
	}
	public class Mortar : Task {
		
		public override string desc {get {return "Do "+damage+" damage to all units in target cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage." +
						"\nRange +1 per Focus (up to 3)";} }
		
		int minRange, range; 
		int damage =18;
		
		public Mortar (Unit parent) : base(parent) {
			name = "Mortar";
			weight = 4;
			price = new Price(2,1);
			aims += Aim.AttackArc(Filters.Cells, 2, 3);
		}
		
		public override void Adjust () {aims[0].range += Mathf.Min(parent.FP, 3);}
		public override void UnAdjust () {aims[0].range -= Mathf.Min(parent.FP, 3);}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Explosion(source, (Cell)targets[0], damage));
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
			GUI.Label(p.LineBox, "Max Range +1 per Focus (up to +3).");
		}
	}

	public class Volley : Task {
		
		int damage = 12;
		
		public override string desc {get {return "Do "+damage+" damage to target unit." +
				"\nMay only be used if neighboring or sharing cell with non-Rook teammate." +
					"\nRange +1 per focus (up to 3).";} }
		
		public Volley (Unit parent) : base(parent) {
			name = "Volley";
			weight = 3;
			aims += Aim.AttackArc(Filters.Units, 2, 2);
		}
		
		public override void Adjust () {aims[0].range += Mathf.Min(parent.FP, 3);}
		public override void UnAdjust () {aims[0].range -= Mathf.Min(parent.FP, 3);}

		public override bool Restrict () {
			TokenGroup neighbors = parent.Body.Neighbors(true);
			neighbors /= parent.Owner;
			neighbors -= EToken.ROOK;
			if (neighbors.Count > 0) {return false;}
			return true;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage(source, (Unit)targets[0], damage));
		}

		public override void Draw (Panel p) {

			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
		
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.DAMAGE);}
			GUI.Box(box, Icons.Effects.damage, p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
			p.NextLine();
			GUI.Label(p.Box(0.9f), "Range +1 per Focus (up to +3).");
		}
	}
}

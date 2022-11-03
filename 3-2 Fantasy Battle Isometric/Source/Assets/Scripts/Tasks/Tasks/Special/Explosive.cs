using UnityEngine; 

namespace HOA.Actions { 

	public class Throw : Task {
		
		public override string desc {get {return "Do "+damage+" damage to all units in target cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage.";} }
		
		int range = 3;
		int damage = 10;
		
		public Throw (Unit parent) : base(parent) {
			name = "Throw";
			weight = 3;
			price = new Price(1,1);
			aims += Aim.AttackArc(Filters.Cells, 0, range);
		}
		
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
		}
	}

	public class Detonate : Task {
		
		public override string desc {get {return "Destroy all mines on team.";} }
		
		public Detonate (Unit parent) : base(parent) {
			name = "Detonate";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup mines = parent.Owner.OwnedUnits / EToken.MINE;
			foreach (Token t in mines) {t.Die(source);}
		}
	}

}

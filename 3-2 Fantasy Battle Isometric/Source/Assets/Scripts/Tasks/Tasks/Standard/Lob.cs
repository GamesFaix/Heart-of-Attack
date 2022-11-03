using UnityEngine;

namespace HOA.Actions {
	
	public class Lob : Task {
		public override string desc {get {return "Do "+damage+" damage to target unit.";} }

		int damage;
		
		public Lob (Unit parent, int range, int damage) : base(parent) {
			name = "Lob";
			this.damage = damage;
			weight = 3;
			aims += Aim.AttackArc(Filters.Units, 0, range);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage (source, (Unit)targets[0], damage));
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
			GUI.Label(p.Box(30), damage.ToString(), p.s);
		}
	}
}

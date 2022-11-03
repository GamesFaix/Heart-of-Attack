using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA.Actions {

	public class Sting : Task {
		public override string desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget recieves "+cor+" corrosion counters." +
					"\n(If a unit has corrosion counters, at the beginning of its turn " +
						"it takes damage equal to the number of counters, " +
						"then removes half the counters (rounded up).)";} }

		int damage;
		int cor;
		
		public Sting (Unit parent, int damage) : base(parent) {
			name = "Sting";
			this.damage = damage;
			cor = (int)Mathf.Floor(damage*0.5f);
			weight = 3;
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Corrode(source, (Unit)targets[0], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.COR);}
			GUI.Box(box, Icons.Effects.corrosive, p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
		}
	}
}

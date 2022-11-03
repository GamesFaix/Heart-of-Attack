using UnityEngine;
using System.Collections.Generic;

namespace HOA.Actions {

	public class Rage : Task {

		public override string desc {get {return "Do "+damage+" damage to target unit. " +
				"\n"+parent+" takes 50% damage (rounded down)."; } }
		
		int damage;
		
		public Rage (Unit parent, int damage) : base(parent) {
			name = "Rage";
			weight = 3;
			aims += Aim.AttackNeighbor(Filters.Units);
			this.damage = damage;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Rage(source, (Unit)targets[0], damage));
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
			box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.DAMAGE);}
			GUI.Box(box, Icons.Effects.damage, p.s);
			p.NudgeX();
			int selfDamage = (int)Mathf.Floor(damage * 0.5f);
			GUI.Label(p.Box(0.9f),selfDamage.ToString()+" to self.");
		}
	}
}

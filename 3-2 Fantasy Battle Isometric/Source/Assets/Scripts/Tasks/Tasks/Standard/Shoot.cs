using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA.Actions {
	
	public class Shoot : Task {

		public override string Desc {get {return "Do "+damage+" damage to target unit.";} }
		
		int damage;
		
		public Shoot (Unit parent, int range, int d) {
			Parent = parent;
			Name = "Shoot";
			damage = d;
			Weight = 3;
			Price = Price.Cheap;
			NewAim(Aim.AttackLine(Special.Unit, range));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Damage (new Source(Parent), (Unit)targets[0], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.DAMAGE);}
			GUI.Box(box, Icons.DMG() ,p.s);
			p.NudgeX();
			GUI.Label(p.Box(30), damage.ToString(), p.s);
		}
	}
}

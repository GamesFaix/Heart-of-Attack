using UnityEngine;

namespace HOA.Actions {
	public class Focus : Task {

		public override string Desc {get {return "Focus +1.";} }

		public Focus (Unit parent) {
			Parent = parent;
			Name = "Focus";
			Weight = 2;
			Price = Price.Cheap;
			NewAim(Aim.Self());
		}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.AddStat(new Source(Parent), Parent, EStat.FP, 1));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.FP);}
			GUI.Box(box, Icons.Stat(EStat.FP) ,p.s);
			p.NudgeX();
			GUI.Label(p.Box(40), "+1", p.s);
		}
	}
}

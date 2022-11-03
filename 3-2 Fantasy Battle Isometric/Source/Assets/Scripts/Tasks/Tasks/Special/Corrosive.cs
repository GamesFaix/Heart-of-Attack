using UnityEngine; 

namespace HOA.Actions { 

	public class Sporatic : Task {
		int damage = 12;
		
		int Cor {get {return (int)Mathf.Floor(damage*0.5f);} }
		
		public override string desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget recieves "+Cor+" corrosion counters." +
					"\n(If a unit has corrosion counters, at the beginning of its turn " +
						"it takes damage equal to the number of counters, " +
						"then removes half the counters (rounded up).)";} }
		
		public Sporatic (Unit parent) : base(parent){
			name = "Sporatic Emission";
			weight = 3;
			aims += Aim.AttackArc(Filters.Units, 0,2);
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

	public class FatalBlow : Task {
		int damage = 15;
		int Cor {get {return (int)Mathf.Floor(damage*0.5f);} }
		
		public override string desc {get {return "Destroy "+parent+"." +
				"\nDo "+damage+" damage to target unit. " +
					"\nTarget takes "+Cor+" corrosion counters. " +
						"\n(If a unit has corrosion counters, at the beginning of its turn " +
						"it takes damage equal to the number of counters, " +
						"then removes half the counters (rounded up).)";} }
		
		public FatalBlow (Unit parent) : base(parent) {
			name = "Fatal Blow";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new Effects.Corrode (source, u, damage));
			EffectQueue.Add(new Effects.Kill (source, parent));
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
			p.NextLine();
			GUI.Label(p.LineBox, "Destroy "+parent.ToString()+".");
		}
	}

	public class Burst : Task {
		int damage = 12;
		
		int Cor {get {return (int)Mathf.Floor(damage*0.5f);} }
		
		public override string desc {get {return "Destroy "+parent+"." +
				"\nDo "+damage+" damage to cellmates and neighbors. " +
					"\nDamaged units take "+Cor+" corrosion counters. " +
						"\n(If a unit has corrosion counters, at the beginning of its turn " +
						"it takes damage equal to the number of counters, " +
						"then removes half the counters (rounded up).)";
			} } 
		
		public Burst (Unit parent) : base(parent) {
			name = "Burst";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup victims = parent.Body.Neighbors(true).units;
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new Effects.Kill(source, parent));
			foreach (Token t in victims) {
				nextEffects.Add(new Effects.Corrode(source, (Unit)t, damage));	
			}
			EffectQueue.Add(nextEffects);
		}
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);

			GUI.Label(p.LineBox, "All neighbors: ");
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.COR);}
			GUI.Box(box, Icons.Effects.corrosive, p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
			p.NextLine();
			GUI.Label(p.LineBox, "Destroy "+parent+".");
		}
	}


}

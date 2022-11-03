using UnityEngine; 

namespace HOA.Actions { 

	public class Throw : Task {
		
		public override string Desc {get {return "Do "+damage+" damage to all units in target cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage.";} }
		
		int range = 3;
		int damage = 10;
		
		public Throw (Unit parent) {
			Name = "Throw";
			Weight = 3;
			Price = new Price(1,1);
			Parent = parent;
			NewAim(new Aim (ETraj.ARC, ESpecial.CELL, EPurp.ATTACK, range));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Explosion(new Source(Parent), (Cell)targets[0], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.EXP);}
			GUI.Box(box,Icons.EXP(),p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
		}
	}

	public class Detonate : Task {
		
		public override string Desc {get {return "Destroy all mines on team.";} }
		
		public Detonate (Unit u) {
			Name = "Detonate";
			Weight = 4;
			
			Parent = u;
			Price = new Price(1,1);
			NewAim(new Aim(ETraj.GLOBAL, ESpecial.DEST));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup mines = Parent.Owner.OwnedUnits;
			for (int i=mines.Count-1; i>=0; i--) {
				Token t = mines[i];
				if (t.ID.Code != EToken.MINE) {mines.Remove(t);}
			}
			
			foreach (Token t in mines) {t.Die(new Source(Parent));}
		}
	}

}

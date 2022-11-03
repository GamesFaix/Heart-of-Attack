using UnityEngine; 

namespace HOA.Actions { 
	public class Cocktail : Task {
		int damage = 20;
		
		public override string desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget's neighbors and cellmates take 50% damage (rounded down).  " +
					"\nDestroy all destructible tokens that would take damage.";} }
		
		public Cocktail (Unit parent) : base(parent) {
			name = "Cocktail";
			weight = 3;
			price = new Price(1,2);
			aims += Aim.AttackArc(Filters.UnitDest, 0, 3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Fire(source, (Token)targets[0], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);

			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.FIR);}
			GUI.Box(box, Icons.Effects.fire, p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
		}
	}

	public class Firebreathing : Task {
		
		int damage = 10;
		
		public override string desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget's neighbors and cellmates take 50% damage (rounded down).  " +
					"\nDestroy all destructible tokens that would take damage.";} }
		
		public Firebreathing (Unit parent) : base(parent) {
			name = "Firebreathing";
			weight = 3;
			price = new Price(2,0);
			aims += Aim.AttackLine(Filters.UnitDest, 3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Fire(source, (Token)targets[0], damage));
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.FIR);}
			GUI.Box(box, Icons.Effects.fire, p.s);
			p.NudgeX();
			GUI.Box(p.Box(30),damage.ToString(), p.s);
		}
	}
	/*
	public class AMonoFlame : Task {
		int damage;
		
		public AMonoFlame (Unit u) {
			weight = 4;
			Price = new Price(1,2);
			Parent = u;
			
			NewAim(new Aim (ETraj.LINE, new List<ESpecial> {ESpecial.UNIT, ESpecial.DEST}, 2));
			damage = 20;
			
			name = "Eternal Flame";
			desc = "Do "+damage+" damage to target unit. \nTarget's neighbors and cellmates take 50% damage (rounded down). \nDamage continues spreading until less than 1. \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (TargetGroup targets) {
			Charge();
			Token tar = (Token)targets[0];

			TokenGroup affected = new TokenGroup(Parent);
			TokenGroup thisRad = new TokenGroup(tar);
			TokenGroup nextRad = new TokenGroup();
			
			int dmg = damage;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Token next = thisRad[j];
					
					if (!affected.Contains(next)) {		
						next.Display.Effect(EEffect.FIRE);
						AEffects.DamageDest(new Source(Parent), next, dmg);
						
						foreach (Token t in next.Neighbors(true)) {
							nextRad.Add(t);
						}
						affected.Add(next);
					}
				}
				thisRad = nextRad;
				nextRad = new TokenGroup();
				dmg = (int)Mathf.Floor(dmg * 0.5f);
				Targeter.Reset();
			}

		}
	}
	*/


}

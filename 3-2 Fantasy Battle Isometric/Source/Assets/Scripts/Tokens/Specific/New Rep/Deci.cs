using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Decimatrix : Unit {
		public Decimatrix(Source s, bool template=false){
			id = new ID(this, EToken.DECI, s, true);
			plane = Plane.Gnd;
			type.Add(EType.TRAM);
			type.Add(EType.KING);
			onDeath = EToken.HSTE;

			ScaleJumbo();
			health = new HealthPano(this, 85);
			NewWatch(2);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new ADeciMove(this),
				new AShoot(this, 3, 15),
				new APanoPierce(this, new Price(1,1), 15),
				new ADeciMortar(this),
				//new ADeciFortify(this),
				new ACreate(this, new Price(1,0), EToken.DEMO),
				new ACreate(this, new Price(1,1), EToken.MEIN),
				new ACreate(this, new Price(2,2), EToken.PANO)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "Defense +1 per Focus (up to 4).";}
	}

	public class ADeciMove : Task, IMultiMove {

		public override string Desc {get {return "Move "+Parent+" to target cell.";} }

		int range = 3;
		public int Optional () {return 1;}
		
		public ADeciMove (Unit parent) {
			Name = "Move";
			Weight = 1;
			Parent = parent;
			ResetAim();
			Price = Price.Cheap;
		}

		public override void Adjust () {
			int bonus = Parent.FP;
			for (int i=0; i<bonus; i++) {
				aim.Remove(aim[aim.Count-1]);
			}
		}
		
		public override void UnAdjust () {
			ResetAim();
		}

		void ResetAim () {
			aim = new List<Aim>();
			for (int i=0; i<range; i++) {
				Aim a = new Aim(ETraj.NEIGHBOR, EType.CELL, EPurp.MOVE) ;
				AddAim(a);
				//Debug.Log(a);
			}
		}

		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			
			Aim actual = new Aim(ETraj.PATH, EType.CELL, EPurp.MOVE, Mathf.Max(0, range-Parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc);	
		}


	}


	public class ADeciFortify : Task {

		public override string Desc {get {return "Health +10/10" +
				"\nDefense + 1" +
					"\nAttack range +1" +
						"\nAttack damage +4" +
						"\nForget 'Move'" +
						"\nLearn 'Mortar'";} }

		public ADeciFortify (Unit parent) {
			Name = "Fortify";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.MHP, 10));
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.HP, 10));
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.DEF, 1));
			EffectQueue.Add(nextEffects);

			Parent.Arsenal().Remove("Move");
			Parent.Arsenal().Replace("Shoot", new AShoot(Parent, 4, 22));
			Parent.Arsenal().Replace("Fortify", new ADeciMobilize(Parent));
			Parent.Arsenal().Add(new ADeciMortar(Parent));
			Parent.Arsenal().Sort();
		}
	}
	public class ADeciMobilize : Task {

		public override string Desc {get {return "Health -10/10" +
				"\nDefense -1" +
					"\nAttack range -1" +
						"\nAttack damage -4" +
						"\nLearn 'Move'" +
						"\nForget 'Mortar'";} }

		public ADeciMobilize (Unit parent) {
			Name = "Mobilize";
			Weight = 4;
			Parent = parent;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectGroup nextEffects = new EffectGroup();
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.MHP, -10));
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.HP, -10));
			nextEffects.Add(new EAddStat(new Source(Parent), Parent, EStat.DEF, -1));
			EffectQueue.Add(nextEffects);

			Parent.Arsenal().Add(new ADeciMove(Parent));
			Parent.Arsenal().Replace("Shoot", new AShoot(Parent, 3, 18));
			Parent.Arsenal().Replace("Mobilize", new ADeciFortify(Parent));
			Parent.Arsenal().Remove("Mortar");
			Parent.Arsenal().Sort();
		}
	}
	public class ADeciMortar : Task {

		public override string Desc {get {return "Do "+damage+" damage to all units in target cell. " +
				"\nAll units in neighboring cells take 50% damage (rounded down). " +
					"\nDamage continues to spread outward with 50% reduction until 1. " +
						"\nDestroy all destructible tokens that would take damage." +
						"\nRange +1 per Focus (up to 3)";} }

		int minRange, range; 
		int damage =18;
		
		public ADeciMortar (Unit parent) {
			Name = "Mortar";
			Weight = 4;
			Price = new Price(2,1);
			Parent = parent;
			AddAim(new Aim (ETraj.ARC, EType.CELL, EPurp.ATTACK, 3, 2));
		}

		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			aim[0] = new Aim (aim[0].Trajectory, aim[0].Special, aim[0].Range+bonus, aim[0].MinRange);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(ETraj.ARC, EType.UNIT, 3, 2);
		}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EExplosion(new Source(Parent), (Cell)targets[0], damage));
		}
	}
}


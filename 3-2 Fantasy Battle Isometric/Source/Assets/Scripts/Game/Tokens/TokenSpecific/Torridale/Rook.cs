using System.Collections.Generic;
using UnityEngine;

namespace HOA{
	public class Rook : Unit {
		public Rook(Source s, bool template=false){
			id = new ID(this, EToken.ROOK, s, false, template);
			plane = Plane.Gnd;
			onDeath = EToken.ROCK;
			ScaleMedium();
			NewHealth(20,3);
			NewWatch(3);

			Aim aim = new Aim(ETraj.ARC, EClass.UNIT, EPurp.ATTACK, 2, 2);
			arsenal.Add(new ARookVolley(Price.Cheap, this, aim, 12));
			arsenal.Add(new ARookMove(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ARookVolley : Action {
		
		int damage;
		
		public ARookVolley (Price p, Unit u, Aim a, int d) {
			weight = 3;
			actor = u;
			price = p;
			AddAim(a);
			damage = d;
			
			name = "Volley";
			desc = "Do "+d+" damage to target unit.\nMay only be used if neighboring or sharing cell with non-Rook teammate.\nRange +1 per focus (up to 3).";
		}

		public override void Adjust () {
			int bonus = Mathf.Min(actor.FP, 3);
			aim[0] = new Aim (aim[0].AimType, aim[0].TargetClass, aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(ETraj.ARC, EClass.UNIT, EPurp.ATTACK, 2, 2);
		}

		public override bool Restrict () {
			TokenGroup neighbors = actor.Body.Neighbors(true);
			for (int i=neighbors.Count-1; i>=0; i--) {
				Token t = neighbors[i];
				if (t.Owner == actor.Owner 
				    && t.ID.Code != EToken.ROOK) {
					return false;
				}
			}
			return true;
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();

			EffectQueue.Add(new EDamage(new Source(actor), (Unit)targets[0], damage));
			Targeter.Reset();
		}

	}
	
	public class ARookMove : Action, IMultiMove {
		Cell target;
		int range = 2;
		public int Optional () {return 1;}

		public ARookMove (Unit u) {
			weight = 1;
			actor = u;
			price = new Price(0,2);
			name = "Move";
			desc = "Move "+actor+" to target cell.";

			for (int i=0; i<range; i++) {
				Aim a = new Aim(ETraj.NEIGHBOR, EClass.CELL, EPurp.MOVE) ;
				AddAim(a);
				//Debug.Log(a);
			}
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			foreach (ITargetable target in targets) {
				EffectQueue.Add(new EMove(new Source(actor), actor, (Cell)target));
			}
			Targeter.Reset();
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			
			Aim actual = new Aim(ETraj.PATH, EClass.CELL, EPurp.MOVE, range);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc());	
			
			
		}
	}
}

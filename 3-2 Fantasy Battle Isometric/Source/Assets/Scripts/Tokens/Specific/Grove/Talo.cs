using System.Collections.Generic;
using System.Reflection;
using System;

namespace HOA{
	public class TalonedScout : Unit {
		public TalonedScout(Source s, bool template=false){
			id = new ID(this, EToken.TALO, s, false, template);
			plane = Plane.Air;
			ScaleMedium();
			NewHealth(35);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 6),
				new AStrike(this, 12),
				new ATaloGust(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class ATaloGust : Task {

		public override string Desc {get {return "Do "+damage+" damage target Unit." +
				"\nTarget's Move range -2 until end of its next turn." +
					"\nTarget's neighbors and cellmates' Move range -1 until end of their next turn." +
						"\n("+Parent.ID.Name+"'s Move range is not affected.)";} }

		int damage = 15;
		
		public ATaloGust (Unit parent) {
			Parent = parent;
			Name = "Arctic Gust";
			Weight = 4;
			Price = new Price(1,1);
			AddAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage (new Source(Parent), u, damage));
			if (u.Arsenal().Move != default(Task)) {
				Task move = u.Arsenal().Move;
				Aim aim = move.Aim[0];
				aim.Range -= 2;
				u.timers.Add(new TFreeze(u, Parent, move, 2));
			}				

			TokenGroup neighborUnits = u.Body.Neighbors().OnlyType(EType.UNIT);

			foreach (Token t in neighborUnits) {
				u = (Unit)t;
				if (u != Parent
				&& (u.Arsenal().Move != default(Task))) {
					Task move = u.Arsenal().Move;
					Aim aim = move.Aim[0];
					aim.Range -= 1;

					u.timers.Add(new TFreeze(u, Parent, move, 1));
					u.Display.Effect(EEffect.STATDOWN);
				}
			}
		}
	}
	public class TFreeze : Timer {

		int amount;

		public TFreeze (Unit par, Token s, Task m, int n) {
			parent = par;
			turns = 1;

			amount = n;

			name = "Arctic Gusted";
			desc = parent.ToString()+"'s Move range reduced until end of its next turn.";
			
		}
		
		public override void Activate () {

			if (parent.Arsenal().Move != default(Task)) {
				Task move = parent.Arsenal().Move;
				Aim aim = move.Aim[0];
				aim.Range += amount;
			}
		}
	}

}
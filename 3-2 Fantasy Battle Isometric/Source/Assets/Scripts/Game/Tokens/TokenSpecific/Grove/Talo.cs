using System.Collections.Generic;

namespace HOA{
	public class TalonedScout : Unit {
		public TalonedScout(Source s, bool template=false){
			id = new ID(this, EToken.TALO, s, false, template);
			plane = Plane.Air;

			ScaleMedium();
			NewHealth(45);
			NewWatch(4);

			arsenal.Add(new AMovePath(this, 6));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 10));
			arsenal.Add(new ATaloGust(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ATaloGust : Action {

		int damage;
		
		public ATaloGust (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			AddAim(HOA.Aim.Melee());
			damage = 15;
			
			name = "Arctic Gust";
			desc = "Do "+damage+" damage target Unit.\nTarget's Move range -2 until end of its next turn.\nTarget's neighbors and cellmates' Move range -1 until end of their next turn.\n("+actor.ID.Name+"'s Move range is not affected.)";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Unit u = (Unit)targets[0];
			EffectQueue.Add(new EDamage (new Source(actor), u, damage));
			if ((u.Arsenal()[0]) is AMove) {
				AMove move = (AMove)u.Arsenal()[0];
				Aim oldAim = move.Aim[0];
				Aim newAim = new Aim(oldAim.AimType, oldAim.TargetClass, oldAim.Purpose, oldAim.Range-2);

				u.Arsenal().Remove(move);
				u.Arsenal().Add(new AMove(u, newAim));
				u.Arsenal().Sort();

				u.timers.Add(new TFreeze(u, actor, move, 2));
			}				

			TokenGroup neighborUnits = u.Body.Neighbors().OnlyType(EClass.UNIT);

			foreach (Token t in neighborUnits) {
				u = (Unit)t;
				if (u != actor
				&& (u.Arsenal()[0]) is AMove) {
					AMove move = (AMove)u.Arsenal()[0];
					Aim oldAim = move.Aim[0];
					Aim newAim = new Aim(oldAim.AimType, oldAim.TargetClass, oldAim.Purpose, oldAim.Range-1);

					u.Arsenal().Remove(move);
					u.Arsenal().Add(new AMove(u, newAim));
					u.Arsenal().Sort();

					u.timers.Add(new TFreeze(u, actor, move, 1));

					u.Display.Effect(EEffect.STATDOWN);
				}
			}
			Targeter.Reset();
		}
	}
	public class TFreeze : Timer {
		
		AMove move;

		public TFreeze (Unit par, Token s, AMove m, int n) {
			parent = par;
			turns = 1;
			
			move = m;

			name = "Arctic Gusted";
			desc = parent.ToString()+"'s Move range reduced until end of its next turn.";
			
		}
		
		public override void Activate () {

			if ((parent.Arsenal()[0]) is AMove) {
				AMove oldMove = (AMove)parent.Arsenal()[0];
				parent.Arsenal().Remove(oldMove);
				parent.Arsenal().Add(move);
				parent.Arsenal().Sort();
			}
		}
	}

}
using UnityEngine;

namespace HOA.Tokens {
	public class PrismGuard : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new PrismGuard (source, template);
		}

		PrismGuard(Source s, bool template=false){
			ID = new TokenID(this, EToken.PRIS, s, false, template);
			Plane = Plane.Ground;
			ScaleSmall();
			Health = new HealthHalfDodge(this, 15);
			NewWatch(3);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 3),
				Ability.Strike(this, 8),
				Ability.Refract(this)
			});
			Arsenal.Sort();
		}

		
			/*"Actions targetting "+ID.Name+" have a 50% chance of missing.";
		}
		/*
		public override bool Select (Source s) {
			int flip = DiceCoin.Throw(s, EDice.COIN);
			Debug.Log("coin result"+flip);
			if (flip == 1) {
				Display.Effect(AVEffects.HEADS);
				return true;
	//			GUISelectors.Instance = this;
			}
			else {
				GameLog.Out(s.ToString()+" tried to target "+ToString()+" and missed.");
				EffectQueue.Add(new ETails(new Source(this), this));
				return false;
			}
		}
		
		*/
	}
}
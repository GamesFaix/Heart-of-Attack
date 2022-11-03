using System.Collections.Generic;

namespace HOA.Tokens {

	public class Necrochancellor : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Necrochancellor (source, template);
		}

		Necrochancellor(Source s, bool template=false){
			ID = new ID(this, EToken.NECR, s, false, template);
			Plane = Plane.Eth;
			OnDeath = EToken.NONE;
			ScaleMedium();
			NewHealth(30,5);
			NewWatch(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 3),
				new Actions.Defile(this),
				new Actions.TouchOfDeath(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
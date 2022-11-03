using System.Collections.Generic;

namespace HOA.Tokens {

	public class PriestOfNaja : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new PriestOfNaja (source, template);
		}

		PriestOfNaja(Source s, bool template=false){
			ID = new ID(this, EToken.PRIE, s, false, template);
			Plane = Plane.Gnd;
			ScaleLarge();
			NewHealth(50,2);
			NewWatch(4);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 4),
				new Actions.Strike(this, 15),
				new Actions.Shove(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
		











}
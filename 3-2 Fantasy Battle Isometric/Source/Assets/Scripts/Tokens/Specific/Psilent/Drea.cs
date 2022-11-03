using System.Collections.Generic;

namespace HOA.Tokens {
	public class DreamReaver : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new DreamReaver (source, template);
		}

		DreamReaver(Source s, bool template=false){
			ID = new ID(this, EToken.DREA, s, true, template);
			Plane = Plane.Air;
			Special.Add(ESpecial.KING);
			OnDeath = EToken.HGLA;
			ScaleJumbo();
			NewHealth(75,2);
			NewWatch(3);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 4),
				new Actions.PsiBeam(this),
				new Actions.Dislocate(this),

				new Actions.Create(this, Price.Cheap, EToken.PRIS),
				new Actions.Create(this, new Price(1,1), EToken.AREN),
				new Actions.Create(this, new Price(1,2), EToken.PRIE)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}


}

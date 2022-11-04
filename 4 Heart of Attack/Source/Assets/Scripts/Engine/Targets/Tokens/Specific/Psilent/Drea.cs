using System.Collections.Generic;

namespace HOA.Tokens {
	public class DreamReaver : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new DreamReaver (source, template);
		}

		DreamReaver(Source s, bool template=false){
			ID = new TokenID(this, EToken.DREA, s, true, template);
			Plane = Plane.Air;
            TargetClass += TargetClasses.King;
            OnDeath = EToken.HGLA;
			ScaleJumbo();
			NewHealth(75,2);
			NewWatch(3);
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Ability[]{
				Ability.Move(this, 4),
				Ability.PsiBeam(this),
				Ability.Dislocate(this),

				Ability.Create(this, Price.Cheap, EToken.PRIS),
				Ability.CreateAren(this),
				Ability.Create(this, new Price(1,2), EToken.PRIE)
			});
			Arsenal.Sort();
		}

		
	}


}

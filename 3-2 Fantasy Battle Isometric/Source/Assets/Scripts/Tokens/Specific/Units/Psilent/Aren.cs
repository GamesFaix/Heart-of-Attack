using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA.Tokens {

	public class ArenaNonSensus : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new ArenaNonSensus (source, template);
		}

		ArenaNonSensus(Source s, bool template=false){
			ID = new ID(this, EToken.AREN, s, false, template);

			Plane = Plane.Ethereal;
			Body = new BodyAren(this, template);

			OnDeath = EToken.NONE;
			ScaleMedium();
			NewHealth(55,3);
			NewWatch(2);
			BuildArsenal();	
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new Actions.Move(this, 3),
				new Actions.MneumonicPlague (this),
				new Actions.Oasis (this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "EXXXtremely buggy.";}
	}
		



}
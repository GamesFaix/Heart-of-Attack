using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class Demolitia : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Demolitia (source, template);
		}

		Demolitia(Source s, bool template=false){
			ID = new ID(this, EToken.DEMO, s, false, template);
			Plane = Plane.Gnd;

			ScaleSmall();
			NewHealth(30);
			NewWatch(3);
			Wallet = new DEFWallet(this, 2, 4);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[] {
				new AMovePath(this, 3),
				new ADemoThrow(this),
				new ADemoSticky(this)
			});
			Arsenal.Sort();
		}
		public override string Notes () {return "";}
	}
}
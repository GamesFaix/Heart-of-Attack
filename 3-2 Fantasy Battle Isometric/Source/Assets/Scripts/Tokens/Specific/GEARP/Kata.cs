using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Katandroid : Unit {
		public Katandroid(Source s, bool template=false){
			ID = new ID(this, EToken.KATA, s, false, template);
			Plane = Plane.Gnd;
			ScaleSmall();
			NewHealth(25);
			NewWatch(5);	
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new AStrike(this, 8),
				new AKataSprint(this),
				new AKataSpin(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
using UnityEngine;
using System.Collections.Generic;

namespace HOA{
	public class MartianManTrap : Unit {
		public MartianManTrap(Source s, bool template=false){
			ID = new ID(this, EToken.MART, s, false, template);
			Plane = Plane.Gnd;
			Special.Add(EType.TRAM);
			ScaleLarge();
			NewHealth(70);
			NewWatch(4);
			BuildArsenal();
		}	

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Remove("Focus");
			Arsenal.Add(new Task[]{
				new AMartMove(this),
				new AMartGrow(this),
				new AStrike(this, 12),
				new AMartWhip(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}
}
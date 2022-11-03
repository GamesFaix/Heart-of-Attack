using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA{
	public class ArenaNonSensus : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new ArenaNonSensus (source, template);
		}

		ArenaNonSensus(Source s, bool template=false){
			ID = new ID(this, EToken.AREN, s, false, template);

			//if (!template) {
			//	Body = new BodyAren(this);
			//}
			//else {
				Body = new Body(this);
		//	}

			Plane = Plane.Eth;
			OnDeath = EToken.NONE;

			ScaleMedium();
			NewHealth(55,3);
			NewWatch(2);
			BuildArsenal();	
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new AArenLeech (this),
				new AArenDonate (this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "EXXXtremely buggy.";}
	}
		

	public class AArenLeech : Task {
		
		int damage = 7;

		public override string Desc {get {return  "Do "+damage+" damage to all enemy cellmates. " +
				"\nGain health equal to damage successfully dealt.";} }

		public AArenLeech (Unit u) {
			Name = "Leech life";
			Weight = 3;

			Price = new Price(1,0);
			NewAim(HOA.Aim.Self());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup tokens = Parent.Body.CellMates;
			tokens = tokens.OnlyType(EType.UNIT);
			tokens = tokens.RemoveOwner(Parent.Owner);
			foreach (Token t in tokens) {
				EffectQueue.Add(new ELeech(new Source(Parent), (Unit)t, damage));
			}
		}
	}

	public class AArenDonate : Task {
		
		int damage = 7;

		public override string Desc {get {return  "All friendly cellmates +"+damage+" health. " +
				"\nLose health equal to health successfully given.";} }
		
		public AArenDonate (Unit u) {
			Name = "Donate life";
			Weight = 3;

			Price = new Price(1,0);
			NewAim(HOA.Aim.Self());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup tokens = Parent.Body.CellMates;
			tokens = tokens.OnlyType(EType.UNIT);
			tokens = tokens.OnlyOwner(Parent.Owner);
			foreach (Token t in tokens) {
				EffectQueue.Add(new EDonate(new Source(Parent), (Unit)t, damage));
			}
		}
	}
}
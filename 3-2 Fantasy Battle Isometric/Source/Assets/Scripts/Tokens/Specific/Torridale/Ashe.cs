using System.Collections.Generic;

namespace HOA{
	public class Ashes : Unit {
		public Ashes(Source s, bool template=false){
			id = new ID(this, EToken.ASHE, s, false, template);
			plane = Plane.Gnd;
			type.Add(EType.DEST);
			type.Add(EType.REM);
			onDeath = EToken.NONE;
			ScaleSmall();
			NewHealth(15);
			NewWatch(5);
			BuildArsenal();
		}	
		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Remove("Focus");
			arsenal.Add(new AAsheArise(this));
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}	
	
	public class AAsheArise : Task {

		public override string Desc {get {return "Transform "+Parent+" into a Conflagragon." +
				"\n(New Conflagragon starts with "+Parent+"'s health.)";} }

		public AAsheArise (Unit par) {
			Name = "Arise";
			Weight = 4;

			Price = new Price(2,0);
			AddAim(HOA.Aim.Self());
			
			Parent = par;
		}

		public override bool Restrict () {
			Cell c = Parent.Body.Cell;
			if (c.Contains(EPlane.AIR)) {return true;}
			return false;
		}

		protected override void ExecuteMain (TargetGroup targets) {
			int hp = ((Unit)Parent).HP;
			Parent.Die(new Source(Parent), false, false);
			Token newToken;
			TokenFactory.Add(EToken.CONF, new Source(Parent), Parent.Body.Cell, out newToken, false);
			((Unit)newToken).SetStat(new Source(Parent), EStat.HP, hp);
		}
	}
}
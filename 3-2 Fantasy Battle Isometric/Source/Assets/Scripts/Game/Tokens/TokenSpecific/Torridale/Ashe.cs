using System.Collections.Generic;

namespace HOA{
	public class Ashes : Unit {
		public Ashes(Source s, bool template=false){
			id = new ID(this, EToken.ASHE, s, false, template);
			plane = Plane.Gnd;
			type.Add(EClass.DEST);
			type.Add(EClass.REM);
			onDeath = EToken.NONE;
			ScaleSmall();
			NewHealth(15);
			NewWatch(5);
			
			//		arsenal.Add(new AMove(this, AIM.PATH, 0));
			
			arsenal.Add(new AAsheArise(new Price(0,2), this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}	
	
	public class AAsheArise : Action {
		
		Token chiTemplate;
		
		public AAsheArise (Price p, Unit par) {
			weight = 4;
			price = p;
			AddAim(HOA.Aim.Self());
			
			actor = par;
			chiTemplate = TemplateFactory.Template(EToken.CONF);
			
			name = chiTemplate.ID.Name;
			desc = "Transform "+actor+" into a "+name+".\n(New "+name+" starts with "+actor+"'s health.)";
		}

		public override bool Restrict () {
			Cell c = actor.Body.Cell;
			if (c.Contains(EPlane.AIR)) {return true;}
			return false;
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();

			int hp = ((Unit)actor).HP;
			actor.Die(new Source(actor), false, false);
			Token newToken;
			TokenFactory.Add(EToken.CONF, new Source(actor), actor.Body.Cell, out newToken, false);
			((Unit)newToken).SetStat(new Source(actor), EStat.HP, hp);
			Targeter.Reset();
		}
	}
}
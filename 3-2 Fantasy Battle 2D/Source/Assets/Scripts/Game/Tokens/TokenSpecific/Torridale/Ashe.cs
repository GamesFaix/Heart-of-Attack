using System.Collections.Generic;

namespace HOA{
	public class Ashes : Unit {
		public Ashes(Source s, bool template=false){
			NewLabel(EToken.ASHE, s, false, template);
			BuildGround();
			AddRem();
			AddCorpseless();
			
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
			AddAim(HOA.Aim.Self);
			
			actor = par;
			chiTemplate = TemplateFactory.Template(EToken.CONF);
			
			name = chiTemplate.Name;
			desc = "Transform "+actor+" into a "+name+".\n(New "+name+" starts with "+actor+"'s health.)";
		}

		public override bool Restrict () {
			Cell c = actor.Cell;
			if (c.Contains(EPlane.AIR)) {return true;}
			return false;
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();

			Cell cell = actor.Cell;
			int hp = ((Unit)actor).HP;
			actor.Die(new Source(actor), false, false);
			Token newToken;
			TokenFactory.Add(EToken.CONF, new Source(actor), cell, out newToken, false);
			((Unit)newToken).SetStat(new Source(actor), EStat.HP, hp);
			Targeter.Reset();
		}
	}
}
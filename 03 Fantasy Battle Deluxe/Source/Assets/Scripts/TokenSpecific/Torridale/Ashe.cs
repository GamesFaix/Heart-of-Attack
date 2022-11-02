
namespace HOA{
	public class Ashes : Unit {
		public Ashes(Source s, bool template=false){
			NewLabel(TTYPE.ASHE, s, false, template);
			BuildGround();
			AddRem();
			AddCorpseless();
			
			NewHealth(15);
			NewWatch(5);
			
			//		arsenal.Add(new AMove(this, AIM.PATH, 0));
			
			arsenal.Add(new AAsheArise(new Price(0,2), this, TTYPE.CONF));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}	
	
	public class AAsheArise : Action {
		
		TTYPE child;
		Token chiTemplate;
		
		public AAsheArise (Price p, Unit par, TTYPE chi) {
			weight = 4;
			price = p;
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, TTAR.NA);
			
			actor = par;
			child = chi;
			chiTemplate = TemplateFactory.Template(child);
			
			name = chiTemplate.Name;
			desc = "Transform "+actor+" into a "+name+".\n(New "+name+" starts with "+actor+"'s health.)";
		}
		
		public override void Perform () {
			if (Charge()) {
				InputBuffer.Submit(new RAsheArise(new Source(actor), actor, child));
			}
		}
	}

	public class RAsheArise : RInstanceSelect{
		public TTYPE token;
		public RAsheArise (Source s, Token t, TTYPE newT) {source = s; instance = t; token = newT;}
		
		public override void Grant () {
			Cell cell = instance.Cell;
			int hp = ((Unit)instance).HP;
			instance.Die(source, false, false);
			Token newToken;
			TokenFactory.Add(token, source, cell, out newToken, false);
			((Unit)newToken).SetStat(source, STAT.HP, hp);
			Reset();
		}
	}
}
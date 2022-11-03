using System.Collections.Generic;

namespace HOA{
	public class Lichenthrope : Unit {
		public Lichenthrope(Source s, bool template=false){
			NewLabel(EToken.LICH, s, false, template);
			BuildGround();
			AddCorpseless();
			
			NewHealth(15);
			NewWatch(5);
			
			arsenal.Add(new AMovePath(this, 0));
			arsenal.Add(new ALeech("Feed",Price.Cheap, this, Aim.Melee(), 5));
			arsenal.Add(new AEvolve(Price.Cheap, this, EToken.BEES));
			arsenal.Add(new AEvolve(new Price(1,2), this, EToken.MYCO));
			arsenal.Add(new AEvolve(new Price(1,3), this, EToken.MART));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class AEvolve : Action {
		
		EToken child;
		Token chiTemplate;
		
		public AEvolve (Price p, Unit par, EToken chi) {
			weight = 4;
			price = p;
			AddAim(HOA.Aim.Self);
			
			actor = par;
			child = chi;
			chiTemplate = TemplateFactory.Template(child);
			
			name = chiTemplate.Name;
			desc = "Transform "+actor+" into a "+name+".  \n(New "+name+" is added to the end of the Queue and does not retain any of "+actor+"'s attributes.)";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new EReplace(new Source(actor), actor, child));
			Targeter.Reset();
		}
	}
}
using System.Collections.Generic;

namespace HOA{
	public class BlackWinnow : Unit {
		public BlackWinnow(Source s, bool template=false){
			id = new ID(this, EToken.BLAC, s, true, template);
			plane = Plane.Gnd;
			type.Add(EType.KING);
			onDeath = EToken.HSLK;
			ScaleJumbo();
			NewHealth(75);
			NewWatch(3); 
			
			arsenal.Add(new AMovePath(this, 3));
			arsenal.Add(new ACorrode("Bite", Price.Cheap, this, Aim.Melee(), 15));
			arsenal.Add(new ABlacLich(this));
			arsenal.Add(new ABlacWeb(this));
			arsenal.Sort();
		}		
		public override string Notes () {return "";}
	}

	public class ABlacWeb : Action {
		
		
		Cell cell;
		EToken child;
		
		public ABlacWeb (Unit par) {
			weight = 4;
			actor = par;
			childTemplate = TemplateFactory.Template(EToken.WEBB);
			price = new Price(1,1);
			
			AddAim(HOA.Aim.CreateArc(3));
			
			name = "Web Shot";
			desc = "Create "+childTemplate.ID.Name+" in target cell.\nAll Units in target cell take 12 damage.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Cell c = (Cell)targets[0];

			EffectQueue.Add(new ECreate(new Source(actor), EToken.WEBB, c));

			TokenGroup occupants = c.Occupants.OnlyClass(EType.UNIT);
			foreach (Unit u in occupants) {
				EffectQueue.Add(new EDamage(new Source(actor), u, 12));
			}
			Targeter.Reset();

		}
	}

	public class ABlacLich : Action{
		
		public ABlacLich (Unit par) {
			weight = 5;
			actor = par;
			childTemplate = TemplateFactory.Template(EToken.LICH);
			price = Price.Cheap;
			
			AddAim(HOA.Aim.Create());

			name = "Create "+childTemplate.ID.Name+"s";
			desc = "Create "+childTemplate.ID.Name+" in up to two target cells.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new ECreate(new Source(actor), EToken.LICH, (Cell)targets[0]));

			CellGroup cg = actor.Body.Cell.Neighbors();
			bool second = false;
			foreach (Cell c in cg) {
				if (childTemplate.Body.CanEnter(c)) {second = true;}
			}
			if (second) {
				Targeter.Find(new ABlacLich2(actor));
			}
			else {Targeter.Reset();}
		}

	}

	public class ABlacLich2 : Action{
		
		public ABlacLich2 (Unit par) {
			weight = 5;
			actor = par;
			childTemplate = TemplateFactory.Template(EToken.LICH);
			price = Price.Free;
			
			AddAim(HOA.Aim.Create());

			
			name = "Create another "+childTemplate.ID.Name;
			desc = "Create "+childTemplate.ID.Name+" in target cell.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new ECreate(new Source(actor), EToken.LICH, (Cell)targets[0]));
			Targeter.Reset();
		}
		
	}


}
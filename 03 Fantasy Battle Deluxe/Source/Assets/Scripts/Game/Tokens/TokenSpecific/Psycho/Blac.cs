using System.Collections.Generic;

namespace HOA{
	public class BlackWinnow : Unit {
		public BlackWinnow(Source s, bool template=false){
			NewLabel(EToken.BLAC, s, true, template);
			BuildGround();
			AddKing();
			OnDeath = EToken.HSLK;
			
			NewHealth(75);
			NewWatch(3); 
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new ACorrode("Bite", Price.Cheap, this, Aim.Melee(), 15));
			arsenal.Add(new ACreate(new Price(0,0), this, EToken.LICH));
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
			desc = "Create "+childTemplate.Name+" in target cell.\nAll Units in target cell take 12 damage.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Cell c = (Cell)targets[0];

			InputBuffer.Submit(new RCreate(new Source(actor), EToken.WEBB, c));

			TokenGroup occupants = c.Occupants.OnlyClass(EClass.UNIT);
			foreach (Unit u in occupants) {
				InputBuffer.Submit(new RDamage(new Source(actor), u, 12));
			}
			

		}
	}



}
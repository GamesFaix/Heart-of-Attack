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
			NewWallet(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal ();
			arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new ASting(this, 15),
				new ABlacLich(this),
				new ABlacWeb(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class ABlacWeb : Task {
		int damage = 12;

		public override string Desc {get {return "Create Web in target cell." +
				"\nAll Units in target cell take "+damage+" damage.";} }

		public ABlacWeb (Unit par) {
			Name = "Web Shot";
			Weight = 4;
			Parent = par;
			Price = new Price(1,1);
			AddAim(HOA.Aim.CreateArc(3));
			Template = TemplateFactory.Template(EToken.WEBB);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell c = (Cell)targets[0];

			EffectQueue.Add(new ECreate(new Source(Parent), EToken.WEBB, c));

			TokenGroup occupants = c.Occupants.OnlyType(EType.UNIT);
			foreach (Unit u in occupants) {
				EffectQueue.Add(new EDamage(new Source(Parent), u, damage));
			}
		}
	}

	public class ABlacLich : Task{

		public override string Desc {get {return "Create Lichenthropes in up to two target cells.";} }

		public ABlacLich (Unit par) {
			Name = "Create Lichenthropes";
			Weight = 5;

			Parent = par;
			Template = TemplateFactory.Template(EToken.LICH);
			Price = Price.Cheap;
			
			AddAim(HOA.Aim.Create());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECreate(new Source(Parent), EToken.LICH, (Cell)targets[0]));

			CellGroup cg = Parent.Body.Cell.Neighbors();
			bool second = false;
			foreach (Cell c in cg) {
				if (Template.Body.CanEnter(c)) {second = true;}
			}
			if (second) {
				Targeter.Find(new ABlacLich2(Parent));
			}
			else {Targeter.Reset();}
		}
		protected override void ExecuteFinish() {}
	}

	public class ABlacLich2 : Task{

		public override string Desc {get {return "Create Lichenthrope in target cell.";} }

		public ABlacLich2 (Unit par) {
			Name = "Create another Lichenthrope";
			Weight = 5;

			Parent = par;
			Template = TemplateFactory.Template(EToken.LICH);
			Price = Price.Free;
			
			AddAim(HOA.Aim.Create());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ECreate(new Source(Parent), EToken.LICH, (Cell)targets[0]));
		}
		
	}


}
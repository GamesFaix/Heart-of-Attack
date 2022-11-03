using UnityEngine; 

namespace HOA { 

	public class AMonoField : Task {
		int range = 2;
		int damage = 5;
		
		public override string Desc {get {return "Do "+damage+" damage to all units within "+range+" cells of "+Parent.ID.Name+". " +
				"\n"+Parent.ID.Name+" gains Health equal to damage successfully dealt.";} }
		
		public AMonoField (Unit u) {
			Parent = u;
			Name = "Death Field";
			Weight = 4;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			CellGroup zone = Zone(Parent, range);
			TokenGroup affected = zone.Occupants.OnlyType(EType.UNIT);
			affected.Remove(Parent);
			
			foreach (Unit u in affected) {
				EffectQueue.Add(new ELeech(new Source(Parent), u, damage));
			}
		}
		
		CellGroup Zone (Token Parent, int range) {
			Cell start = Parent.Body.Cell;
			
			int startX = start.X-range;
			int endX = start.X+range;
			int startY = start.Y-range;
			int endY = start.Y+range;
			
			CellGroup cells = new CellGroup();
			Cell cell;
			
			for (int i=startX; i<=endX; i++) {
				for (int j=startY; j<=endY; j++) {
					if (Game.Board.HasCell(i,j, out cell)) {cells.Add(cell);}
				}
			}
			return cells;
		}
		
	}
	public class AGateFeast : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nGain health equal to damage successfully dealt.";} }
		
		public AGateFeast (Unit u) {
			Name = "Feast";
			Weight = 3;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Arc(3,2));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ELeech(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class ALichFeed : Task {
		
		int damage = 5;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nGain health equal to damage successfully dealt.";} }
		
		public ALichFeed (Unit u) {
			Name = "Feed";
			Weight = 3;
			
			Price = Price.Cheap;
			NewAim(HOA.Aim.Melee());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ELeech(new Source(Parent), (Unit)targets[0], damage));
		}
	}



}

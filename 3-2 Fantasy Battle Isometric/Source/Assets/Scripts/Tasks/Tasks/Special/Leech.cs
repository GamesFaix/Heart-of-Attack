using UnityEngine; 

namespace HOA.Actions { 

	public class DeathField : Task {
		int range = 2;
		int damage = 5;
		
		public override string Desc {get {return "Do "+damage+" damage to all units within "+range+" cells of "+Parent.ID.Name+". " +
				"\n"+Parent.ID.Name+" gains Health equal to damage successfully dealt.";} }
		
		public DeathField (Unit u) {
			Parent = u;
			Name = "Death Field";
			Weight = 4;
			Price = new Price(1,1);
			NewAim(HOA.Aim.Self());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			CellGroup zone = Zone(Parent, range);
			TokenGroup affected = zone.Occupants.OnlyType(ESpecial.UNIT);
			affected.Remove(Parent);
			
			foreach (Unit u in affected) {
				EffectQueue.Add(new Effects.Leech(new Source(Parent), u, damage));
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
	public class Feast : Task {
		
		int damage = 12;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nGain health equal to damage successfully dealt.";} }
		
		public Feast (Unit u) {
			Name = "Feast";
			Weight = 3;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Arc(3,2));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Leech(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class Feed : Task {
		
		int damage = 5;
		
		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nGain health equal to damage successfully dealt.";} }
		
		public Feed (Unit u) {
			Name = "Feed";
			Weight = 3;
			
			Price = Price.Cheap;
			NewAim(HOA.Aim.Melee());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Leech(new Source(Parent), (Unit)targets[0], damage));
		}
	}

	public class MneumonicPlague : Task {
		
		int damage = 7;
		
		public override string Desc {get {return  "Do "+damage+" damage to all enemy cellmates. " +
				"\nGain health equal to damage successfully dealt.";} }
		
		public MneumonicPlague (Unit u) {
			Name = "Leech life";
			Weight = 3;
			
			Price = new Price(1,0);
			NewAim(HOA.Aim.Self());
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup tokens = Parent.Body.CellMates;
			tokens = tokens.OnlyType(ESpecial.UNIT);
			tokens = tokens.RemoveOwner(Parent.Owner);
			foreach (Token t in tokens) {
				EffectQueue.Add(new Effects.Leech(new Source(Parent), (Unit)t, damage));
			}
		}
	}


}

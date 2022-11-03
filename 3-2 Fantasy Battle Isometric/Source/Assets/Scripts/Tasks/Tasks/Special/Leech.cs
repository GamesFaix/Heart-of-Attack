using UnityEngine; 

namespace HOA.Actions { 

	public class DeathField : Task {
		int range = 2;
		int damage = 5;
		
		public override string desc {get {return "Do "+damage+" damage to all units within "+range+" cells of "+parent.ID.Name+". " +
				"\n"+parent.ID.Name+" gains Health equal to damage successfully dealt.";} }
		
		public DeathField (Unit parent) : base(parent) {
			name = "Death Field";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			CellGroup zone = Zone(parent, range);
			TokenGroup affected = zone.Occupants.units - parent;

			foreach (Unit u in affected) {
				EffectQueue.Add(new Effects.Leech(source, u, damage));
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
		
		public override string desc {get {return "Do "+damage+" damage to target unit. " +
				"\nGain health equal to damage successfully dealt.";} }
		
		public Feast (Unit parent) : base(parent) {
			name = "Feast";
			weight = 3;
			aims += Aim.AttackArc(Filters.Units, 2,3);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Leech(source, (Unit)targets[0], damage));
		}
	}

	public class Feed : Task {
		
		int damage = 5;
		
		public override string desc {get {return "Do "+damage+" damage to target unit. " +
				"\nGain health equal to damage successfully dealt.";} }
		
		public Feed (Unit parent) : base(parent) {
			name = "Feed";
			weight = 3;
			aims += Aim.AttackNeighbor(Filters.Units);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new Effects.Leech(source, (Unit)targets[0], damage));
		}
	}

	public class MneumonicPlague : Task {
		
		int damage = 7;
		
		public override string desc {get {return  "Do "+damage+" damage to all enemy cellmates. " +
				"\nGain health equal to damage successfully dealt.";} }
		
		public MneumonicPlague (Unit parent) : base(parent) {
			name = "Leech life";
			weight = 3;
			aims += Aim.Self();
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup tokens = parent.Body.CellMates;
			tokens = (tokens.units) - parent.Owner;
			EffectGroup effects = new EffectGroup();
			foreach (Token t in tokens) {
				effects.Add(new Effects.Leech(source, (Unit)t, damage));
			}
			EffectQueue.Add(effects);
		}
	}


}

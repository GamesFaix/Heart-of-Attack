using UnityEngine; 

namespace HOA.Actions {

	public class LaserSpin : Task {
		int damage = 10;
		
		public override string desc {get {return 
				"Select target Unit and a direction (clockwise or counterclockwise)." +
				"\nDo "+damage+" damage to target unit and its cellmates," +
				"\nHalf damage (rounded down) to Units in the next cell in the direction." +
				"\nHalf of that damage to Units in the next cell, and so on," +
				"\nuntil damage is less than 1 or a cell contains a non-Sunken Obstacle.";} }
		
		public LaserSpin (Unit parent) : base(parent) {
			name = "Laser Spin";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackNeighbor(Filters.Units);
			aims += Aim.Radial(Filters.Cells);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell center = parent.Body.Cell;
			Unit first = (Unit)targets[0];
			Cell start = first.Body.Cell;
			Cell next = (Cell)targets[1];

			NeighborMatrix neighbors = new NeighborMatrix(center);
			CellGroup ring = neighbors.Ring(start, next);

			EffectQueue.Add(new Effects.Laser(source, ring, damage));
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			DrawAim(1, p.LinePanel);
			GUI.Box(p.Box(30), desc);
		}
	}

	public class TailWhip : Task {
		int damage = 10;
		
		public override string desc {get {return 
				"Select target Unit and a direction (clockwise or counterclockwise)." +
				"\nDo "+damage+" damage to target unit and Units in its Cell," +
				"\nand destroy any Destructible tokens in its Cell." +
				"\nDo the same for each Cell in the chosen direciton" +
				"\nuntil a Cell contains a non-Sunken, non-Destructible Obstacle," +
				"\nor all 8 neighboring Cells are hit.";} }
		
		public TailWhip (Unit parent) : base(parent) {
			name = "Tail Whip";
			weight = 4;
			price = new Price(1,1);
			aims += Aim.AttackNeighbor(Filters.UnitDest);
			aims += Aim.Radial(Filters.Cells);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell center = parent.Body.Cell;
			Unit first = (Unit)targets[0];
			Cell start = first.Body.Cell;
			Cell next = (Cell)targets[1];
			
			NeighborMatrix neighbors = new NeighborMatrix(center);
			CellGroup ring = neighbors.Ring(start, next);

			foreach (Cell cell in ring) {
				TokenGroup units = cell.Occupants.units;
				EffectGroup effects = new EffectGroup();
				foreach (Token t in units) {
					Unit u = (Unit)t;
					effects.Add(new Effects.Damage(source, u, damage));
				}
				TokenGroup dests = cell.Occupants.destructible;
				foreach (Token t in dests) {
					effects.Add(new Effects.Destruct(source, t));
				}
				EffectQueue.Add(effects);
				TokenGroup obstacles = cell.Occupants.obstacles;
				bool stop = false;
				foreach (Token t in obstacles) {
					if ((t.Plane.ground || t.Plane.air) && !(t.TokenType.destructible)) {
						stop = true;
					}
				}
				if (stop) {break;}
			}
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			DrawAim(1, p.LinePanel);
			GUI.Box(p.Box(30), desc);
		}
	}

}

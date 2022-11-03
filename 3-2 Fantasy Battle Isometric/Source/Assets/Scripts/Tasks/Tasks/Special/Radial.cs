using UnityEngine; 

namespace HOA.Actions {

	public class LaserSpin : Task {
		int damage = 10;
		
		public override string Desc {get {return 
				"Select target Unit and a direction (clockwise or counterclockwise)." +
				"\nDo "+damage+" damage to target unit and its cellmates," +
				"\nHalf damage (rounded down) to Units in the next cell in the direction." +
				"\nHalf of that damage to Units in the next cell, and so on," +
				"\nuntil damage is less than 1 or a cell contains a non-Sunken Obstacle.";} }
		
		public LaserSpin (Unit u) {
			Name = "Laser Spin";
			Weight = 4;
			Price = new Price(1,1);
			NewAim(Aim.AttackNeighbor(Special.Unit));
			Aims.Add(Aim.Radial(Special.Cell));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell center = Parent.Body.Cell;
			Unit first = (Unit)targets[0];
			Cell start = first.Body.Cell;
			Cell next = (Cell)targets[1];

			NeighborMatrix neighbors = new NeighborMatrix(center);
			CellGroup ring = neighbors.Ring(start, next);

			EffectQueue.Add(new Effects.Laser(new Source(Parent), ring, damage));
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			DrawAim(1, p.LinePanel);
			GUI.Box(p.Box(30), Desc);
		}
	}

	public class TailWhip : Task {
		int damage = 10;
		
		public override string Desc {get {return 
				"Select target Unit and a direction (clockwise or counterclockwise)." +
				"\nDo "+damage+" damage to target unit and Units in its Cell," +
				"\nand destroy any Destructible tokens in its Cell." +
				"\nDo the same for each Cell in the chosen direciton" +
				"\nuntil a Cell contains a non-Sunken, non-Destructible Obstacle," +
				"\nor all 8 neighboring Cells are hit.";} }
		
		public TailWhip (Unit u) {
			Name = "Tail Whip";
			Weight = 4;
			Price = new Price(1,1);
			Parent = u;
			NewAim(Aim.AttackNeighbor(Special.UnitDest));
			Aims.Add(Aim.Radial(Special.Cell));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell center = Parent.Body.Cell;
			Unit first = (Unit)targets[0];
			Cell start = first.Body.Cell;
			Cell next = (Cell)targets[1];
			
			NeighborMatrix neighbors = new NeighborMatrix(center);
			CellGroup ring = neighbors.Ring(start, next);

			foreach (Cell cell in ring) {
				TokenGroup occupants = cell.Occupants;
				TokenGroup units = occupants.OnlyType(ESpecial.UNIT);
				EffectGroup effects = new EffectGroup();
				foreach (Token t in units) {
					Unit u = (Unit)t;
					effects.Add(new Effects.Damage(new Source(Parent), u, damage));
				}
				TokenGroup dests = occupants.OnlyType(ESpecial.DEST);
				foreach (Token t in dests) {
					effects.Add(new Effects.Destruct(new Source(Parent), t));
				}
				EffectQueue.Add(effects);
				TokenGroup obstacles = occupants.OnlyType(ESpecial.OB);
				bool stop = false;
				foreach (Token t in obstacles) {
					if ((t.Plane.Is(EPlane.GND) || t.Plane.Is(EPlane.AIR)) && !t.Special.Is(ESpecial.DEST)) {
						stop = true;
					}
				}
				if (stop) {break;}
			}
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			DrawAim(1, p.LinePanel);
			GUI.Box(p.Box(30), Desc);
		}
	}

}

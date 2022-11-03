using UnityEngine; 

namespace HOA { 
	public class AKataSpin : Task {
		int damage = 10;
		
		public override string Desc {get {return 
				"Select target Unit and a direction (clockwise or counterclockwise)." +
				"\nDo "+damage+" damage to target unit and its cellmates," +
				"\nHalf damage (rounded down) to Units in the next cell in the direction." +
				"\nHalf of that damage to Units in the next cell, and so on," +
				"\nuntil damage is less than 1 or a cell contains a non-Sunken Obstacle.";} }
		
		public AKataSpin (Unit u) {
			Name = "Laser Spin";
			Weight = 4;
			Price = new Price(1,1);
			NewAim(new HOA.Aim(ETraj.NEIGHBOR, Special.Unit));
			Aim.Add(new HOA.Aim(ETraj.RADIAL, Special.Cell));
			Parent = u;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell center = Parent.Body.Cell;
			Unit first = (Unit)targets[0];
			Cell start = first.Body.Cell;
			Cell next = (Cell)targets[1];

			NeighborMatrix neighbors = new NeighborMatrix(center);
			CellGroup ring = neighbors.Ring(start, next);
			int dmg = damage;

			foreach (Cell cell in ring) {
				TokenGroup occupants = cell.Occupants;
				TokenGroup units = occupants.OnlyType(EType.UNIT);
				EffectGroup effects = new EffectGroup();
				foreach (Token t in units) {
					Unit u = (Unit)t;
					effects.Add(new ELaser2(new Source(Parent), u, dmg));
				}
				EffectQueue.Add(effects);
				TokenGroup obstacles = occupants.OnlyType(EType.OB);
				bool stop = false;
				foreach (Token t in obstacles) {
					if (t.Plane.Is(EPlane.GND) || t.Plane.Is(EPlane.AIR)) {
						stop = true;
					}
				}
				if (stop) {break;}
				if (units.Count > 0) {dmg = (int)Mathf.Floor(dmg*0.5f);}
			}
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			DrawPrice(new Panel(p.Box(150), p.LineH, p.s));
			if (Used) {GUI.Label(p.Box(150), "Used this turn.");}
			p.NextLine();
			DrawAim(0, p.LinePanel);
			DrawAim(1, p.LinePanel);

			//Rect box = p.IconBox;
			//if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.FIR);}
			//GUI.Box(box,Icons.FIR(),p.s);
			//p.NudgeX();
			GUI.Box(p.Box(30), Desc);
		}
	}

	public class AGargTailWhip : Task {
		int damage = 10;
		
		public override string Desc {get {return 
				"Select target Unit and a direction (clockwise or counterclockwise)." +
				"\nDo "+damage+" damage to target unit and Units in its Cell," +
				"\nand destroy any Destructible tokens in its Cell." +
				"\nDo the same for each Cell in the chosen direciton" +
				"\nuntil a Cell contains a non-Sunken, non-Destructible Obstacle," +
				"\nor all 8 neighboring Cells are hit.";} }
		
		public AGargTailWhip (Unit u) {
			Name = "Tail Whip";
			Weight = 4;
			Price = new Price(1,1);
			Parent = u;
			NewAim(new HOA.Aim(ETraj.NEIGHBOR, Special.UnitDest));
			Aim.Add(new HOA.Aim(ETraj.RADIAL, Special.Cell));
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
				TokenGroup units = occupants.OnlyType(EType.UNIT);
				EffectGroup effects = new EffectGroup();
				foreach (Token t in units) {
					Unit u = (Unit)t;
					effects.Add(new EDamage(new Source(Parent), u, damage));
				}
				TokenGroup dests = occupants.OnlyType(EType.DEST);
				foreach (Token t in dests) {
					effects.Add(new EDestruct(new Source(Parent), t));
				}
				EffectQueue.Add(effects);
				TokenGroup obstacles = occupants.OnlyType(EType.OB);
				bool stop = false;
				foreach (Token t in obstacles) {
					if ((t.Plane.Is(EPlane.GND) || t.Plane.Is(EPlane.AIR)) && !t.Special.Is(EType.DEST)) {
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
			
			//Rect box = p.IconBox;
			//if (GUI.Button(box,"")) {TipInspector.Inspect(ETip.FIR);}
			//GUI.Box(box,Icons.FIR(),p.s);
			//p.NudgeX();
			GUI.Box(p.Box(30), Desc);
		}
	}

}

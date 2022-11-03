using UnityEngine;

namespace HOA {

	public enum TargetTypes {Cell, Unit, Obstacle, King, Heart, Destructible, Trample}
	
	public struct TokenType {
		Token parent;
		
		public TokenType (Token parent, bool template=false) {
			this.parent = parent;
			this.template = template;
			destructible = false;
			trample = false;
		}
		
		public bool unit {get {return (parent is Unit);} }
		public bool obstacle {get {return (parent is Obstacle);} }
		public bool king {get {return (parent is King);} }
		public bool heart {get {return (parent is Heart);} }
		public bool destructible;
		public bool trample;
		public bool template {get; set;}

		public bool[] types {get {return new bool[6] {unit,obstacle,king,heart,destructible,trample};} }


		public void Display (Panel p) {
			Rect box = p.IconBox;

			if (unit) {
				if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.Unit);}
				GUI.Box(box, Icons.Types.unit, p.s);

				p.NudgeX();
				box = p.IconBox;

				if (king) {
					if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.King);}
					GUI.Box(box, Icons.Types.king, p.s);

					p.NudgeX();
					box = p.IconBox;
				}
			}

			else if (obstacle) {
				if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.Obstacle);}
				GUI.Box(box, Icons.Types.obstacle, p.s);
			
				p.NudgeX();
				box = p.IconBox;

				if (heart) {
					if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.Heart);}
					GUI.Box(box, Icons.Types.heart, p.s);

					p.NudgeX();
					box = p.IconBox;
				}
			}

			if (destructible) {
				if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.Destructible);}
				GUI.Box(box, Icons.Types.destructible, p.s);
			}

			else if (trample) {
				if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.Trample);}
				GUI.Box(box, Icons.Types.trample, p.s);
			}
		}

	}
}


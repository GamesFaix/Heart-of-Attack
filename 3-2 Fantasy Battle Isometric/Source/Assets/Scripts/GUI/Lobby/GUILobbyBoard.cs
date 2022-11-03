using UnityEngine; 

namespace HOA { 

	public static class GUILobbyMap {

		static Float2 boardSize = new Float2(3,3);
		public static Int2 zoneCount {get {return boardSize.Round;} }
		static Int2 cellCount {get {return zoneCount*Zone.size;} }
		static TileSet tile {get {return TileSet.Chess; } }

		static Map map = Map.MapsCustom[1];
		public static Map Map {
			get {return map;} 
			private set {map = value;}
		}

		public static void Assign () {
			Game.Map = Map;
			if (Map is IMapCustom) {
				Game.Board = ((IMapCustom)Map).Build(zoneCount);
			}
			else {Game.Board = Map.Build();}
		}


		public static void Display (Panel p) {

			GUI.Label(p.LineBox, "Map: "+Map.Name, p.s);

			p.NextLine();
			GUI.Label(p.LineBox, "Preset:");
			foreach (Map m in Map.Maps) {
				if (GUI.Button(p.Box(100), m.Name)) {Map = m;}
				p.NextLine();
			}

			p.NextLine();
			float y3 = p.y2;

			GUI.Label(p.LineBox, "Custom:");
			foreach (Map m in Map.MapsCustom) {
				if (GUI.Button(p.Box(100), m.Name)) {Map = m;}
				p.NextLine();
			}

			Rect box = new Rect(p.x2+200, y3, 200,200);
			BoardSizer(new Panel(box, p.LineH, p.s));
		}


		static void BoardSizer (Panel p) {

			GUI.Label(p.LineBox, "("+cellCount.x+"x"+cellCount.y+")");
			p.NextLine();

			p.x2 += 30;
			p.y2 += 5;
			boardSize.x = GUI.HorizontalSlider(p.Box(180), boardSize.x, Board.MinZones.x, Board.MaxZones.x);
			p.NextLine();
			boardSize.y = GUI.VerticalSlider(new Rect(p.x2, p.y2, 30, 180), boardSize.y, Board.MinZones.y, Board.MaxZones.y);
			p.x2 += 30;


			Int2 cellSize = new Int2 (10,10); 
			Vector2 indent;
			Texture2D tex;
			Rect box;

			foreach (Int2 index in cellCount) {
				indent = new Vector2(p.x2+index.x*cellSize.x, p.y2+index.y*cellSize.y);

				if ((index.x + index.y)%2 == 0) {tex = tile.Even;}
				else {tex = tile.Odd;}

				box = new Rect (indent.x, indent.y, cellSize.x, cellSize.y);
				GUI.DrawTexture(box, tex);
			}
		}
	}
}

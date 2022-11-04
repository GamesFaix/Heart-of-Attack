using UnityEngine;

using HOA.Textures;

namespace HOA { 

	public static class TipInspector {
	
		public static ETip Inspected {get; private set;}

		public static void Inspect (ETip tip) {
			Inspected = tip;
			GUIGame.tip = true;
		}

		public static void Display (Panel p) {
			Color normalColor = p.s.normal.textColor;
			p.s.normal.textColor = Color.black;

			GUI.DrawTexture(p.FullBox, Backgrounds.Parchment, ScaleMode.StretchToFill);
		
			if (GUI.Button(p.LineBox, "Resume")) {
				GUIGame.tip = false;
				Reset();
			}
		
			if (Inspected != ETip.NONE) {
				Tip tip = Tip.FromETip(Inspected);
				p.NextLine();
				tip.Label(p.LinePanel);
				p.NextLine();
				tip.Content(new Panel(p.TallWideBox(16), p.LineH, p.s));
				p.NextLine();
				tip.SeeAlso(new Panel(p.TallWideBox(5), p.LineH, p.s));
			}
			p.s.normal.textColor = normalColor;
		}

		static void Reset () {
			Inspected = ETip.NONE;
		}

	}
}

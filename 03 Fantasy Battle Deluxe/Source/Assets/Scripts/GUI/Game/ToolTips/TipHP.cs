using UnityEngine;

namespace HOA {

	public static class TipHP {
		static float iconSize = 20;
		
		public static void Draw (Vector2 mousePos) {
			float x = mousePos.x;
			float y = mousePos.y;
			float w = 150;
			float h = 250;
			
			Rect box = new Rect(x,y,w,h);
			
			
			Panel p = new Panel(box, iconSize, GUIMaster.S);
			
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			GUI.Box(p.FullBox,"");
			
			Rect iconBox = p.Box(30);
			iconBox.height = 30;
			GUI.Box(iconBox, Icons.Stat(EStat.HP));
			p.NudgeX(); p.NudgeY();
			GUI.Box(p.Box(100), "Health", p.s);
			p.NudgeY();
			p.NextLine();
			
			int oldSize = p.s.fontSize;
			p.s.fontSize = 12;
			
			GUI.Label(p.TallBox(2.3f), "Health is noted as a fraction \n(current/maximum). \nUnless otherwise specified, \nActions can only change \na Unit's current Health. \nCurrent Health can never \nexceed maximum Health. \nWhen a Unit takes \nDamage, its current \nHealth is reduced by the \namount of Damage (unless \nit has Defense).\nIf a Unit's current Health \nis less than 1, it dies.", p.s);
			
			
			p.s.fontSize = oldSize;
			
		}	
		
	}
	
}

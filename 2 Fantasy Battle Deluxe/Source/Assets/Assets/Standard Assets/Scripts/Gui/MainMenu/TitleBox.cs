/* Title/logo area on top of menu screens. */

using UnityEngine;
using System.Collections;

namespace FBI.Gui.MainMenu {
	public static class TitleBox {
		static Rect mainTitleBox = Drawing.RectDouble(Screen.width*0.35, 20, Screen.width*0.3, MainMenuScale.bH*2);
		static Rect subTitleBox = Drawing.RectDouble(Screen.width*0.35, 65, Screen.width*0.3, MainMenuScale.bH*2);
		
		public static void Draw(){
			GUI.Box(mainTitleBox," Fantasy Battle:", Styles.heading[0]);
			GUI.Box(subTitleBox,"ISO", Styles.heading[1]);
		}

	}
}
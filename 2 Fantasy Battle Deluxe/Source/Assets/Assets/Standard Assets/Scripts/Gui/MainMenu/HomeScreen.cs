/* Opening screen.  (New Game/Exit...) */

using UnityEngine;
using System.Collections;

namespace FBI.Gui.MainMenu {
	public static class HomeScreen {
		public static void Draw(){
			Backgrounds.Draw();
			TitleBox.Draw();
			NewGameButton();	
			EmptyBoardButton();
		}
		
		static void NewGameButton(){
			//Rect buttonBox = Drawing.RectDouble(Screen.width*0.35, Screen.height/2, Screen.width*0.3, MainMenuScale.bH*2);
			
			Rect buttonBox = Drawing.RectDouble(Screen.width*0.35, Screen.height/2, 300, 50);
			if (GUI.Button(buttonBox, "New Game")) {/*Master.NewLobby()*/;}
		}
		
		static void EmptyBoardButton(){
			Rect buttonBox = Drawing.RectDouble(Screen.width*0.35, Screen.height/2 +50, 300, 50);
			if (GUI.Button(buttonBox, "Empty Board")) {/*Master.NewLobby()*/;}
		}
			
		
		
	}
}
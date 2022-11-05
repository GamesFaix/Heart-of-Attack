/* Changes state of GUI layout. (Home/Lobby/Game...) */

using UnityEngine;
using System.Collections;
using FBI.Gui;
using FBI.Gui.MainMenu;
//using FBI.Gui.Game;

namespace FBI.Gui {public enum GuiView {HOME, LOBBY, LOADING, GAME, OVER};}

public class GuiMaster : MonoBehaviour {

	public static GuiView guiView = GuiView.HOME;
	
	void Start(){
			Backgrounds.ToggleBG(GuiView.HOME);
			guiView = GuiView.HOME;
	}
	
	void OnGUI(){
		if (guiView == GuiView.HOME)   {HomeScreen.Draw();}
		if (guiView == GuiView.LOBBY)  {LobbyScreen.Draw();}
		//if (guiView == GuiView.LOADING){LoadingScreen.Draw();}
		//if (guiView == GuiView.GAME)   {Game.Draw();}
		//if (guiView == GuiView.OVER)   {Over.Draw();}
	}	
	
}

/* Loads and sets background image for Main Menu. */

using UnityEngine;
using System.Collections;

namespace FBI.Gui.MainMenu {
	public static class Backgrounds {
		static Texture[] backgrounds;
		static Texture2D currentMenuBG;
		static Texture2D currentBG;
		
		static Texture2D victoryBG;	
			
		static Backgrounds(){
			backgrounds = new Texture[5];
			
			backgrounds[0] = Resources.Load("Images/MenuBackgrounds/battle_apocalypse") as Texture2D;
			backgrounds[1] = Resources.Load("Images/MenuBackgrounds/battle_cats") as Texture2D;
			backgrounds[2] = Resources.Load("Images/MenuBackgrounds/battle_medieval") as Texture2D;
			backgrounds[3] = Resources.Load("Images/MenuBackgrounds/battle_robot") as Texture2D;
			backgrounds[4] = Resources.Load("Images/MenuBackgrounds/battle_steampunk") as Texture2D;	
			
			victoryBG = Resources.Load("Images/MenuBackgrounds/victory") as Texture2D;
			
			RandomizeMenuBG();
		}	
		
		public static Texture2D RandomizeMenuBG(){
			int randomIndex = Mathf.FloorToInt(UnityEngine.Random.value * backgrounds.Length);
			currentMenuBG = backgrounds[randomIndex] as Texture2D;
			return currentMenuBG;
		}
		
		public static void ToggleBG(GuiView selection){
			switch (selection){
				case GuiView.HOME: 
					currentBG = currentMenuBG; 
					break;
				case GuiView.OVER: 
					currentBG = victoryBG; 
					break;
				case GuiView.GAME: 
					currentBG = null; 
					break;
				default: 
					currentBG = null;
					break;
			}
		}
		
		public static void Draw(){DrawFullScreen(currentBG);}
		
		static void DrawFullScreen(Texture2D tex){
			Rect screenSize = Drawing.FullScreenRect();
			float screenRatio = Drawing.ScreenRatio();
			UnityEngine.GUI.DrawTexture(screenSize, tex, ScaleMode.StretchToFill, true, screenRatio);	
		}
	}
}
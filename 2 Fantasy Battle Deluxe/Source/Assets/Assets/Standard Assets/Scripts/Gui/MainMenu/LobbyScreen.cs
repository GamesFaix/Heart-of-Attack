/* Add players, set map parameters... */

using UnityEngine;
using System;

namespace FBI.Gui.MainMenu{
	public static class LobbyScreen{
			
		public static void Draw() {
			Backgrounds.Draw();
			TitleBox.Draw();
			
			//PlayerNameSelector.Draw();
			//FactionSelector.Draw(block);
			
			//MapSelector.Draw(block);
			//ObstacleDensity.Draw();
		
			//StartButtons.Draw();
		}
		
		static bool block = false;
		
		public static void Block() {block = true;}
		public static void Unblock() {block = false;}
		
	}
}
/* Custom styles for GUI Elements. */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FBI.Gui{
	public static class Styles{
		public static Font[] fonts = new Font[10];
		public static GUIStyle[] sButton = new GUIStyle[10];
		public static GUIStyle label = new GUIStyle();
		public static GUIStyle[] heading = new GUIStyle[2];
		public static GUIStyle[] clock = new GUIStyle[2];
		
		static Styles(){
			fonts[0]=Resources.Load("Fonts/Arial") as Font;
			fonts[1]=Resources.Load("Fonts/TeamFonts/GEARP") as Font;
			fonts[2]=Resources.Load("Fonts/TeamFonts/NewRepublic") as Font;
			fonts[3]=Resources.Load("Fonts/TeamFonts/Torridale") as Font;
			fonts[4]=Resources.Load("Fonts/TeamFonts/ForgottenGrove") as Font;
			fonts[5]=Resources.Load("Fonts/TeamFonts/Chrononista") as Font;
			fonts[6]=Resources.Load("Fonts/TeamFonts/PsychoTropic") as Font;
			fonts[7]=Resources.Load("Fonts/TeamFonts/PsilentAureator") as Font;
			fonts[8]=Resources.Load("Fonts/TeamFonts/Voidoid") as Font;
			fonts[9]=Resources.Load("Fonts/Arial") as Font;
			
			for (byte i=0; i<sButton.Length; i++){
				sButton[i] = new GUIStyle();
				sButton[i].font = fonts[i];
				sButton[i].normal.textColor=Color.white;
				sButton[i].hover.textColor=Color.white;
				sButton[i].active.textColor=Color.white;
				sButton[i].normal.background=Resources.Load("Images/GUI/Styles/button") as Texture2D;
				sButton[i].hover.background=Resources.Load("Images/GUI/Styles/button_hover") as Texture2D;
				sButton[i].active.background=Resources.Load("Images/GUI/Styles/button_active") as Texture2D;
				sButton[i].alignment=TextAnchor.MiddleCenter;
			}
		
			label.font=fonts[0];
			label.normal.textColor=Color.white;
			label.normal.background=Resources.Load("Images/GUI/Styles/box") as Texture2D;
			label.alignment=TextAnchor.MiddleCenter;
		
			heading[0] = new GUIStyle();
			heading[0].font=Resources.Load("Fonts/Heading1") as Font;
			heading[0].normal.textColor=Color.white;
			heading[0].normal.background=Resources.Load("Images/GUI/Styles/box") as Texture2D;
			heading[0].alignment=TextAnchor.MiddleCenter;
			
			heading[1] = new GUIStyle();
			heading[1].font=Resources.Load("Fonts/Heading2") as Font;
			heading[1].normal.textColor=Color.white;
			//heading[1].normal.background=Resources.Load("gui/style/box") as Texture2D;
			heading[1].alignment=TextAnchor.MiddleCenter;
			
			clock[0] = new GUIStyle();
			clock[0].font=Resources.Load("Fonts/RomanNum") as Font;
			clock[0].normal.textColor=Color.black;
			clock[0].normal.background=Resources.Load("Images/GUI/Styles/boxW") as Texture2D;
			clock[0].alignment=TextAnchor.MiddleCenter;
			
			clock[1] = new GUIStyle();
			clock[1].font=Resources.Load("Fonts/RomanNum") as Font;
			clock[1].normal.textColor=Color.yellow;
			clock[1].normal.background=Resources.Load("Images/GUI/Styles/boxB") as Texture2D;
			clock[1].alignment=TextAnchor.MiddleCenter;
		}
	}
}
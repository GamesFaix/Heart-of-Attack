/* Scales the Main Menu based on the screen size. */

using UnityEngine;
using System.Collections;
using FBI.Math;

namespace FBI.Gui.MainMenu{
	
	public static class MainMenuScale{
		public static short c1;
		public static short c2;
		public static short bW;
		public static short bH;
		public static short lH;
		public static short startY;
		public static short spacingY;
		
		public static void Adjust(){
			c1 = Rounding.ToShort(Screen.width*0.1);
			c2 = Rounding.ToShort(Screen.width*0.6);
			bW = Rounding.ToShort((Screen.width-(c1*2))/5);
			if (bW > 150){bW = 150;}
			if (bW < 75){bW = 75;}
			
			bH = 30;
			lH = 20;
			startY = Rounding.ToShort(Screen.height*0.2);
			spacingY = Rounding.ToShort((Screen.height-(bH*8)-(startY*2))/7);
			if (spacingY < 0){spacingY = 0;}
		}
	}
}
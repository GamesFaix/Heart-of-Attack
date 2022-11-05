namespace FBI.GameTime {
	public class GameClock{
	
		public static byte hour = 5;
		public static bool day = true;
		
		public bool Advance(){
			if (hour < 24){hour++;}
			else {hour = 1;}
			
			if (hour >= 6 && hour <= 17){day = true;}
			else {day = false;}
			
			return true;
		}
	}
}
using UnityEngine;

namespace HOA {


	public static class DiceCoin{
		
		static Source lastSource = new Source();
		static EDice lastDice;
		static int result;
		
		public static int Throw(Source s, EDice d){
			int max;
			switch (d){
				case EDice.COIN: max=2; break;
				case EDice.D4: max=4; break;
				case EDice.D6: max=6; break;
				case EDice.D8: max=8; break;
				case EDice.D10: max=10; break;
				case EDice.D12: max=12; break;
				case EDice.D20: max=20; break;
				default: 
					max = 1; 
					GameLog.Debug("DiceCoin: Illegal dice type.");
					return -1;
			}
			lastDice = d;
			lastSource = s;
			result = RandomSync.Range(1,max);
			GameLog.Out(PrintResult);
			return result;
		}
		
		public static string PrintResult {
			get {
				if (lastSource.Player != Roster.Neutral) {
					string printString = lastSource.ToString();
					
					if (lastDice == EDice.COIN) {
						printString += " flipped ";
						if (result == 1) {printString += "heads.";}
						else {printString += "tails.";}
					}
					else {
						printString += " rolled "+result+" on a "+lastDice.ToString();
					}	
					return printString;	
				}
				return "";
			}
		}
		
		public static int LastResult {get {return result;} }
		
	}
}
using UnityEngine;
using HOA.Players;

public enum DICE {COIN, D4, D6, D8, D10, D12, D20}

public static class DiceCoin{
	
	static Source lastSource = new Source();
	static DICE lastDice;
	static int result;
	
	public static int Throw(Source s, DICE d){
		int max;
		switch (d){
			case DICE.COIN: max=2; break;
			case DICE.D4: max=4; break;
			case DICE.D6: max=6; break;
			case DICE.D8: max=8; break;
			case DICE.D10: max=10; break;
			case DICE.D12: max=12; break;
			case DICE.D20: max=20; break;
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
				
				if (lastDice == DICE.COIN) {
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

using UnityEngine;
using System.Collections;

public enum DICE {COIN, D4, D6, D8, D10, D12, D20}

public static class DiceCoin{

	public static int Throw(DICE d){
		int max=1;
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
				GameLog.Add("DiceCoin: Illegal dice type.", LogIO.DEBUG);
				break;
		}
		return Mathf.RoundToInt(Random.Range(1,max));
	}
}

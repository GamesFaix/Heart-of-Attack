using UnityEngine;
using System.Collections.Generic;


public class Unit {
	public string code;
	public string name;
	public char instance;
	public string fullName;
	
	public int init;
	public bool skipped;
	
	public Unit(string newCode){
		code = newCode;
		
		UnitFactory.CodeNames.TryGetValue(code,out name);
		Debug.Log("Unit name: "+name);
		instance = NextAvailableInstance(this);
		fullName = name+" "+instance;
		InitializeStats(code);
		skipped = false;
		
	}
	
	static char NextAvailableInstance(Unit unit){
		List<Unit> likeUnits = new List<Unit>();
	
		foreach (Unit u in TurnQueue.units){
			if (u.name == unit.name){likeUnits.Add(u);}
		}		
		
		bool[] letterTaken = new bool[10] {
			false, false, false, false, false, 
			false, false, false, false, false};
	
		foreach (Unit u in likeUnits){
			if (u.instance == 'A'){letterTaken[0] = true;}
			if (u.instance == 'B'){letterTaken[1] = true;}	
			if (u.instance == 'C'){letterTaken[2] = true;}
			if (u.instance == 'D'){letterTaken[3] = true;}
			if (u.instance == 'E'){letterTaken[4] = true;}
			if (u.instance == 'F'){letterTaken[5] = true;}
			if (u.instance == 'G'){letterTaken[6] = true;}
			if (u.instance == 'H'){letterTaken[7] = true;}
			if (u.instance == 'I'){letterTaken[8] = true;}
			if (u.instance == 'J'){letterTaken[9] = true;}
		}
		if (letterTaken[0] == false){return 'A';}
		if (letterTaken[1] == false){return 'B';}
		if (letterTaken[2] == false){return 'C';}
		if (letterTaken[3] == false){return 'D';}
		if (letterTaken[4] == false){return 'E';}
		if (letterTaken[5] == false){return 'F';}
		if (letterTaken[6] == false){return 'G';}
		if (letterTaken[7] == false){return 'H';}
		if (letterTaken[8] == false){return 'I';}
		if (letterTaken[9] == false){return 'J';}
	
		return 'Z';
	}
	
	void InitializeStats(string code){
		switch (code){
			case "KATA":
				init = 5;		
				break;
			case "CARA":	
				init = 4;
				break;
			case "MAWT":
				init = 3;
				break;
			case "KABU":
				init = 4;
				break;
			
			case "DEMO":
				init = 3;
				break;
			case "MEIN":
				init = 4;
				break;
			case "PANO":
				init = 1;
				break;
			case "DECI":
				init = 2;
				break;
			
			case "ROOK":
				init = 3;
				break;
			case "SMAS":
				init = 3;
				break;
			case "CONF":
				init = 4;
				break;
			case "ASHE":
				init = 2;
				break;
			case "BATT":
				init = 1;
				break;
			case "GARG":
				init = 3;
				break;
			
			case "GRIZ":
				init = 3;
				break;
			case "TALO":
				init = 4;
				break;
			case "META":
				init = 1;
				break;
			case "ULTR":
				init = 2;
				break;
			
			case "REVO":
				init = 4;
				break;
			case "PIEC":
				init = 1;
				break;
			case "REPR":
				init = 2;
				break;
			case "OLDT":
				init = 3;
				break;
			
			case "LICH":
				init = 3;
				break;
			case "BEES":
				init = 5;
				break;
			case "MYCO":
				init = 2;
				break;
			case "MART":
				init = 4;
				break;
			case "BLAC":
				init = 3;
				break;
			
			case "PRIS":
				init = 3;
				break;
			case "AREN":
				init = 1;
				break;
			case "PRIE":
				init = 4;
				break;
			case "DREA":
				init = 3;
				break;
			
			case "RECY":
				init = 4;
				break;
			case "NECR":
				init = 3;
				break;
			case "MOUT":
				init = 4;
				break;
			case "MONO":
				init = 2;
				break;
			default:
				break;
		}
	}
	
	
}

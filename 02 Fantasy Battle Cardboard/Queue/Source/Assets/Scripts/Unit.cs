using UnityEngine;
using System.Collections.Generic;


public class Unit {
	public string code;
	public string name;
	public char instance;
	public string fullName;
	
	int init;
	int mhp;
	int hp;
	int def = 0;
	int cor = 0;
	int stun = 0;
	
	public bool skipped = false;
	
	public Unit(string newCode){
		code = newCode;
		
		UnitFactory.CodeNames.TryGetValue(code,out name);
		instance = NextAvailableInstance(this);
		fullName = name+" "+instance;
		InitializeStats(code);
		hp = mhp;
		
	}
	
	public int ModHP (string operation, int magnitude){
		if (operation == "="){
			hp = magnitude;
			if (hp>mhp){hp = mhp;}
			if (hp<1){
				QueueGUI.AddToLog(fullName+" has been killed!");
				CMD.New("kill "+fullName);
			}
			return hp;
		}
		if (operation == "+"){
			hp += magnitude;
			if (hp>mhp){hp = mhp;}
			return hp;
		}
		if (operation == "-"){
			hp -= magnitude;
			if (hp<1){
				QueueGUI.AddToLog(fullName+" has been killed!");
				CMD.New("kill "+fullName);
			}
			return hp;
		}
		return 9999;
	}
	public int HP(){return hp;}
	
	public int ModMHP (string operation, int magnitude){
		if (operation == "="){
			mhp = magnitude;
			return mhp;
		}
		if (operation == "+"){
			mhp += magnitude;
			return mhp;
		}
		if (operation == "-"){
			mhp -= magnitude;
			return mhp;
		}
		return 9999;
	}
	public int MHP(){return mhp;}
	
	public string HPFraction(){return hp+"/"+mhp;}
	
	public int ModIN (string operation, int magnitude){
		if (operation == "="){
			init = magnitude;
			return init;
		}
		if (operation == "+"){
			init += magnitude;
			return init;
		}
		if (operation == "-"){
			init -= magnitude;
			return init;
		}
		return 9999;
	}
	public int IN(){return init;}
	
	public int ModDEF (string operation, int magnitude){
		if (operation == "="){
			def = magnitude;
			if (def<0){def = 0;}
			return def;
		}
		if (operation == "+"){
			def += magnitude;
			return def;
		}
		if (operation == "-"){
			def -= magnitude;
			return def;
		}
		return 9999;
	}
	public int DEF(){return def;}
	
	public int ModCOR (string operation, int magnitude){
		if (operation == "="){
			cor = magnitude;
			return cor;
		}
		if (operation == "+"){
			cor += magnitude;
			return cor;
		}
		if (operation == "-"){
			cor -= magnitude;
			return cor;
		}
		return 9999;
	}
	public int COR(){return cor;}
	
	public int ModSTUN (string operation, int magnitude){
		if (operation == "="){
			stun = magnitude;
			return stun;
		}
		if (operation == "+"){
			stun += magnitude;
			return stun;
		}
		if (operation == "-"){
			stun -= magnitude;
			return stun;
		}
		return 9999;
	}
	public int STUN(){return stun;}
	
	
	
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
				mhp = 25;
				break;
			case "CARA":	
				init = 4;
				mhp = 35;
				def = 3;
				break;
			case "MAWT":
				init = 3;
				mhp = 55;
				break;
			case "KABU":
				init = 4;
				mhp = 75;
				break;
			
			case "DEMO":
				init = 3;
				mhp = 30;
				break;
			case "MEIN":
				init = 4;
				mhp = 40;
				break;
			case "PANO":
				init = 1;
				mhp = 65;
				break;
			case "DECI":
				init = 2;
				mhp = 85;
				def = 3;
				break;
			
			case "ROOK":
				init = 3;
				mhp = 30;
				def = 5;
				break;
			case "SMAS":
				init = 3;
				mhp = 30;
				break;
			case "CONF":
				init = 4;
				mhp = 40;
				break;
			case "ASHE":
				init = 2;
				mhp = 15;
				break;
			case "BATT":
				init = 1;
				mhp = 65;
				break;
			case "GARG":
				init = 3;
				mhp = 75;
				break;
			
			case "GRIZ":
				init = 3;
				mhp = 25;
				break;
			case "TALO":
				init = 4;
				mhp = 45;
				break;
			case "META":
				init = 1;
				mhp = 50;
				break;
			case "ULTR":
				init = 2;
				mhp = 80;
				break;
			
			case "REVO":
				init = 4;
				mhp = 30;
				break;
			case "PIEC":
				init = 1;
				mhp = 35;
				def = 3;
				break;
			case "REPR":
				init = 2;
				mhp = 55;
				break;
			case "OLDT":
				init = 3;
				mhp = 85;
				def = 2;
				break;
			
			case "LICH":
				init = 3;
				mhp = 15;
				break;
			case "BEES":
				init = 5;
				mhp = 25;
				break;
			case "MYCO":
				init = 2;
				mhp = 40;
				break;
			case "MART":
				init = 4;
				mhp = 70;
				break;
			case "BLAC":
				init = 3;
				mhp = 75;
				break;
			
			case "PRIS":
				init = 3;
				mhp = 15;
				break;
			case "AREN":
				init = 1;
				mhp = 55;
				def = 3;
				break;
			case "PRIE":
				init = 4;
				mhp = 50;
				def = 2;
				break;
			case "DREA":
				init = 3;
				mhp = 75;
				def = 2;
			break;
			
			case "RECY":
				init = 4;
				mhp = 15;
				break;
			case "NECR":
				init = 3;
				mhp = 30;
				def = 5;
				break;
			case "MOUT":
				init = 4;
				mhp = 30;
				break;
			case "MONO":
				init = 2;
				mhp = 100;
				break;
			default:
				break;
		}
	}
	
	
}

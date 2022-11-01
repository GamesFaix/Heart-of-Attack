using UnityEngine;
using System;
using System.Collections.Generic;

public enum PLANE {SUNK, GND, AIR, ETH}
public enum TCLASS {NONE, KING, TRAM, DEST, REM}

public class Unit {
	public string code;
	public string name;
	public char instance;
	public string fullName;
	public string deathCode = "CORP";
	
	int mhp;
	int hp;
	int def = 0;
	int cor = 0;

	int turnAP = 2;
	int ap = 0;
	int fp = 0;

	int init;
	int stun = 0;
	public bool skipped = false;

	static int planes = Enum.GetNames(typeof(PLANE)).Length;
	bool[] plane = new bool[planes];
	static int tClasses = Enum.GetNames(typeof(TCLASS)).Length;
	bool[] tClass = new bool[tClasses];

	public Unit(string newCode){
		code = newCode;
		
		UnitFactory.CodeNames.TryGetValue(code,out name);
		instance = NextAvailableInstance(this);
		fullName = name+" "+instance;
		UnitConstructor.Make(this,code);
		hp = mhp;
		
	}

	public void SetPlane (PLANE p){
		for (int i=0; i<planes; i++) {plane[i] = false;}
		switch (p){
			case PLANE.SUNK: plane[0]=true; break;
			case PLANE.GND:  plane[1]=true; break;
			case PLANE.AIR:  plane[2]=true; break;
			case PLANE.ETH:  plane[3]=true; break;
			default:
				GameLog.Add("Unit: Attempt to assign invalid plane.",LogIO.DEBUG);
				break;
		}
	}
	public void SetPlane (PLANE[] ps){
		for (int i=0; i<planes; i++) {plane[i] = false;}
		foreach (PLANE p in ps){
			switch (p){
				case PLANE.SUNK: plane[0]=true; break;
				case PLANE.GND:  plane[1]=true; break;
				case PLANE.AIR:  plane[2]=true; break;
				case PLANE.ETH:  plane[3]=true; break;
				default:
				GameLog.Add("Unit: Attempt to assign invalid plane.",LogIO.DEBUG);
				break;
			}
		}
	}
	public List<PLANE> Plane(){
		List<PLANE> ps = new List<PLANE>();
		if (plane[0]){ps.Add(PLANE.SUNK);}
		if (plane[1]){ps.Add(PLANE.GND);}
		if (plane[2]){ps.Add(PLANE.AIR);}
		if (plane[3]){ps.Add(PLANE.ETH);}
		return ps;
	}
	public void SetTClass (TCLASS tc){
		for (int i=0; i<tClasses; i++) {tClass[i] = false;}
		switch (tc){
		case TCLASS.KING: tClass[1]=true; break;
		case TCLASS.TRAM: tClass[2]=true; break;
		case TCLASS.DEST: tClass[3]=true; break;
		case TCLASS.REM:  tClass[4]=true; break;
		default:
			break;
		}
	}
	public void SetTClass (TCLASS[] tcs){
		for (int i=0; i<tClasses; i++) {tClass[i] = false;}
		foreach (TCLASS tc in tcs){
			switch (tc){
			case TCLASS.KING: tClass[1]=true; break;
			case TCLASS.TRAM: tClass[2]=true; break;
			case TCLASS.DEST: tClass[3]=true; break;
			case TCLASS.REM:  tClass[4]=true; break;
			default:
				break;
			}
		}
	}
	public List<TCLASS> TClass(){
		List<TCLASS> tcs = new List<TCLASS>();
		if (tClass[1]){tcs.Add(TCLASS.KING);}
		if (tClass[2]){tcs.Add(TCLASS.TRAM);}
		if (tClass[3]){tcs.Add(TCLASS.DEST);}
		if (tClass[4]){tcs.Add(TCLASS.REM);}
		if (!tClass[1] && !tClass[2] && !tClass[3] && !tClass[4]){tcs.Add(TCLASS.NONE);}
		return tcs;
	}
	public bool IsTClass(TCLASS tc){
		if (tc==TCLASS.KING && tClass[1]){return true;}
		if (tc==TCLASS.TRAM && tClass[2]){return true;}
		if (tc==TCLASS.DEST && tClass[3]){return true;}
		if (tc==TCLASS.REM && tClass[4]) {return true;}
		return false;
	}

	public int SetHP (int n, bool log=true){
		hp = n;
		if (hp>mhp){hp=mhp; 
			if(log){GameLog.Add(fullName+" HP set to max. "+HPFraction(), LogIO.OUT);}}
		else {if(log){GameLog.Add(fullName+" HP set to "+HPFraction()+".", LogIO.OUT);}}
		if (hp<1){CMD.New("kill "+fullName);}
		return hp;
	}
	public int ModHP (int n, bool log=true){
		hp += n;
		string change = "+"+n;
		if (n<0) {change = ""+n;}
		int diff = hp-mhp;
		if (hp>mhp){hp = mhp;
			if(log){GameLog.Add(fullName+" "+change+"HP. HP above max. "+HPFraction(),LogIO.OUT);}}
		else{if(log){GameLog.Add(fullName+" "+change+"HP. "+HPFraction(),LogIO.OUT);}}
		if (hp<1){CMD.New("kill "+fullName);}
		return hp;
	}
	public int HP(){return hp;}

	public int SetMHP (int n, bool log=true){
		mhp = n;
		int diff = hp-mhp;
		if (hp>mhp){hp=mhp; 
			if(log){GameLog.Add(fullName+" MHP set below HP. -"+diff+"HP. "+HPFraction(), LogIO.OUT);}}
		else {if(log){GameLog.Add(fullName+" MHP set to "+HPFraction()+".", LogIO.OUT);}}
		if (mhp<1){CMD.New("kill "+fullName);}
		return mhp;
	}
	public int ModMHP (int n, bool log=true){
		mhp += n;
		string change = "+"+n;
		if (n<0) {change = ""+n;}
		int diff = hp-mhp;
		if (hp>mhp){hp=mhp;
			if(log){GameLog.Add(fullName+" "+change+"MHP. HP above max. "+HPFraction(),LogIO.OUT);}}
		else {if(log){GameLog.Add(fullName+" "+change+"MHP. "+HPFraction(),LogIO.OUT);}}
		if (mhp<1){CMD.New("kill "+fullName);}
		return mhp;	
	}
	public int MHP(){return mhp;}
	
	public string HPFraction(){return "("+hp+"/"+mhp+")";}

	public int Damage(int n, bool log=true){
		if (n >= 0){
			if (n <= def) {
				if(log) {GameLog.Add(fullName+" defended against all damage. "+HPFraction(), LogIO.OUT);}
			}
			if (n > def){
				int dmg = n-def;
				hp -= dmg;
				if(log) {
					if(def==0){GameLog.Add(fullName+" took "+dmg+" damage. "+HPFraction(), LogIO.OUT);}
					if(def>0){GameLog.Add(fullName+" defended against "+def+", took "+dmg+" damage. "+HPFraction(), LogIO.OUT);}
				}
			}
			else {GameLog.Add("Units cannot take negative damage.", LogIO.DEBUG);}
		}			
		if (hp<1){CMD.New("kill "+fullName);}
		return hp;
	}

	public int SetIN (int n, bool log=true){
		init = n;
		if(log){GameLog.Add(fullName+"'s IN set to "+init+".",LogIO.OUT);}
		return init;
	}
	public int ModIN (int n, bool log=true){
		init += n;
		string change = "+"+n;
		if (n<0) {change = ""+n;}
		if(log){GameLog.Add(fullName+" "+change+"IN. IN="+init, LogIO.OUT);}
		return init;
	}
	public int IN(){return init;}

	public int SetDEF (int n, bool log=true){
		def = n;
		if (def<0){def = 0;}
		if(log){GameLog.Add(fullName+"'s DEF set to "+def+".", LogIO.OUT);}
		return def;
	}
	public int ModDEF (int n, bool log=true){
		def = n;
		if (def<0){def = 0;}
		string change = "+"+n;
		if (n<0) {change = ""+n;}
		if(log){GameLog.Add(fullName+" "+change+"DEF. DEF="+def, LogIO.OUT);}
		return def;
	}
	public int DEF(){return def;}




	public int SetCOR (int n, bool log=true){
		if (n<0){n=0;}
		cor = n;
		if(log){GameLog.Add(fullName+"'s corrosion counters set to "+cor+".", LogIO.OUT);}
		return cor;
	}
	public int ModCOR (int n, bool log=true){
		cor += n;
		string change = "+"+n;
		if (n<0) {change = ""+n;}
		if (cor<0){cor=0;}
		if(log){GameLog.Add(fullName+" "+change+"COR. COR="+cor, LogIO.OUT);}
		return cor;
	}
	public int COR(){return cor;}

	public int SetSTUN (int n, bool log=true){
		if (n<0){n=0;}
		stun = n;
		if(log){GameLog.Add(fullName+"'s STUN set to "+stun+".", LogIO.OUT);}
		return stun;
	}
	public int ModSTUN (int n, bool log=true){
		stun += n;
		string change = "+"+n;
		if (n<0) {change = ""+n;}
		if (stun<0){stun=0;}
		if(log){GameLog.Add(fullName+" "+change+"STUN. STUN="+stun, LogIO.OUT);}
		return stun;
	}
	public int STUN(){return stun;}

	public int SetAP (int n, bool log=true){
		if (n<0){n=0;} 
		ap = n;
		if(log){GameLog.Add(fullName+"'s AP set to "+ap+".", LogIO.OUT);}
		return ap;
	}
	public int ModAP (int n, bool log=true){
		ap += n;
		string change ="+"+n;
		if (n<0) {change = ""+n;}
		if (ap<0) {ap=0;}
		if(log){GameLog.Add(fullName+" "+change+"AP. AP="+ap, LogIO.OUT);}
		return ap;
	}
	public int AP(){return ap;}
	public void SetTurnAP(int n){turnAP=n;}
	public int TurnAP(){return turnAP;}

	public int SetFP (int n, bool log=true){
		if (n<0){n=0;} 
		fp = n;
		if(log){GameLog.Add(fullName+"'s FP set to "+fp+".", LogIO.OUT);}
		return fp;
	}
	public int ModFP (int n, bool log=true){
		fp += n;
		string change ="+"+n;
		if (n<0) {change = ""+n;}
		if (fp<0) {fp=0;}
		if(log){GameLog.Add(fullName+" "+change+"FP. FP="+fp, LogIO.OUT);}
		return fp;
	}
	public int FP(){return fp;}
	
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
	

	
	
}

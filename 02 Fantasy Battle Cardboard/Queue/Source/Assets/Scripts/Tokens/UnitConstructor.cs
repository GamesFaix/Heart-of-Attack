using UnityEngine;
using System.Collections;

public static class UnitConstructor {

	public static void Make(Unit u, string code){
		u.SetTClass(TCLASS.NONE);

		switch (code){
		case "KATA":
			u.SetIN(5,false);	
			u.SetMHP(25,false);
			u.SetPlane(PLANE.GND);
			break;
		case "CARA":	
			u.SetIN(4,false);
			u.SetMHP(35,false);
			u.SetDEF(3,false);
			u.SetPlane(PLANE.GND);
			break;
		case "MAWT":
			u.SetIN(3,false);
			u.SetMHP(55,false);
			u.SetPlane(PLANE.AIR);
			break;
		case "KABU":
			u.SetIN(4,false);
			u.SetMHP(75,false);
			u.SetPlane(PLANE.AIR);
			u.SetTClass(TCLASS.KING);
			break;
			
		case "DEMO":
			u.SetIN(3,false);
			u.SetMHP(30,false);
			u.SetPlane(PLANE.GND);
			break;
		case "MEIN":
			u.SetIN(4,false);
			u.SetMHP(40,false);
			u.SetPlane(PLANE.GND);
			break;
		case "PANO":
			u.SetIN(1,false);
			u.SetMHP(65,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.TRAM);
			break;
		case "DECI":
			u.SetIN(2,false);
			u.SetMHP(85,false);
			u.SetDEF(3,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(new TCLASS[]{TCLASS.KING, TCLASS.TRAM});
			break;
			
		case "ROOK":
			u.SetIN(3,false);
			u.SetMHP(30,false);
			u.SetDEF(5,false);
			u.SetPlane(PLANE.GND);
			break;
		case "SMAS":
			u.SetIN(3,false);
			u.SetMHP(30,false);
			u.SetPlane(PLANE.GND);
			break;
		case "CONF":
			u.SetIN(4,false);
			u.SetMHP(40,false);
			u.SetPlane(PLANE.AIR);
			break;
		case "ASHE":
			u.SetIN(2,false);
			u.SetMHP(15,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(new TCLASS[]{TCLASS.DEST, TCLASS.REM});
			break;
		case "BATT":
			u.SetIN(1,false);
			u.SetMHP(65,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.TRAM);
			break;
		case "GARG":
			u.SetIN(3,false);
			u.SetMHP(75,false);
			u.SetPlane(PLANE.AIR);
			u.SetTClass(TCLASS.KING);
			break;
			
		case "GRIZ":
			u.SetIN(3,false);
			u.SetMHP(25,false);
			u.SetPlane(PLANE.GND);
			break;
		case "TALO":
			u.SetIN(4,false);
			u.SetMHP(45,false);
			u.SetPlane(PLANE.AIR);
			break;
		case "META":
			u.SetIN(1,false);
			u.SetMHP(50,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.TRAM);
			break;
		case "ULTR":
			u.SetIN(2,false);
			u.SetMHP(80,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(new TCLASS[]{TCLASS.KING, TCLASS.TRAM});
			break;
			
		case "REVO":
			u.SetIN(4,false);
			u.SetMHP(30,false);
			u.SetPlane(PLANE.GND);
			break;
		case "PIEC":
			u.SetIN(1,false);
			u.SetMHP(35,false);
			u.SetDEF(3,false);
			u.SetPlane(PLANE.GND);
			break;
		case "REPR":
			u.SetIN(2,false);
			u.SetMHP(55,false);
			u.SetPlane(PLANE.GND);
			break;
		case "OLDT":
			u.SetIN(3,false);
			u.SetMHP(85,false);
			u.SetDEF(2,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.KING);
			break;
			
		case "LICH":
			u.SetIN(3,false);
			u.SetMHP(15,false);
			u.SetPlane(PLANE.GND);
			break;
		case "BEES":
			u.SetIN(5,false);
			u.SetMHP(25,false);
			u.SetPlane(PLANE.AIR);
			break;
		case "MYCO":
			u.SetIN(2,false);
			u.SetMHP(40,false);
			u.SetPlane(PLANE.GND);
			break;
		case "MART":
			u.SetIN(4,false);
			u.SetMHP(70,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.TRAM);
			break;
		case "BLAC":
			u.SetIN(3,false);
			u.SetMHP(75,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.KING);
			break;
			
		case "PRIS":
			u.SetIN(3,false);
			u.SetMHP(15,false);
			u.SetPlane(PLANE.GND);
			break;
		case "AREN":
			u.SetIN(1,false);
			u.SetMHP(55,false);
			u.SetDEF(3,false);
			u.SetPlane(PLANE.ETH);
			break;
		case "PRIE":
			u.SetIN(4,false);
			u.SetMHP(50,false);
			u.SetDEF(2,false);
			u.SetPlane(PLANE.GND);
			break;
		case "DREA":
			u.SetIN(3,false);
			u.SetMHP(75,false);
			u.SetDEF(2,false);
			u.SetPlane(PLANE.AIR);
			u.SetTClass(TCLASS.KING);
			break;
			
		case "RECY":
			u.SetIN(4,false);
			u.SetMHP(15,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(new TCLASS[]{TCLASS.DEST, TCLASS.REM});
			break;
		case "NECR":
			u.SetIN(3,false);
			u.SetMHP(30,false);
			u.SetDEF(5,false);
			u.SetPlane(PLANE.ETH);
			break;
		case "MOUT":
			u.SetIN(4,false);
			u.SetMHP(30,false);
			u.SetPlane(PLANE.GND);
			break;
		case "MONO":
			u.SetIN(2,false);
			u.SetMHP(100,false);
			u.SetPlane(PLANE.AIR);
			u.SetTClass(TCLASS.KING);
			break;
		default:
			break;
		}

		if (u.IsTClass(TCLASS.KING)) {u.SetTurnAP(3);}
	}





}

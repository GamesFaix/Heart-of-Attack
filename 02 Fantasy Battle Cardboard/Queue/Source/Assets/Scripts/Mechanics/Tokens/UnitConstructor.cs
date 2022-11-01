using UnityEngine;
using System.Collections;

public static class UnitConstructor {

	public static void Make(Unit u, string code){
		u.SetTClass(TCLASS.NONE);

		switch (code){
		case "KATA":
			u.SetOwner(1,false);
			u.SetIN(5,false);	
			u.SetMHP(25,false);
			u.SetPlane(PLANE.GND);
			break;
		case "CARA":	
			u.SetOwner(1,false);
			u.SetIN(4,false);
			u.SetMHP(35,false);
			u.SetDEF(3,false);
			u.SetPlane(PLANE.GND);
			break;
		case "MAWT":
			u.SetOwner(1,false);
			u.SetIN(3,false);
			u.SetMHP(55,false);
			u.SetPlane(PLANE.AIR);
			break;
		case "KABU":
			u.SetOwner(1,false);
			u.SetIN(4,false);
			u.SetMHP(75,false);
			u.SetPlane(PLANE.AIR);
			u.SetTClass(TCLASS.KING);
			u.deathCode = "HSIL";
			Roster.Activate(1);
			break;
			
		case "DEMO":
			u.SetOwner(2,false);
			u.SetIN(3,false);
			u.SetMHP(30,false);
			u.SetPlane(PLANE.GND);
			break;
		case "MEIN":
			u.SetOwner(2,false);
			u.SetIN(4,false);
			u.SetMHP(40,false);
			u.SetPlane(PLANE.GND);
			break;
		case "PANO":
			u.SetOwner(2,false);
			u.SetIN(1,false);
			u.SetMHP(65,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.TRAM);
			break;
		case "DECI":
			u.SetOwner(2,false);
			u.SetIN(2,false);
			u.SetMHP(85,false);
			u.SetDEF(3,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(new TCLASS[]{TCLASS.KING, TCLASS.TRAM});
			u.deathCode = "HSTE";
			Roster.Activate(2);
			break;
			
		case "ROOK":
			u.SetOwner(3,false);
			u.SetIN(3,false);
			u.SetMHP(30,false);
			u.SetDEF(5,false);
			u.SetPlane(PLANE.GND);
			break;
		case "SMAS":
			u.SetOwner(3,false);
			u.SetIN(3,false);
			u.SetMHP(30,false);
			u.SetPlane(PLANE.GND);
			break;
		case "CONF":
			u.SetOwner(3,false);
			u.SetIN(4,false);
			u.SetMHP(40,false);
			u.SetPlane(PLANE.AIR);
			u.deathCode = "ASHE";
			break;
		case "ASHE":
			u.SetOwner(3,false);
			u.SetIN(2,false);
			u.SetMHP(15,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(new TCLASS[]{TCLASS.DEST, TCLASS.REM});
			u.deathCode = "";
			break;
		case "BATT":
			u.SetOwner(3,false);
			u.SetIN(1,false);
			u.SetMHP(65,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.TRAM);
			break;
		case "GARG":
			u.SetOwner(3,false);
			u.SetIN(3,false);
			u.SetMHP(75,false);
			u.SetPlane(PLANE.AIR);
			u.SetTClass(TCLASS.KING);
			u.deathCode = "HSTO";
			Roster.Activate(3);
			break;
			
		case "GRIZ":
			u.SetOwner(4,false);
			u.SetIN(3,false);
			u.SetMHP(25,false);
			u.SetPlane(PLANE.GND);
			break;
		case "TALO":
			u.SetOwner(4,false);
			u.SetIN(4,false);
			u.SetMHP(45,false);
			u.SetPlane(PLANE.AIR);
			break;
		case "META":
			u.SetOwner(4,false);
			u.SetIN(1,false);
			u.SetMHP(50,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.TRAM);
			break;
		case "ULTR":
			u.SetOwner(4,false);
			u.SetIN(2,false);
			u.SetMHP(80,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(new TCLASS[]{TCLASS.KING, TCLASS.TRAM});
			Roster.Activate(4);
			break;
			
		case "REVO":
			u.SetOwner(5,false);
			u.SetIN(4,false);
			u.SetMHP(30,false);
			u.SetPlane(PLANE.GND);
			break;
		case "PIEC":
			u.SetOwner(5,false);
			u.SetIN(1,false);
			u.SetMHP(35,false);
			u.SetDEF(3,false);
			u.SetPlane(PLANE.GND);
			break;
		case "REPR":
			u.SetOwner(5,false);
			u.SetIN(2,false);
			u.SetMHP(55,false);
			u.SetPlane(PLANE.GND);
			break;
		case "OLDT":
			u.SetOwner(5,false);
			u.SetIN(3,false);
			u.SetMHP(85,false);
			u.SetDEF(2,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.KING);
			Roster.Activate(5);
			break;
			
		case "LICH":
			u.SetOwner(6,false);
			u.SetIN(3,false);
			u.SetMHP(15,false);
			u.SetPlane(PLANE.GND);
			break;
		case "BEES":
			u.SetOwner(6,false);
			u.SetIN(5,false);
			u.SetMHP(25,false);
			u.SetPlane(PLANE.AIR);
			break;
		case "MYCO":
			u.SetOwner(6,false);
			u.SetIN(2,false);
			u.SetMHP(40,false);
			u.SetPlane(PLANE.GND);
			break;
		case "MART":
			u.SetOwner(6,false);
			u.SetIN(4,false);
			u.SetMHP(70,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.TRAM);
			break;
		case "BLAC":
			u.SetOwner(6,false);
			u.SetIN(3,false);
			u.SetMHP(75,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(TCLASS.KING);
			Roster.Activate(6);
			break;
			
		case "PRIS":
			u.SetOwner(7,false);
			u.SetIN(3,false);
			u.SetMHP(15,false);
			u.SetPlane(PLANE.GND);
			break;
		case "AREN":
			u.SetOwner(7,false);
			u.SetIN(1,false);
			u.SetMHP(55,false);
			u.SetDEF(3,false);
			u.SetPlane(PLANE.ETH);
			break;
		case "PRIE":
			u.SetOwner(7,false);
			u.SetIN(4,false);
			u.SetMHP(50,false);
			u.SetDEF(2,false);
			u.SetPlane(PLANE.GND);
			break;
		case "DREA":
			u.SetOwner(7,false);
			u.SetIN(3,false);
			u.SetMHP(75,false);
			u.SetDEF(2,false);
			u.SetPlane(PLANE.AIR);
			u.SetTClass(TCLASS.KING);
			Roster.Activate(7);
			break;
			
		case "RECY":
			u.SetOwner(8,false);
			u.SetIN(4,false);
			u.SetMHP(15,false);
			u.SetPlane(PLANE.GND);
			u.SetTClass(new TCLASS[]{TCLASS.DEST, TCLASS.REM});
			break;
		case "NECR":
			u.SetOwner(8,false);
			u.SetIN(3,false);
			u.SetMHP(30,false);
			u.SetDEF(5,false);
			u.SetPlane(PLANE.ETH);
			break;
		case "MOUT":
			u.SetOwner(8,false);
			u.SetIN(4,false);
			u.SetMHP(30,false);
			u.SetPlane(PLANE.GND);
			break;
		case "MONO":
			u.SetOwner(8,false);
			u.SetIN(2,false);
			u.SetMHP(100,false);
			u.SetPlane(PLANE.AIR);
			u.SetTClass(TCLASS.KING);
			Roster.Activate(8);
			break;
		default:
			break;
		}

		if (u.IsTClass(TCLASS.KING)) {u.SetTurnAP(3);}
	}





}

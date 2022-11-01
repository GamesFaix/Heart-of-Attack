using UnityEngine;
using System.Collections;
using Tokens;

public static class UnitConstructor {

	public static void Make (Unit u, string code){

		switch (code){
		case "KATA":
			u.NewBody(PLANE.GND);
			u.NewHealth(25);
			u.SetOwner(1,false);
			u.NewClock(5);	
			break;
		case "CARA":	
			u.NewBody(PLANE.GND);
			u.NewHealth(35,3);
			u.SetOwner(1,false);
			u.NewClock(4);
			break;
		case "MAWT":
			u.NewBody(PLANE.AIR);
			u.NewHealth(55);
			u.SetOwner(1,false);
			u.NewClock(3);
			break;
		case "KABU":
			u.NewBody(PLANE.GND, SPECIAL.KING);
			u.NewHealth(75);
			u.SetOwner(1,false);
			u.NewClock(4);
			u.SetOnDeath("HSIL", false);
			Roster.Activate(1);
			break;
			
		case "DEMO":
			u.NewBody(PLANE.GND);
			u.NewHealth(30);
			u.SetOwner(2,false);
			u.NewClock(3);
			break;
		case "MEIN":
			u.NewBody(PLANE.GND);
			u.NewHealth(40);
			u.SetOwner(2,false);
			u.NewClock(4);
			break;
		case "PANO":
			u.NewBody(PLANE.GND, SPECIAL.TRAM);
			u.NewHealth(65);
			u.SetOwner(2,false);
			u.NewClock(1);
			break;
		case "DECI":
			u.NewBody(PLANE.GND,new SPECIAL[]{SPECIAL.KING, SPECIAL.TRAM});
			u.NewHealth(85,3);
			u.SetOwner(2,false);
			u.NewClock(2);
			u.SetOnDeath("HSTE", false);
			Roster.Activate(2);
			break;
			
		case "ROOK":
			u.NewBody(PLANE.GND);
			u.NewHealth(30,5);
			u.SetOwner(3,false);
			u.NewClock(3);
			u.SetOnDeath("",false);
			break;
		case "SMAS":
			u.NewBody(PLANE.GND);
			u.NewHealth(30);
			u.SetOwner(3,false);
			u.NewClock(3);
			break;
		case "CONF":
			u.NewBody(PLANE.AIR);
			u.NewHealth(40);
			u.SetOwner(3,false);
			u.NewClock(4);
			u.SetOnDeath("ASHE", false);
			break;
		case "ASHE":
			u.NewBody(PLANE.GND, new SPECIAL[]{SPECIAL.DEST, SPECIAL.REM});
			u.NewHealth(15);
			u.SetOwner(3,false);
			u.NewClock(2);
			u.SetOnDeath("", false);
			break;
		case "BATT":
			u.NewBody(PLANE.GND, SPECIAL.TRAM);
			u.NewHealth(65);
			u.SetOwner(3,false);
			u.NewClock(1);
			break;
		case "GARG":
			u.NewBody(PLANE.AIR, SPECIAL.KING);
			u.NewHealth(75);
			u.SetOwner(3,false);
			u.NewClock(3);
			u.SetOnDeath("HSTO", false);
			Roster.Activate(3);
			break;
			
		case "GRIZ":
			u.NewBody(PLANE.GND);
			u.NewHealth(25);
			u.SetOwner(4,false);
			u.NewClock(3);
			break;
		case "TALO":
			u.NewBody(PLANE.AIR);
			u.NewHealth(45);
			u.SetOwner(4,false);
			u.NewClock(4);
			break;
		case "META":
			u.NewBody(PLANE.GND, SPECIAL.TRAM);
			u.NewHealth(50);
			u.SetOwner(4,false);
			u.NewClock(1);
			u.SetOnDeath("TREE", false);
			break;
		case "ULTR":
			u.NewBody(PLANE.GND,new SPECIAL[]{SPECIAL.KING, SPECIAL.TRAM});
			u.NewHealth(80);
			u.SetOwner(4,false);
			u.NewClock(2);
			u.SetOnDeath("HFIR", false);
			Roster.Activate(4);
			break;
			
		case "REVO":
			u.NewBody(PLANE.GND);
			u.NewHealth(30);
			u.SetOwner(5,false);
			u.NewClock(4);
			break;
		case "PIEC":
			u.NewBody(PLANE.GND);
			u.NewHealth(35,3);
			u.SetOwner(5,false);
			u.NewClock(1);
			break;
		case "REPR":
			u.NewBody(PLANE.GND);
			u.NewHealth(55);
			u.SetOwner(5,false);
			u.NewClock(2);
			break;
		case "OLDT":
			u.NewBody(PLANE.GND, SPECIAL.KING);
			u.NewHealth(85,2);
			u.SetOwner(5,false);
			u.NewClock(3);
			u.SetOnDeath("HBRA",false);
			Roster.Activate(5);
			break;
			
		case "LICH":
			u.NewBody(PLANE.GND);
			u.NewHealth(15);
			u.SetOwner(6,false);
			u.NewClock(3);
			u.SetOnDeath("", false);
			break;
		case "BEES":
			u.NewBody(PLANE.AIR);
			u.NewHealth(25);
			u.SetOwner(6,false);
			u.NewClock(5);
			break;
		case "MYCO":
			u.NewBody(PLANE.GND);
			u.NewHealth(40);
			u.SetOwner(6,false);
			u.NewClock(2);
			break;
		case "MART":
			u.NewBody(PLANE.GND, SPECIAL.TRAM);
			u.NewHealth(70);
			u.SetOwner(6,false);
			u.NewClock(4);
			break;
		case "BLAC":
			u.NewBody(PLANE.GND, SPECIAL.KING);
			u.NewHealth(75);
			u.SetOwner(6,false);
			u.NewClock(3);
			u.SetOnDeath("HSIL",false);
			Roster.Activate(6);
			break;
			
		case "PRIS":
			u.NewBody(PLANE.GND);
			u.NewHealth(15);
			u.SetOwner(7,false);
			u.NewClock(3);
			break;
		case "AREN":
			u.NewBody(PLANE.ETH);
			u.NewHealth(55,3);
			u.SetOwner(7,false);
			u.NewClock(1);
			u.SetOnDeath("",false);
			break;
		case "PRIE":
			u.NewBody(PLANE.GND);
			u.NewHealth(50,2);
			u.SetOwner(7,false);
			u.NewClock(4);
			break;
		case "DREA":
			u.NewBody(PLANE.AIR, SPECIAL.KING);
			u.NewHealth(75,2);
			u.SetOwner(7,false);
			u.NewClock(3);
			u.SetOnDeath("HGLA",false);
			Roster.Activate(7);
			break;
			
		case "RECY":
			u.NewBody(PLANE.GND, new SPECIAL[]{SPECIAL.DEST, SPECIAL.REM});
			u.NewHealth(15);
			u.SetOwner(8,false);
			u.NewClock(4);
			break;
		case "NECR":
			u.NewBody(PLANE.ETH);
			u.NewHealth(30,5);
			u.SetOwner(8,false);
			u.NewClock(3);
			u.SetOnDeath("",false);
			break;
		case "MOUT":
			u.NewBody(PLANE.GND);
			u.NewHealth(30);
			u.SetOwner(8,false);
			u.NewClock(4);
			break;
		case "MONO":
			u.NewBody(new PLANE[] {PLANE.GND, PLANE.AIR}, SPECIAL.KING);
			u.NewHealth(100);
			u.SetOwner(8,false);
			u.NewClock(2);
			u.SetOnDeath("HBLO",false);
			Roster.Activate(8);
			break;
		default:
			break;
		}

		if (u.IsSpecial(SPECIAL.KING)) {u.SetMaxAP(3, false);}
	}





}

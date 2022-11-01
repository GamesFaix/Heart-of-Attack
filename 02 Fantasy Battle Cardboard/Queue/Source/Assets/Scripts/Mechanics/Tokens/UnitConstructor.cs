using UnityEngine;
using System.Collections;
using Tokens;

public static class UnitConstructor {

	public static void Make (Unit u, TTYPE code){

		switch (code){
		case TTYPE.KATA:
			u.NewBody(PLANE.GND);
			u.NewHealth(25);
			u.SetOwner(1,false);
			u.NewClock(5);	
			break;
		case TTYPE.CARA:	
			u.NewBody(PLANE.GND);
			u.NewHealth(35,3);
			u.SetOwner(1,false);
			u.NewClock(4);
			break;
		case TTYPE.MAWT:
			u.NewBody(PLANE.AIR);
			u.NewHealth(55);
			u.SetOwner(1,false);
			u.NewClock(3);
			break;
		case TTYPE.KABU:
			u.NewBody(PLANE.GND, SPECIAL.KING);
			u.NewHealth(75);
			u.SetOwner(1,false);
			u.NewClock(4);
			u.SetOnDeath(TTYPE.HSIL, false);
			Roster.Activate(1);
			break;
			
		case TTYPE.DEMO:
			u.NewBody(PLANE.GND);
			u.NewHealth(30);
			u.SetOwner(2,false);
			u.NewClock(3);
			break;
		case TTYPE.MEIN:
			u.NewBody(PLANE.GND);
			u.NewHealth(40);
			u.SetOwner(2,false);
			u.NewClock(4);
			break;
		case TTYPE.PANO:
			u.NewBody(PLANE.GND, SPECIAL.TRAM);
			u.NewHealth(65);
			u.SetOwner(2,false);
			u.NewClock(1);
			break;
		case TTYPE.DECI:
			u.NewBody(PLANE.GND,new SPECIAL[]{SPECIAL.KING, SPECIAL.TRAM});
			u.NewHealth(85,3);
			u.SetOwner(2,false);
			u.NewClock(2);
			u.SetOnDeath(TTYPE.HSTE, false);
			Roster.Activate(2);
			break;
			
		case TTYPE.ROOK:
			u.NewBody(PLANE.GND);
			u.NewHealth(30,5);
			u.SetOwner(3,false);
			u.NewClock(3);
			u.SetOnDeath(TTYPE.NONE,false);
			break;
		case TTYPE.SMAS:
			u.NewBody(PLANE.GND);
			u.NewHealth(30);
			u.SetOwner(3,false);
			u.NewClock(3);
			break;
		case TTYPE.CONF:
			u.NewBody(PLANE.AIR);
			u.NewHealth(40);
			u.SetOwner(3,false);
			u.NewClock(4);
			u.SetOnDeath(TTYPE.ASHE, false);
			break;
		case TTYPE.ASHE:
			u.NewBody(PLANE.GND, new SPECIAL[]{SPECIAL.DEST, SPECIAL.REM});
			u.NewHealth(15);
			u.SetOwner(3,false);
			u.NewClock(2);
			u.SetOnDeath(TTYPE.NONE, false);
			break;
		case TTYPE.BATT:
			u.NewBody(PLANE.GND, SPECIAL.TRAM);
			u.NewHealth(65);
			u.SetOwner(3,false);
			u.NewClock(1);
			break;
		case TTYPE.GARG:
			u.NewBody(PLANE.AIR, SPECIAL.KING);
			u.NewHealth(75);
			u.SetOwner(3,false);
			u.NewClock(3);
			u.SetOnDeath(TTYPE.HSTO, false);
			Roster.Activate(3);
			break;
			
		case TTYPE.GRIZ:
			u.NewBody(PLANE.GND);
			u.NewHealth(25);
			u.SetOwner(4,false);
			u.NewClock(3);
			break;
		case TTYPE.TALO:
			u.NewBody(PLANE.AIR);
			u.NewHealth(45);
			u.SetOwner(4,false);
			u.NewClock(4);
			break;
		case TTYPE.META:
			u.NewBody(PLANE.GND, SPECIAL.TRAM);
			u.NewHealth(50);
			u.SetOwner(4,false);
			u.NewClock(1);
			u.SetOnDeath(TTYPE.TREE, false);
			break;
		case TTYPE.ULTR:
			u.NewBody(PLANE.GND,new SPECIAL[]{SPECIAL.KING, SPECIAL.TRAM});
			u.NewHealth(80);
			u.SetOwner(4,false);
			u.NewClock(2);
			u.SetOnDeath(TTYPE.HFIR, false);
			Roster.Activate(4);
			break;
			
		case TTYPE.REVO:
			u.NewBody(PLANE.GND);
			u.NewHealth(30);
			u.SetOwner(5,false);
			u.NewClock(4);
			break;
		case TTYPE.PIEC:
			u.NewBody(PLANE.GND);
			u.NewHealth(35,3);
			u.SetOwner(5,false);
			u.NewClock(1);
			break;
		case TTYPE.REPR:
			u.NewBody(PLANE.GND);
			u.NewHealth(55);
			u.SetOwner(5,false);
			u.NewClock(2);
			break;
		case TTYPE.OLDT:
			u.NewBody(PLANE.GND, SPECIAL.KING);
			u.NewHealth(85,2);
			u.SetOwner(5,false);
			u.NewClock(3);
			u.SetOnDeath(TTYPE.HBRA,false);
			Roster.Activate(5);
			break;
			
		case TTYPE.LICH:
			u.NewBody(PLANE.GND);
			u.NewHealth(15);
			u.SetOwner(6,false);
			u.NewClock(3);
			u.SetOnDeath(TTYPE.NONE, false);
			break;
		case TTYPE.BEES:
			u.NewBody(PLANE.AIR);
			u.NewHealth(25);
			u.SetOwner(6,false);
			u.NewClock(5);
			break;
		case TTYPE.MYCO:
			u.NewBody(PLANE.GND);
			u.NewHealth(40);
			u.SetOwner(6,false);
			u.NewClock(2);
			break;
		case TTYPE.MART:
			u.NewBody(PLANE.GND, SPECIAL.TRAM);
			u.NewHealth(70);
			u.SetOwner(6,false);
			u.NewClock(4);
			break;
		case TTYPE.BLAC:
			u.NewBody(PLANE.GND, SPECIAL.KING);
			u.NewHealth(75);
			u.SetOwner(6,false);
			u.NewClock(3);
			u.SetOnDeath(TTYPE.HSLK,false);
			Roster.Activate(6);
			break;
			
		case TTYPE.PRIS:
			u.NewBody(PLANE.GND);
			u.NewHealth(15);
			u.SetOwner(7,false);
			u.NewClock(3);
			break;
		case TTYPE.AREN:
			u.NewBody(PLANE.ETH);
			u.NewHealth(55,3);
			u.SetOwner(7,false);
			u.NewClock(1);
			u.SetOnDeath(TTYPE.NONE,false);
			break;
		case TTYPE.PRIE:
			u.NewBody(PLANE.GND);
			u.NewHealth(50,2);
			u.SetOwner(7,false);
			u.NewClock(4);
			break;
		case TTYPE.DREA:
			u.NewBody(PLANE.AIR, SPECIAL.KING);
			u.NewHealth(75,2);
			u.SetOwner(7,false);
			u.NewClock(3);
			u.SetOnDeath(TTYPE.HGLA,false);
			Roster.Activate(7);
			break;
			
		case TTYPE.RECY:
			u.NewBody(PLANE.GND, new SPECIAL[]{SPECIAL.DEST, SPECIAL.REM});
			u.NewHealth(15);
			u.SetOwner(8,false);
			u.NewClock(4);
			break;
		case TTYPE.NECR:
			u.NewBody(PLANE.ETH);
			u.NewHealth(30,5);
			u.SetOwner(8,false);
			u.NewClock(3);
			u.SetOnDeath(TTYPE.NONE,false);
			break;
		case TTYPE.MOUT:
			u.NewBody(PLANE.GND);
			u.NewHealth(30);
			u.SetOwner(8,false);
			u.NewClock(4);
			break;
		case TTYPE.MONO:
			u.NewBody(new PLANE[] {PLANE.GND, PLANE.AIR}, SPECIAL.KING);
			u.NewHealth(100);
			u.SetOwner(8,false);
			u.NewClock(2);
			u.SetOnDeath(TTYPE.HBLO,false);
			Roster.Activate(8);
			break;
		default:
			break;
		}

		if (u.IsSpecial(SPECIAL.KING)) {u.SetMaxAP(3, false);}
	}





}

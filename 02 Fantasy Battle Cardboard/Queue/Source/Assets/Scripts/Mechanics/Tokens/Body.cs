using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tokens {

	public enum PLANE {SUNK, GND, AIR, ETH}
	public enum SPECIAL {NONE, KING, TRAM, DEST, REM}


	public class Body{
		Token parent;

		static int planes = Enum.GetNames(typeof(PLANE)).Length;
		static int specials = Enum.GetNames(typeof(SPECIAL)).Length;

		bool[] plane = new bool[planes];
		bool[] special = new bool[specials];

		string deathCode = "";

		public Body(Token t, PLANE p = PLANE.GND, SPECIAL s = SPECIAL.NONE){
			parent = t;
			SetPlane(p, false);
			SetSpecial(s, false);
		}

		public Body(Token t, PLANE p, SPECIAL[] s){
			parent = t;
			SetPlane(p, false);
			SetSpecial(s, false);
		}

		public Body(Token t, PLANE[] p, SPECIAL s = SPECIAL.NONE){
			parent = t;
			SetPlane(p, false);
			SetSpecial(s, false);
		}

		public Body(Token t, PLANE[] p, SPECIAL[] s){
			parent = t;
			SetPlane(p, false);
			SetSpecial(s, false);
		}

		public void SetOnDeath(string code, bool log=true){
			deathCode = code;
			if (log) {GameLog.Out(parent+" death token set to "+deathCode+".");}
		}
		public string OnDeath(){return deathCode;}

		public void SetPlane (PLANE p, bool log=true){
			for (int i=0; i<planes; i++) {plane[i] = false;}
			switch (p){
			case PLANE.SUNK: plane[0]=true; break;
			case PLANE.GND:  plane[1]=true; break;
			case PLANE.AIR:  plane[2]=true; break;
			case PLANE.ETH:  plane[3]=true; break;
			default:
				GameLog.Debug("Attempt to assign invalid plane.");
				break;
			}
			if (log) {GameLog.Out(parent+" plane set to "+PlaneString()+".");}
		}
		public void SetPlane (PLANE[] ps, bool log=true){
			for (int i=0; i<planes; i++) {plane[i] = false;}
			foreach (PLANE p in ps){
				switch (p){
				case PLANE.SUNK: plane[0]=true; break;
				case PLANE.GND:  plane[1]=true; break;
				case PLANE.AIR:  plane[2]=true; break;
				case PLANE.ETH:  plane[3]=true; break;
				default:
					GameLog.Debug("Attempt to assign invalid plane.");
					break;
				}
			}
			if (log) {GameLog.Out(parent+" plane set to "+PlaneString()+".");}

		}
		public List<PLANE> Plane(){
			List<PLANE> ps = new List<PLANE>();
			if (plane[0]){ps.Add(PLANE.SUNK);}
			if (plane[1]){ps.Add(PLANE.GND);}
			if (plane[2]){ps.Add(PLANE.AIR);}
			if (plane[3]){ps.Add(PLANE.ETH);}
			return ps;
		}
		public void SetSpecial (SPECIAL tc, bool log=true){
			for (int i=0; i<specials; i++) {special[i] = false;}
			switch (tc){
			case SPECIAL.KING: special[1]=true; break;
			case SPECIAL.TRAM: special[2]=true; break;
			case SPECIAL.DEST: special[3]=true; break;
			case SPECIAL.REM:  special[4]=true; break;
			default:
				break;
			}
			if (log) {GameLog.Out(parent+" specials set to "+SpecialString()+".");}

		}

		public void SetSpecial (SPECIAL[] ss, bool log=true){
			for (int i=0; i<specials; i++) {special[i] = false;}
			foreach (SPECIAL s in ss){
				switch (s){
				case SPECIAL.KING: special[1]=true; break;
				case SPECIAL.TRAM: special[2]=true; break;
				case SPECIAL.DEST: special[3]=true; break;
				case SPECIAL.REM:  special[4]=true; break;
				default:
					break;
				}
			}
			if (log) {GameLog.Out(parent+" specials set to "+SpecialString()+".");}
		}
		public List<SPECIAL> Special(){
			List<SPECIAL> ss = new List<SPECIAL>();
			if (special[1]){ss.Add(SPECIAL.KING);}
			if (special[2]){ss.Add(SPECIAL.TRAM);}
			if (special[3]){ss.Add(SPECIAL.DEST);}
			if (special[4]){ss.Add(SPECIAL.REM);}
			if (!special[1] && !special[2] && !special[3] && !special[4]){ss.Add(SPECIAL.NONE);}
			return ss;
		}
		public bool IsSpecial(SPECIAL s){
			if (s==SPECIAL.KING && special[1]){return true;}
			if (s==SPECIAL.TRAM && special[2]){return true;}
			if (s==SPECIAL.DEST && special[3]){return true;}
			if (s==SPECIAL.REM && special[4]) {return true;}
			return false;
		}

		public string PlaneString(){
			string str = "";
			foreach (PLANE p in Plane()){
				if (p == PLANE.SUNK){str += "Sunken, ";}
				if (p == PLANE.GND) {str += "Ground, ";}
				if (p == PLANE.AIR) {str += "Air, ";}
				if (p == PLANE.ETH) {str += "Ethereal, ";}
			}
			char[] trim = new char[2]{' ',','};
			return str.Trim(trim);
		}
		public string SpecialString(){
			string str = "";
			foreach (SPECIAL s in Special()){
				if (s == SPECIAL.KING){str += "Attack King, ";}
				if (s == SPECIAL.TRAM){str += "Trample, ";}
				if (s == SPECIAL.DEST){str += "Destructible, ";}
				if (s == SPECIAL.REM) {str += "Remains, ";}
			}
			char[] trim = new char[2]{' ',','};
			return str.Trim(trim);
		}


	}
}
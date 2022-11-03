﻿using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public enum EPlane {SUNK, GND, AIR, ETH}

	public class Plane : IDeepCopy<Plane> {
		List<EPlane> planes;

		public Plane (EPlane p) {planes = new List<EPlane> {p};}
		public Plane (List<EPlane> p) {planes = p;}

		public void Set (EPlane p) {planes = new List<EPlane> {p};}
		public void Set (List<EPlane> p) {planes = p;}
		
		public List<EPlane> Value {get {return planes;} }

		public Plane DeepCopy () {return new Plane(Value);}

		public bool Is (EPlane p){
			if (planes.Contains(p)) {return true;}
			return false;
		}

		public void Display (Panel p) {
			foreach (EPlane plane in planes) {
				Rect box = p.Box(p.LineH);
				if (GUI.Button(box, "")) {
					//if (GUIInspector.RightClick) {
						TipInspector.Inspect(ETip.PLANE);
					//}
				}
				GUI.Box (box, Icons.Plane(plane), p.s);
				p.NudgeX();
			}
		}

		public static Plane Gnd {get {return new Plane(EPlane.GND);} }
		public static Plane Air {get {return new Plane(EPlane.AIR);} }
		public static Plane Eth {get {return new Plane(EPlane.ETH);} }
		public static Plane Sunk {get {return new Plane(EPlane.SUNK);} }
		public static Plane Tall {get {return new Plane(new List<EPlane> {EPlane.GND, EPlane.AIR});} }
		public static Plane GndSunk {get {return new Plane(new List<EPlane> {EPlane.GND, EPlane.SUNK});} }
	}
}
using System;
using System.Collections.Generic;
using HOA.Map;
using UnityEngine;

namespace HOA.Tokens.Components {

	public class BodyCara : Body{
		List<Sensor> sensors;
		
		public BodyCara(Token t){
			parent = t;
			SetPlane(PLANE.GND, false);
			SetSpecial(SPECIAL.NONE, false);
			OnDeath = TTYPE.CORP;
			sensors = new List<Sensor>();
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				if (CanTrample(newCell)) {Trample(newCell);}
				newCell.Enter(parent);
				
				foreach (Sensor s in sensors) {s.Delete();}
				
				CellGroup shieldCells = newCell.Neighbors(true);
				foreach (Cell c in shieldCells) {
					Sensor s = new SensorCaraShield((Unit)parent, c);
					sensors.Add(s);
					c.AddSensor(s);
				}
				
				return true;
			}	
			if (newCell == TemplateFactory.c) {
				cell = newCell;
				return true;	
			}
			return false;
		}
				
		public override void Exit () {
			cell.Exit(parent);
		}

		public void DestroySensors () {
			foreach (Sensor s in sensors) {s.Delete();}
		}

	}
}
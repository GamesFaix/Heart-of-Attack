using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class Board {

	/*static Cell[,] cells;
	static public void New (int n){
		cells = new Cell[n,n];

	}
	*/

}
public class Cell {
	Unit[] units = new Unit[Enum.GetNames(typeof(PLANE)).Length];
	List<Sensor> sensors = new List<Sensor>();


	public Unit Occupant (PLANE p){
		return units[(int)p];
	}
	public void Occupy (PLANE p, Unit u){
		if (units[(int)p] == default(Unit)){GameLog.Add("Cell already contains token in that plane.",LogIO.DEBUG);}
		else {units[(int)p] = u;}
	}
	public void Empty (PLANE p){
		if (units[(int)p] == default(Unit)){GameLog.Add("Cell does not contain token in that plane.",LogIO.DEBUG);}
		units[(int)p] = default(Unit);
	}
	public List<Sensor> Sensors (){
		return sensors;
	}

	public void AddSensor(Sensor s){
		sensors.Add(s);
	}
	public void RemoveSensor(Sensor s){
		if (sensors.Contains(s)) {sensors.Remove(s);}
		else {GameLog.Add("Attempt to remove invalid sensor from cell.",LogIO.DEBUG);}
	}



}
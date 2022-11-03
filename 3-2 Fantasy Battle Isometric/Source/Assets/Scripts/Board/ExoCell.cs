using System;
using System.Collections.Generic;
using UnityEngine; 

namespace HOA { 

	public class ExoCell : Cell {
	
		public ExoCell (Board board, index2 index) : base(board, index) {}
	
		public override void Enter (Token t) {throw new Exception ("Exocell cannot be entered.");} 
		public override void EnterSunken (Token t) {throw new Exception ("Exocell cannot be entered.");}
		public override void Exit (Token t) {throw new Exception ("Exocell cannot be exited.");}
		public override void Clear () {}

		public override List<Sensor> Sensors () {return new List<Sensor>();}
		public override void AddSensor (Sensor s) {throw new Exception ("Exocell cannot have sensors.");}
		public override void RemoveSensor (Sensor s) {throw new Exception ("Exocell cannot have sensors.");}

		public override bool StopToken (Token t) {return true;}

		public override bool Legal {get {return false;} }
	}
}

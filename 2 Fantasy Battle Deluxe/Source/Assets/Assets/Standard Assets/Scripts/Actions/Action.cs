using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FBI.Actions.Effects;

namespace FBI.Actions {
	public enum TargetingMethod {
		POWERUP, LINE, FREE, ARC, SPIN, AREA, GLOBAL, CREATE
	} 
	
	public class Action {
		public string name;
		public string desc;
		public ActionCost cost;
		
		public TargetingMethod tar;
		public byte range = 0;
		
		public List<ActionEffect> fx;
		
		public Action(){
			fx = new List<ActionEffect>();	
		}
			
		public static Action Move(){
			Action act = new Action();
			act.name = "Move";
			act.desc = "Move executing unit to another cell.";
			act.cost = new ActionCost(1,0);
			act.tar = TargetingMethod.FREE;
			
			ActionEffect fx1 = new ActionEffect();
				fx1.name = "Move unit";
				fx1.desc = "Move a unit to another cell.";
				fx1.mag = 0;
				
			act.fx.Add(fx1);
			return act;
		}		
			
		public static Action Focus(){
			Action act = new Action();
			act.name = "Focus";
			act.desc = "+1FP to executing unit.";
			act.cost = new ActionCost(1,0);
			act.tar = TargetingMethod.POWERUP;
			
			ActionEffect fx1 = new ActionEffect();
				fx1.name = "FP modification";
				fx1.desc = "Change FP of unit.";
				fx1.mag = 1;
			
			act.fx.Add(fx1);
			return act;
		}	
	
	}
}
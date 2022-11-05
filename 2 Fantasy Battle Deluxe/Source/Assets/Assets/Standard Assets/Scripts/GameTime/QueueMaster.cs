using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FBI.GameTime {
	public class GameQueue {
	
		public static TurnQueue<Unit> turnQueue = new TurnQueue<Unit>();
		public static GameClock clock = new GameClock();
		
		public static bool debug = false;
		
		public static IEnumerator EndTurn(){
			Timer t = turnQueue.First().GetTimer();
			
			if (t.end) {yield return t.End();}
			
			yield return turnQueue.Advance();
			yield return clock.Advance();
			
			if (debug == false){turnQueue.PrepareFirst();}
			else {turnQueue.PrepareFirst("debug");}
			
			if (t.upkeep) {yield return t.Upkeep();}
			
			yield return GlobalUpkeepPhase();
		}
		///
		
		public static List<Unit> globalUpkeeps = new List<Unit>();
		
		static bool GlobalUpkeepPhase(){
			foreach (Unit i in globalUpkeeps){
				///do stuff
			}
			return true;
		}
	}
}
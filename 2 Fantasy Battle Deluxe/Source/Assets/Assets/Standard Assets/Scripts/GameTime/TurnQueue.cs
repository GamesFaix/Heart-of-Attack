using System;
using System.Collections;
using System.Collections.Generic;
using FBI.Collections;
using FBI.Tokens;

namespace  FBI.GameTime {
	public class TurnQueue<T> : ListPlus<T> where T: Unit{
		
		public T First(){return this[0];}
		public T Last(){return this[this.Count-1];}
		
		public void MoveToIndex(T unit, int index){
			Remove(unit);
			Insert(index,unit);
		}
		public void MoveToBottom(T unit){
			Remove(unit);
			Add(unit);
		}
		
		public bool Advance(){
			T first = First();
			T last = Last();
			first.stats.ap = 0;
			
			RemoveAt(0);
			if(CanBeSkipped(last) && CanSkip(first, last)){
				Skip(first, last);
			}
			
			else{Add(first);}
			
			for(byte i=0; i<10; i+=1){
					//reset used actions on turn
					//ACoordinator.actUsed[i]=false;
				}
			return true;
		}
		
		bool CanBeSkipped (Unit last){
			if (last.stats.init >= 0) { 
				if (last.stats.skipped == 0) {return true;}
				else {return false;}
			}
			else {
				int possibleSkips = 1 + System.Math.Abs(last.stats.init);
				
				if (last.stats.skipped < possibleSkips) {return true;}
				else {return false;}	
			}	
		}
		
		bool CanSkip(Unit first, Unit last){
			if(last.stats.init < first.stats.init) {return true;}
			else {return false;}		
		}	
		
		void Skip(T first, T last){
			RemoveAt(Count-1);
			Add(first);
			Add(last);
			last.stats.skipped++;
		}
		
		public void PrepareFirst(){
			First().stats.skipped = 0;
			First().stats.ap = 2;	
		}
		public void PrepareFirst(string debug){
			if (debug == "debug"){
				First().stats.skipped = 0;
				First().stats.ap = 3;
				First().stats.fp = 5;
			}
			else {
				First().stats.skipped = 0;
				First().stats.ap = 2;		
			}
		}
	}
}

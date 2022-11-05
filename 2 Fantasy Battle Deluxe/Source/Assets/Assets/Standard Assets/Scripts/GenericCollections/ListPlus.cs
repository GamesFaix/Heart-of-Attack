using System;
using System.Collections;
using System.Collections.Generic;

namespace FBI.Collections{
	public class ListPlus<T> : List<T> {
		Random rand = new Random();
	
	
		public T RandomIndex (){
			if (Count>0){
				int random = rand.Next(0,Count);
				return this[random];
			}
			else {return default(T);}
		}
		
		public bool Shuffle(){
			for (int i=0; i<Count; i++){
				int random = rand.Next(0,Count);
				
				T temp = this[i];
				Insert(i, this[random]);
				Insert(random, temp);
			}
			
			for (int i=(Count-1); i>=0; i--){
				int random = rand.Next(0,Count);
				
				T temp = this[i];
				Insert(i, this[random]);
				Insert(random, temp);
			}
			return true;
		}
		
		public void CopyArray(T[] array){
			Clear();
			for (int i=0; i<array.Length; i++){
				Add(array[i]);	
			}
		}
	}
}
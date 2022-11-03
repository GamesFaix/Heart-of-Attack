using System.Collections.Generic;
using UnityEngine;

public class HOAQueue<t> : Group<t> {



	public void MoveUp(t item, int n){
		for (int i=0; i<=(n-1); i++){
			if (IndexOf(item) != 0){
				int index = list.IndexOf(item);
				t temp = list[index-1];
				list.Remove(temp);
				list.Insert(index, temp);             
			}
		}
	}

	public void MoveDown(t item, int n){
		for (int i=0; i<=(n-1); i++){
			if (IndexOf(item) != Count-1){
				int index = list.IndexOf(item);
				t temp = list[index+1];
				list.Remove(temp);
				list.Insert(index, temp);           	
			}
		}
	}

	public t First {
		get {
			if (list.Count>0) {return list[0];}
			return default(t);
		}
	}
	public t Last {
		get {
			if (list.Count>0) {return list[list.Count-1];}
			return default(t);
		}
	}
}
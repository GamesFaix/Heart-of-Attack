using System.Collections.Generic;
using UnityEngine;

public class HOAQueue<t> : Group<t> {

	public void Shuffle () {
		List<t> old = list;
		
		List<t> shuffled = new List<t>();
		while (old.Count>0){
			int rand = (int)Mathf.Round(UnityEngine.Random.Range(0, old.Count));	
			shuffled.Add(old[rand]);
			old.Remove(old[rand]);
		}
		list = shuffled;
	}
	
	public void Insert (int index, t item) {list.Insert(index, item);}
	
	public int IndexOf (t item) {return list.IndexOf(item);}

	public t Top {
		get {
			if (Count > 0) {return list[0];}
			return default(t);
		}
	}
	
	public t Bottom {
		get {return list[list.Count-1];} 
	}
	
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
}
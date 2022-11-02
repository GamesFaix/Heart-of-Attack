using System.Collections.Generic;
using UnityEngine;

public class Group<t> {

	protected List<t> list;

	public Group () {list = new List<t>();}
	public Group (t item) {list = new List<t>{item};}
	public Group (List<t> items) {list = items;}
	
	public void Add (t item) {if (!Contains(item)) {list.Add(item);} }
	public void Add (Group<t> group) {foreach (t item in group) {Add(item);} }

	public void Remove (t item) {if (Contains(item)) {list.Remove(item);} }
	public void Remove (Group<t> group) {foreach (t item in group) {Remove(item);} }

	public bool Contains (t item) {
		if (list.Contains(item)) {return true;}
		return false;
	}
	
	public int Count {get {return list.Count;} }
	
	public t this[int i] {get { return (t)this.list[i];} }
	
	public t Random () {
		int rand = UnityEngine.Random.Range(0, Count-1);
	//	Debug.Log("random: "+rand);
	//	Debug.Log("indexof: "+list[rand]);
		return list[rand];
	}

	public MyEnumerator GetEnumerator() {return new MyEnumerator(this);}
	
	public class MyEnumerator {
		int n;
		Group<t> bufferGroup;
		public MyEnumerator(Group<t> inputGroup) {bufferGroup = inputGroup; n = -1;}
		public bool MoveNext() {n++; return (n < bufferGroup.Count);}
		public t Current {get {return bufferGroup[n];} }
	}
}

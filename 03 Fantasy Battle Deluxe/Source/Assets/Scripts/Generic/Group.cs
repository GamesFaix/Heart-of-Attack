using System.Collections.Generic;

public class Group<t> {

	List<t> list;
	//constructors
	public Group () {list = new List<t>();}
	public Group (t item) {list = new List<t>{item};}
	public Group (List<t> items) {list = items;}
	
	//list stuff
	public void Add (t item) {if (!Contains(item)) {list.Add(item);} }
	public void Remove (t item) {if (Contains(item)) {list.Remove(item);} }

	public bool Contains (t item) {
		if (list.Contains(item)) {return true;}
		return false;
	}
	
	public int Count {get {return list.Count;} }
	
	public t this[int i] {get { return (t)this.list[i];} }
	
	public t Random () {
		int rand = RandomSync.Range(0, Count);	
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

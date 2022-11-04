using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group<t> : IEnumerable<t>, ICollection<t>, IList<t>{

	protected List<t> list;

	public int Capacity {
		get {return list.Capacity;}
		set {list.Capacity = value;}
	}
	public void TrimExcess() {list.TrimExcess();}

    public bool AllowDuplicates { get; protected set; }

    #region //Constructors
    public Group (int capacity=4, bool allowDuplicates=false) 
    {
        list = new List<t>(capacity);
        AllowDuplicates = allowDuplicates;
    }
	public Group (t item, int capacity=4, bool allowDuplicates=false) 
    {
        list = new List<t>(capacity){item};
        AllowDuplicates = allowDuplicates;
    }
	public Group (IEnumerable<t> items, bool allowDuplicates=false) 
    {
        list = new List<t>(items);
        AllowDuplicates = allowDuplicates;
    }
    #endregion

    //ICollection
	public int Count {get {return list.Count;} }
	public bool IsReadOnly {get {return false;} }

	public void Add (t item) 
    {
        if (Contains(item) && !AllowDuplicates) { return; }
        list.Add(item);
    }
	public void Add (IEnumerable<t> collection) {foreach (t item in collection) {Add(item);} }
	public bool Remove (t item) {
		if (Contains(item)) {
			list.Remove(item);
			return true;
		} 
		return false;
	}
	public void Remove (IEnumerable<t> collection) {foreach (t item in collection) {Remove(item);} }

	public bool Contains (t item) {return (list.Contains(item) ? true: false);}
	public bool Contains (IEnumerable<t> collection) {
		foreach (t item in collection) {
			if (!list.Contains(item)) {return false;}
		}
		return true;
	}

	public void Clear () {list = new List<t>();}
	public void CopyTo (t[] array, int start) {list.CopyTo(array,start);}

	//IEnumerator
	public IEnumerator<t> GetEnumerator() {
		for (int i=0; i< list.Count; i++) {
			yield return list[i];
		}
	}
	IEnumerator IEnumerable.GetEnumerator() {return GetEnumerator();}

	//IList
	public t this[int i] {
		get {return (t)this.list[i];} 
		set {list[i] = value;}	
	}
	public int IndexOf (t item) {return list.IndexOf(item);}
	public void Insert (int n, t item) {list.Insert(n,item);}
	public void RemoveAt (int n) {list.RemoveAt(n);}

	//New methods
	public t Random () {
		int rand = UnityEngine.Random.Range(0, Count-1);
		return list[rand];
	}
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

    #region //Marking
    public List<t> Marked { get; protected set; }
    public void Mark(t item)
    {
        if (Marked == null) Marked = new List<t>();
        if (Contains(item)) Marked.Add(item);
    }
    public void Mark(int i)
    {
        if (i >= 0 && i < Marked.Count) { Mark(this[i]); }
        else { throw new IndexOutOfRangeException(); }
    }
    public void Mark(IEnumerable<t> collection)
    {
        foreach (t item in collection)
        {
            Mark(item);
        }
    }

    public void ClearMarked() { Marked = null; }
    public void RemoveMarked()
    {
        if (Marked != null)
        {
            foreach (t item in Marked) { list.Remove(item); }
            ClearMarked();
        }
    }
    #endregion
}

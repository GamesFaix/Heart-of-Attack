using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOA { 

	public class Matrix<T> : IEnumerable<T> {
	
		public virtual size2 Size {get; protected set;}
		public int Count {get {return Size.Count;} }

		protected T[,] array;

		protected Matrix () {}

		public Matrix (size2 size) {
			Size = size;
			array = new T[Size.x, Size.y];
			for (int j=0; j<Size.y; j++) {
				for (int i=0; i<Size.x; i++) {
					array[i,j] = default(T);
				}
			}
		}

		public Matrix (size2 size, IList<T> list) {
			Size = size;
			if (list.Count != Count) {InvalidArgumentCount();}
			array = new T[Size.x, Size.y];

			int listIndex = 0;
			for (int j=0; j<Size.y; j++) {
				for (int i=0; i<Size.x; i++) {
					array[i,j] = list[listIndex];
					listIndex++;
				}
			}
		}
		public Matrix (size2 size, T[] list) : this(size, new List<T>(list)){}

		public T this[index2 index] {
			get {
				if (Contains(index)) {return array[index.x, index.y];}
				else {InvalidIndex(); return default(T);}
			}
			set {
				if (Contains(index)) {array[index.x, index.y] = value;}
				else {InvalidIndex();}
			}		
		}
		public T this[int x, int y] {
			get {return this[new index2(x,y)];}
			set {this[new index2(x,y)] = value;}		
		}

		public bool Contains (T item) {
			foreach (index2 index in Size) {
				if (this[index] != null) {
					if (this[index].Equals(item)) {return true;}
				}
			}
			return false;
		}

		public bool Contains (index2 index) {
			if (Size.Contains(index)) {return true;}
			return false;
		}

		public bool Contains (index2 index, out T item) {
			if (Size.Contains(index)) {
				item = this[index];
				return true;
			}
			else {
				item = default(T);
				return false;
			}
		}

		protected void InvalidIndex () {throw new Exception("Matrix: Invalid index. Use 'bool TryIndex(index)' or 'bool TryIndex(index, out T)' for satefy.");}

		public T Random {
			get {
				checked {
					int x = (int)RandRange(0, Size.x-1);
					int y = (int)RandRange(0, Size.y-1);
					return array[x,y];
				}
			}
		}
		protected int RandRange (int min, int max) {return UnityEngine.Random.Range(min,max);}

		//IEnumerator
		public IEnumerator<T> GetEnumerator() {
			for (int j=0; j<Size.y; j++) {
				for (int i=0; i<Size.x; i++) {
					yield return array[i,j];
				}
			}
		}
		IEnumerator IEnumerable.GetEnumerator() {return GetEnumerator();}

        public ListSet<T> Periphery
        {
			get {
                ListSet<T> periphery = new ListSet<T>();
				for (int i=0; i<Size.x; i++) {periphery.Add(array[i, 0]);}
				for (int i=0; i<Size.y; i++) {periphery.Add(array[Size.x-1, i]);}
				for (int i=Size.x-1; i>=0; i--) {periphery.Add(array[i, Size.y-1]);}
				for (int i=Size.y-1; i>=0; i--) {periphery.Add(array[0, i]);}
				return periphery;
			}
		}

        public ListSet<index2> PeripheralIndexes
        {
			get {
                ListSet<index2> periphery = new ListSet<index2>();
				for (int i=0; i<Size.x; i++) {periphery.Add(new index2(i, 0));}
				for (int i=0; i<Size.y; i++) {periphery.Add(new index2(Size.x-1, i));}
				for (int i=Size.x-1; i>=0; i--) {periphery.Add(new index2(i, Size.y-1));}
				for (int i=Size.y-1; i>=0; i--) {periphery.Add(new index2(0, i));}
				return periphery;
			}
		}

		protected void InvalidArgumentCount () {throw new Exception("Matrix"+Size+" requires 0 or "+Count+" entries for construction.");}

		public index2 IndexOf (T item) {
			foreach (index2 index in Size) {
				if (this[index].Equals(item)) {return index;}
			}
			Debug.Log("Matrix.IndexOf: Item not in matrix.");
			return index2.MaxValue;
		}
	}
}

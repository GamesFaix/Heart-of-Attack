using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOA { 

	public class Matrix<T> : IEnumerable<T> {
	
		public virtual Int2 Size {get; protected set;}
		public int Count {get {return Size.Product;} }

		protected T[,] array;

		protected Matrix () {}

		public Matrix (Int2 size) {
			Size = size;
			array = new T[Size.x, Size.y];
		}

		public Matrix (Int2 size, IList<T> list) {
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
		public Matrix (Int2 size, T[] list) : this(size, new List<T>(list)){}

		public T this[Int2 index] {
			get {
				T item;
				if (TryIndex(index, out item)) {return item;}
				else {InvalidIndex(); return default(T);}
			}
			set {
				if (TryIndex(index)) {array[index.x, index.y] = value;}
				else {InvalidIndex();}
			}		
		}
		public T this[int x, int y] {
			get {return this[new Int2(x,y)];}
			set {this[new Int2(x,y)] = value;}		
		}

		public bool TryIndex (Int2 index, out T item) {
			item = default(T);
			if (Size.Covers(index)) {
				item = array[index.x, index.y];
				return true;
			}
			else {return false;}
		}
		public bool TryIndex (Int2 index) {
			if (Size.Covers(index)) {return true;}
			return false;
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

		public Group<T> Periphery {
			get {
				Group<T> periphery = new Group<T>();
				for (int i=0; i<Size.x; i++) {periphery.Add(array[i, 0]);}
				for (int i=0; i<Size.y; i++) {periphery.Add(array[Size.x-1, i]);}
				for (int i=Size.x-1; i>=0; i--) {periphery.Add(array[i, Size.y-1]);}
				for (int i=Size.y-1; i>=0; i--) {periphery.Add(array[0, i]);}
				return periphery;
			}
		}

		public Group<Int2> PeripheralIndexes {
			get {
				Group<Int2> periphery = new Group<Int2>();
				for (int i=0; i<Size.x; i++) {periphery.Add(new Int2(i, 0));}
				for (int i=0; i<Size.y; i++) {periphery.Add(new Int2(Size.x-1, i));}
				for (int i=Size.x-1; i>=0; i--) {periphery.Add(new Int2(i, Size.y-1));}
				for (int i=Size.y-1; i>=0; i--) {periphery.Add(new Int2(0, i));}
				return periphery;
			}
		}

		protected void InvalidArgumentCount () {throw new Exception("Matrix"+Size+" requires 0 or "+Count+" entries for construction.");}


	}
}

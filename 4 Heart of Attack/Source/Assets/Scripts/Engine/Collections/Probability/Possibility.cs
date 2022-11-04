using System;

namespace HOA { 

	public class Possibility<T> {

		public T Item {get; private set;}
		public int Frequency {get; private set;}

		public Possibility (T item, int frequency) {
			if (frequency < 0) {
				throw new Exception ("Possibility must have positive frequency.");
			}
			Item = item;
			Frequency = frequency;
		}

	}
}

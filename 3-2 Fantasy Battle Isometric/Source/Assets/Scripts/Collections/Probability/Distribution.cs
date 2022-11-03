using UnityEngine;
using System.Collections.Generic; 

namespace HOA { 

	public class Distribution<T> {

		public List<Possibility<T>> Possibilities {get; private set;}

		public Distribution () {
			Possibilities = new List<Possibility<T>>();
		}

		public void Add (Possibility<T> item) {Possibilities.Add(item);}
		public bool Remove (Possibility<T> item) {
			if (Possibilities.Contains(item)) {
				Possibilities.Remove(item);
				return true;
			}
			return false;
		}

		public int TotalFrequency {
			get {
				int sum = 0;
				foreach (Possibility<T> poss in Possibilities) {
					sum += poss.Frequency;
				}
				return sum;
			}
		}

		public T Random () {
			float random = UnityEngine.Random.Range(1,TotalFrequency);
			int sum = 0;
			foreach (Possibility<T> poss in Possibilities) {
				sum += poss.Frequency;
				if (random < sum) {return poss.Item;}
			}
			return Possibilities[Possibilities.Count-1].Item;
		}
	}
}

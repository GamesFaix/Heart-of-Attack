using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Faction {
		protected string name;
		public string Name {get {return name;} }
		protected List<Species> tokens;
		protected Species king;
		protected Species heart;
		protected Color color1;
		protected Color color2;
		protected bool playable;
		protected AudioClip theme;

		public override string ToString () {return name;}

		public List<Species> Tokens () {return tokens;}
		public Species this[int i] {get { return (Species)this.tokens[i];} }
		public int Count {get {return tokens.Count;} }

		public bool Contains (Species t) {
			if (tokens.Contains(t)) {return true;}
			return false;
		}

		public MyEnumerator GetEnumerator() {return new MyEnumerator(tokens);}
		public class MyEnumerator {
			int n;
			List<Species> bufferGroup;
			public MyEnumerator(List<Species> inputGroup) {bufferGroup = inputGroup; n = -1;}
			public bool MoveNext() {n++; return (n < bufferGroup.Count);}
			public Species Current {get {return bufferGroup[n];} }
		}

		public Species King {get {
		//		Debug.Log("Getting king: "+king);
				return king;
			} }
		public Species Heart {get {return heart;} }
		
		public Color[] Colors {get {return new Color[2] {color1, color2}; } }

		public bool Playable {get {return playable;} }

		public AudioClip Theme {get {return theme;} }
	}
}
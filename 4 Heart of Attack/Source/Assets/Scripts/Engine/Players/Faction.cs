using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Faction {
		protected string name;
		public string Name {get {return name;} }
		protected List<EToken> tokens;
		protected EToken king;
		protected EToken heart;
		protected Color color1;
		protected Color color2;
		protected bool playable;
		protected AudioClip theme;

		public override string ToString () {return name;}

		public List<EToken> Tokens () {return tokens;}
		public EToken this[int i] {get { return (EToken)this.tokens[i];} }
		public int Count {get {return tokens.Count;} }

		public bool Contains (EToken t) {
			if (tokens.Contains(t)) {return true;}
			return false;
		}

		public MyEnumerator GetEnumerator() {return new MyEnumerator(tokens);}
		public class MyEnumerator {
			int n;
			List<EToken> bufferGroup;
			public MyEnumerator(List<EToken> inputGroup) {bufferGroup = inputGroup; n = -1;}
			public bool MoveNext() {n++; return (n < bufferGroup.Count);}
			public EToken Current {get {return bufferGroup[n];} }
		}

		public EToken King {get {
		//		Debug.Log("Getting king: "+king);
				return king;
			} }
		public EToken Heart {get {return heart;} }
		
		public Color[] Colors {get {return new Color[2] {color1, color2}; } }

		public bool Playable {get {return playable;} }

		public AudioClip Theme {get {return theme;} }
	}
}
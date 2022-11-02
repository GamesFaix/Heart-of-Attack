using HOA.Tokens;
using System.Collections.Generic;
using UnityEngine;

public abstract class Faction {
	protected string name;
	protected List<TTYPE> tokens;
	protected TTYPE king;
	protected TTYPE heart;
	protected Color color1;
	protected Color color2;
	protected bool playable;

	public override string ToString () {return name;}

	public List<TTYPE> Tokens () {return tokens;}
	public TTYPE this[int i] {get { return (TTYPE)this.tokens[i];} }
	public int Count {get {return tokens.Count;} }

	public MyEnumerator GetEnumerator() {return new MyEnumerator(tokens);}
	public class MyEnumerator {
		int n;
		List<TTYPE> bufferGroup;
		public MyEnumerator(List<TTYPE> inputGroup) {bufferGroup = inputGroup; n = -1;}
		public bool MoveNext() {n++; return (n < bufferGroup.Count);}
		public TTYPE Current {get {return bufferGroup[n];} }
	}

	public TTYPE King {get {return king;} }
	public TTYPE Heart {get {return heart;} }
	
	public Color[] Colors {get {return new Color[2] {color1, color2}; } }

	public bool Playable {get {return playable;} }
}

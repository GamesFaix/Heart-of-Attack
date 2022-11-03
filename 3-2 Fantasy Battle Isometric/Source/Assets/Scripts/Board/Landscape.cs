using UnityEngine; 
using System.Collections.Generic;

namespace HOA { 

	public class Landscape {
		public Matrix<EToken> Tokens {get; private set;}
		public Board Board {get; private set;}

		public Landscape (Board board) {
			Board = board;
			Tokens = new Matrix<EToken> (Board.CellCount);
			foreach (index2 index in Tokens.Size) {Tokens[index] = EToken.NONE;}
		}

		public void Add (index2 index, EToken token) {
			if (Tokens.PeripheralIndexes.Contains(index)) {
				Debug.Log("Landscape cannot contain Token in border cell.");
			}
			Tokens[index] = token;
		}

		public void Add (index2 start, Terrain terrain) {
			foreach (index2 index in terrain.Size) {
				Add(index+(int2)start, terrain[index]);
			}
		}
	
		public void AddRandom (float density, Distribution<EToken> dist) {
			int totalCells = Tokens.Size.Count - Tokens.PeripheralIndexes.Count;
			int finalTokenCount = (int)Mathf.Round(density * totalCells);
			int currentCount = FullIndexes.Count;
			int newTokenCount = finalTokenCount - currentCount;
			Group<index2> empty = EmptyIndexes;

			for (int i=0; i<newTokenCount; i++) {
				index2 index = empty.Random();
				EToken token = dist.Random();
				Tokens[index] = token;
				empty.Remove(index);
			}
		}

		public void Build () {
			EffectGroup effects = new EffectGroup();
			foreach (index2 index in Tokens.Size) {
				if (Tokens[index] != EToken.NONE) {
					Cell cell = Board.Cell(index);
					effects.Add(new Effects.Create(Source.Neutral, Tokens[index], cell));
				}
			}
			EffectQueue.Add(effects);
		}

		Group<index2> EmptyIndexes {
			get {
				Group<index2> empty = new Group<index2>();
				foreach (index2 index in Tokens.Size) {
					if (!Tokens.PeripheralIndexes.Contains(index)
					 && Tokens[index] == EToken.NONE) {empty.Add(index);}
				}
				return empty;
			}
		}

		Group<index2> FullIndexes {
			get {
				Group<index2> full = new Group<index2>();
				foreach (index2 index in Tokens.Size) {
					if (!Tokens.PeripheralIndexes.Contains(index)
					    && Tokens[index] != EToken.NONE) {full.Add(index);}
				}
				return full;
			}
		}

	}
}

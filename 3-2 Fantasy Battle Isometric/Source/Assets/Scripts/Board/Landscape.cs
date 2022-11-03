using UnityEngine; 
using System.Collections.Generic;

namespace HOA { 

	public class Landscape {
		public Matrix<EToken> Tokens {get; private set;}
		public Board Board {get; private set;}

		public Landscape (Board board) {
			Board = board;
			Tokens = new Matrix<EToken> (Board.CellCount);
			foreach (Int2 index in Tokens.Size) {Tokens[index] = EToken.NONE;}
		}

		public void Add (Int2 index, EToken token) {
			if (Tokens.PeripheralIndexes.Contains(index)) {
				Debug.Log("Landscape cannot contain Token in border cell.");
			}
			Tokens[index] = token;
		}

		public void Add (Int2 start, Terrain terrain) {
			foreach (Int2 index in terrain.Size) {
				Add(index+start, terrain[index]);
			}
		}
	
		public void AddRandom (float density, Distribution<EToken> dist) {
			int totalCells = Tokens.Size.Product - Tokens.PeripheralIndexes.Count;
			int finalTokenCount = (int)Mathf.Round(density * totalCells);
			int currentCount = FullIndexes.Count;
			int newTokenCount = finalTokenCount - currentCount;
			Group<Int2> empty = EmptyIndexes;

			for (int i=0; i<newTokenCount; i++) {
				Int2 index = empty.Random();
				EToken token = dist.Random();
				Tokens[index] = token;
				empty.Remove(index);
			}
		}

		public void Build () {
			EffectGroup effects = new EffectGroup();
			foreach (Int2 index in Tokens.Size) {
				if (Tokens[index] != EToken.NONE) {
					Cell cell = Board.Cell(index);
					effects.Add(new ECreate(Source.Neutral, Tokens[index], cell));
				}
			}
			EffectQueue.Add(effects);
		}

		Group<Int2> EmptyIndexes {
			get {
				Group<Int2> empty = new Group<Int2>();
				foreach (Int2 index in Tokens.Size) {
					if (!Tokens.PeripheralIndexes.Contains(index)
					 && Tokens[index] == EToken.NONE) {empty.Add(index);}
				}
				return empty;
			}
		}

		Group<Int2> FullIndexes {
			get {
				Group<Int2> full = new Group<Int2>();
				foreach (Int2 index in Tokens.Size) {
					if (!Tokens.PeripheralIndexes.Contains(index)
					    && Tokens[index] != EToken.NONE) {full.Add(index);}
				}
				return full;
			}
		}

	}
}

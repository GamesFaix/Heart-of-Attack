using UnityEngine; 
using System.Collections.Generic;

namespace HOA { 

	public class Terrain : Matrix<Species> {
	
		public override size2 Size {get {return Zone.size;} }

		public Terrain () {
			array = new Species[Size.x, Size.y];
			foreach (index2 index in Size) {
				this[index] = Species.None;
			}
		}
		
		public Terrain (IList<Species> list) {
			if (list.Count != Count) {InvalidArgumentCount();}
			array = new Species[Size.x, Size.y];
			
			int listIndex = 0;
			for (int j=0; j<Size.y; j++) {
				for (int i=0; i<Size.x; i++) {
					array[i,j] = list[listIndex];
					listIndex++;
				}
			}
		}
		public Terrain (Species[] list) : this(new List<Species>(list)){}
		
		public Terrain (Terrain source) {
			array = new Species[Size.x, Size.y];
			foreach (index2 index in Size) {
                this[index] = Species.None;
                if (source[index] != Species.None)
                    this[index] = source[index];
			}
		}

		public Terrain FlipHor () {
			Terrain flipped = new Terrain();
			foreach (index2 index in Size) {
				index2 newIndex = index; 
				int median = (int)Mathf.Ceil(Size.x/2);
				int distance = index.x - median;
				if (index.x != median) {
					int newX = index.x - 2*distance;
					newIndex = new index2(newX, index.y);
				}
				flipped[newIndex] = this[index];
			}
			return flipped;
		}
		public Terrain FlipVer () {
			Terrain flipped = new Terrain();
			foreach (index2 index in Size) {
				index2 newIndex = index; 
				int median = (int)Mathf.Ceil(Size.y/2);
				int distance = index.y - median;
				if (index.y != median) {
					int newY = index.y- 2*distance;
					newIndex = new index2(index.x, newY);
				}
				flipped[newIndex] = this[index];
			}
			return flipped;
		}
		public Terrain FlipPos () {
			Terrain flipped = new Terrain();
			foreach (index2 index in Size) {
				index2 newIndex = new index2 (index.y, index.x);
				flipped[newIndex] = this[index];
			}
			return flipped;
		}
		public Terrain FlipNeg () {
			Terrain flipped = new Terrain();
			foreach (index2 index in Size) {
				index2 newIndex = new index2 (index.y*-1, index.x*-1);
				flipped[newIndex] = this[index];
			}
			return flipped;
		}
		
		public Terrain Rotate (int quarters) {
			Terrain rotated = new Terrain(this);
			if (quarters > 0) {
				for (int i=1; i<=quarters; i++) {
					rotated = rotated.FlipHor();
					rotated = rotated.FlipNeg();
				}
			}
			if (quarters < 0) {
				for (int i=-1; i>=quarters; i++) {
					rotated = rotated.FlipNeg();
					rotated = rotated.FlipHor();
				}
			}
			
			return rotated;
		}


		public static Terrain Lake {
			get {
				return new Terrain( new Species[] {
					Species.Water, Species.Water, Species.Water,
					Species.Water, Species.Water, Species.Water,
					Species.Water, Species.Water, Species.Water
				});
			}
		}

		public static Terrain FrozenLake {
			get {
				return new Terrain( new Species[] {
					Species.Ice, Species.Ice, Species.Ice,
					Species.Ice, Species.Ice, Species.Ice,
					Species.Ice, Species.Ice, Species.Ice
				});
			}
		}

		public static Terrain Volcano {
			get {
				return new Terrain( new Species[] {
					Species.Lava, Species.None, Species.Lava,
					Species.None, Species.Mountain, Species.None,
					Species.Lava, Species.None, Species.Lava
				});
			}
		}

		public static Terrain MountainPlus {
			get {
				return new Terrain( new Species[] {
					Species.None, Species.Mountain, Species.None,
					Species.Mountain, Species.Mountain, Species.Mountain,
					Species.None, Species.Mountain, Species.None
				});
			}
		}

		public static Terrain RockCorner {
			get {
				return new Terrain( new Species[] {
					Species.None, Species.Rock, Species.Rock,
					Species.None, Species.None, Species.Rock,
					Species.None, Species.None, Species.None
				});
			}
		}


		public static Terrain Blank {
			get {return new Terrain();}
		}	
	}
}

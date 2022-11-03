using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class Map {

		public static void Blank(int n) {
			Board.New(n);
		}

		static void Spawn (EToken t, int x, int y) {
			terrain.Add(new ECreate(new Source(), t, Board.Cell(x,y)));
		}

		static EffectGroup terrain;

		public static void Map1 () {
			Board.New(10);
			
			terrain = new EffectGroup();

			Spawn(EToken.MNTN, 5,5); 
			Spawn(EToken.MNTN, 5,6);
			Spawn(EToken.MNTN, 6,5); 
			Spawn(EToken.MNTN, 6,6);

			Spawn(EToken.HILL, 2,5); 
			Spawn(EToken.ROCK, 3,5);
			Spawn(EToken.ROCK, 2,6); 
			Spawn(EToken.HILL, 3,6);

			Spawn(EToken.HILL, 5,2); 
			Spawn(EToken.ROCK, 6,2);
			Spawn(EToken.ROCK, 5,3); 
			Spawn(EToken.HILL, 6,3);

			Spawn(EToken.HILL, 8,5); 
			Spawn(EToken.ROCK, 9,5);
			Spawn(EToken.ROCK, 8,6); 
			Spawn(EToken.HILL, 9,6);

			Spawn(EToken.HILL, 5,8); 
			Spawn(EToken.ROCK, 6,8);
			Spawn(EToken.ROCK, 5,9); 
			Spawn(EToken.HILL, 6,9);

			Spawn(EToken.WATR, 3,2); 
			Spawn(EToken.WATR, 2,3);

			Spawn(EToken.TREE, 8,2); 
			Spawn(EToken.TREE, 9,3);

			Spawn(EToken.TREE, 2,8); 
			Spawn(EToken.TREE, 3,9);

			Spawn(EToken.WATR, 9,8); 
			Spawn(EToken.WATR, 8,9);

			EffectQueue.Add(terrain);
		}

		public static void Map2 () {
		
			Board.New(10);
			terrain = new EffectGroup();

			Spawn(EToken.TREE, 5,5);
			Spawn(EToken.TREE, 5,6);
			Spawn(EToken.TREE, 6,5);
			Spawn(EToken.TREE, 6,6);

			Spawn(EToken.MNTN, 3,3);
			Spawn(EToken.MNTN, 3,4);
			Spawn(EToken.MNTN, 4,3);

			Spawn(EToken.MNTN, 7,3);
			Spawn(EToken.MNTN, 8,3);
			Spawn(EToken.MNTN, 8,4);

			Spawn(EToken.MNTN, 3,7);
			Spawn(EToken.MNTN, 3,8);
			Spawn(EToken.MNTN, 4,8);

			Spawn(EToken.MNTN, 8,7);
			Spawn(EToken.MNTN, 7,8);
			Spawn(EToken.MNTN, 8,8);

			Spawn(EToken.LAVA, 2,4);
			Spawn(EToken.LAVA, 10,7);
			Spawn(EToken.LAVA, 1,7);
			Spawn(EToken.ROCK, 4,9);
			Spawn(EToken.ROCK, 9,4);
			Spawn(EToken.LAVA, 7,10);
			Spawn(EToken.LAVA, 4,1);
			Spawn(EToken.LAVA, 7,2);
			EffectQueue.Add(terrain);
		}

		public static void Map3 () {
			Board.New(10);
			terrain = new EffectGroup();

			Spawn(EToken.WATR, 5,4);
			Spawn(EToken.WATR, 6,4);
			Spawn(EToken.WATR, 4,5);
			Spawn(EToken.WATR, 5,5);
			Spawn(EToken.WATR, 6,5);
			Spawn(EToken.WATR, 7,5);
			Spawn(EToken.WATR, 4,6);
			Spawn(EToken.WATR, 5,6);
			Spawn(EToken.WATR, 6,6);
			Spawn(EToken.WATR, 7,6);
			Spawn(EToken.WATR, 5,7);
			Spawn(EToken.WATR, 6,7);

			Spawn(EToken.MNTN, 5,1);
			Spawn(EToken.MNTN, 6,1);
			Spawn(EToken.MNTN, 5,2);
			Spawn(EToken.MNTN, 6,3);

			Spawn(EToken.MNTN, 1,5);
			Spawn(EToken.MNTN, 1,6);
			Spawn(EToken.MNTN, 2,5);
			Spawn(EToken.MNTN, 3,6);

			Spawn(EToken.MNTN, 10,5);
			Spawn(EToken.MNTN, 10,6);
			Spawn(EToken.MNTN, 9,6);
			Spawn(EToken.MNTN, 8,5);

			Spawn(EToken.MNTN, 5,10);
			Spawn(EToken.MNTN, 6,10);
			Spawn(EToken.MNTN, 5,8);
			Spawn(EToken.MNTN, 6,9);

			Spawn(EToken.TREE, 2,8);
			Spawn(EToken.TREE, 3,9);
			Spawn(EToken.HILL, 3,8);
			
			Spawn(EToken.TREE, 3,2);
			Spawn(EToken.TREE, 2,3);
			Spawn(EToken.HILL, 3,3);
			
			Spawn(EToken.TREE, 8,2);
			Spawn(EToken.TREE, 9,3);
			Spawn(EToken.HILL, 8,3);
			
			Spawn(EToken.TREE, 8,9);
			Spawn(EToken.TREE, 9,8);
			Spawn(EToken.HILL, 8,8);

			EffectQueue.Add(terrain);
		}
	}
}
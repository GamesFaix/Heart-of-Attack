using UnityEngine; 

namespace HOA { 

	public class MapFactory {

		public const int zoneSize = 3;
		public static int zonesPerSide=4;
		static int CellsPerSide {get {return zonesPerSide*zoneSize;} }
		public const int min = 2;
		public const int max = 6;

		public static int MaxPlayers () {
			int peripheralZones = 4* (zonesPerSide -1);
			return Mathf.Min(8, peripheralZones/2);
		}

		public static void Build(int map=0) {
			Zone[,] zones = new Zone[0,0];

			if (map==0) {
				Board.New(CellsPerSide);
				zones = Zones();
			}
			if (map==1) {
				zonesPerSide=4;
				Board.New(CellsPerSide);
				zones = Zones1();
			}


			Group<Zone> periphery = Periphery(zones);
			Group<Zone> spawnZones = SpawnZones (periphery);
			SpawnKings(spawnZones);

		}

		static Zone[,] Zones () {
			int zps = zonesPerSide;
			Zone[,] zones = new Zone[zps,zps];
			
			for (int i=0; i<zps; i++){
				for (int j=0; j<zps; j++) {
					int cellX = 1 + zoneSize*i;
					int cellY = 1 + zoneSize*j;
					Cell startCell = Board.Cell(cellX, cellY);
					
					zones[i,j] = new Zone(zoneSize, startCell);
				}
			}
			return zones;
		}

		static Zone[,] Zones1() {return Zones();}

		static Group<Zone> Periphery (Zone[,] zones) {
			int zps = zonesPerSide;
			Group<Zone> periphery = new Group<Zone>();

			for (int i=0; i<zones.GetLength(1); i++) {
				periphery.Add(zones[i,0]);
			}
			for (int i=0; i<zones.GetLength(0); i++) {
				periphery.Add(zones[zps-1, i]);
			}
			for (int i=zps-1; i>=0; i--) {
				periphery.Add(zones[i,zps-1]);
			}
			for (int i=zps-1; i>=0; i--) {
				periphery.Add(zones[0,i]);
			}
			return periphery;
		}

		static Group<Zone> SpawnZones (Group<Zone> periphery) {
			int playerCount = Roster.Players().Count;
			int playerSpacing = (int)Mathf.Floor(periphery.Count/playerCount);

			Group<Zone> spawnZones = new Group<Zone>();
			Zone firstZone = periphery.Random();
			spawnZones.Add(firstZone);
			int index = periphery.IndexOf(firstZone);

			for (int i=2; i<=playerCount; i++) {
				int nextIndex = (index+playerSpacing) % periphery.Count;
				spawnZones.Add(periphery[nextIndex]);
				index = nextIndex;
			}
			return spawnZones;
		}
	
		static void SpawnKings (Group<Zone> spawnZones) {
			spawnZones.Shuffle();
			int zoneIndex = 0;

			EffectGroup heroSpawn = new EffectGroup();
			
			foreach (Player p in Roster.Players()) {
				Cell cell;
				Token temp = TemplateFactory.Template(p.King);

				Zone zone = spawnZones[zoneIndex];

				if (zone.RandomLegalCell(temp, out cell)) {
					heroSpawn.Add(new ECreate (new Source(p), p.King, cell));
				}
				else {
					Debug.Log("Cannot spawn "+temp+". No legal cells.");
				}
				zoneIndex++;
			}
			EffectQueue.Add(heroSpawn);
		}
	}
}

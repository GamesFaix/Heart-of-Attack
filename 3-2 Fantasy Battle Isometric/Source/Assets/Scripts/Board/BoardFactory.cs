using System;
using UnityEngine; 

namespace HOA { 

	public static class BoardFactory {

		public static Group<Zone> SpawnZones (Group<Zone> periphery) {
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
		
		public static void SpawnKings (Group<Zone> spawnZones) {
			spawnZones.Shuffle();
			int zoneIndex = 0;
			
			EffectGroup heroSpawn = new EffectGroup();
			
			foreach (Player p in Roster.Players()) {
				Cell cell;
				Token temp = TokenFactory.Template(p.King);
				
				Zone zone = spawnZones[zoneIndex];
				
				if (zone.RandomLegalCell(temp, out cell)) {
					heroSpawn.Add(new Effects.Create (new Source(p), p.King, cell));
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

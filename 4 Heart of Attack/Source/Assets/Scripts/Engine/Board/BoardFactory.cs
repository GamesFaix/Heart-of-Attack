using System;
using UnityEngine; 

namespace HOA { 

	public static class BoardFactory {

		public static ListSet<Zone> SpawnZones (ListSet<Zone> periphery) {
            int playerCount = Roster.Players.Count;
			int playerSpacing = (int)Mathf.Floor(periphery.Count/playerCount);
			
			ListSet<Zone> spawnZones = new ListSet<Zone>();
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
		
		public static void SpawnKings (ListSet<Zone> spawnZones) {
            spawnZones.Shuffle();
			
			EffectSet heroSpawn = new EffectSet();
			
            for (int i=0; (i < Roster.Players.Count && i < spawnZones.Count); i++)
            {
                Player p = Roster.Players[i];
                Zone z = spawnZones[i];

				Cell cell;
				Token temp = TokenRegistry.Templates[p.Faction.King];

				if (z.RandomLegalCell(temp, out cell)) {
                    heroSpawn.Add(Effect.Create(new Source(p), cell, p.Faction.King));
				}
				else {
					Debug.Log("Cannot spawn "+temp+". No legal cells.");
				}
			}
			EffectQueue.Add(heroSpawn);
		}



	}
}

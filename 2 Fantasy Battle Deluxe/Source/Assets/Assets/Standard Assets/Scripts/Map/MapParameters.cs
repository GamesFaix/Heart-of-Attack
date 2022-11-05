using UnityEngine;
using System.Collections;

namespace FBI.Map{
	public class MapParameters {
		byte levelSelection;
		byte spawnLocation;
		byte obstacleDensity;
	
		public MapParameters(){
			levelSelection = 0;
			spawnLocation = 0;
			obstacleDensity = 10;	
		}
		
		public void SetLevel(byte level){levelSelection = level;}
		public void SetSpawnLocation(byte sl){spawnLocation = sl;}
		public void SetObstacleDensity(byte den){obstacleDensity = den;}
		
		public byte GetLevel(){return levelSelection;}
		public byte GetSpawnLocation(){return spawnLocation;}
		public byte GetObstacleDensity(){return obstacleDensity;}
		
	}
}
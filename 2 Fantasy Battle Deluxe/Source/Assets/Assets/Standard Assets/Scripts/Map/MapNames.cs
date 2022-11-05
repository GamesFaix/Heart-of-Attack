using UnityEngine;
using System.Collections;

namespace FBI.Map {
	public static class MapNames{
	
		public static string[] names;
		
		static MapNames(){
			names = new string[20];
			names[0]="[random]";
			names[1]="Grassafras";
			names[2]="Just Deserts";
			names[3]="Magmountain";
			names[4]="Ice Mountain";
			names[19]="debug";
		}
		
		public static string GetName(byte selection){
			if (names[selection] != null){return names[selection];}
			else{
				Debug.Log("MapNames.GetName: Invalid selection!");
				return null;
			}
		}
	}
}

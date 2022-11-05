using UnityEngine;
using System.Collections;
using FBI.Map;
using FBI.Actions;

namespace FBI.Tokens {

	public static class TBuildGearp {
	
		public static void Ninjoid(Token token){
			token.gameObject.name = "Ninjoid";
			Unit unit = TBuildGeneric.Unit(token);
	
			TokenDisplay display = token.GetDisplay();
			Texture2D sprite = Resources.Load("Images/Sprites/SP_Ninjoid") as Texture2D;
			display.SetSprite(sprite);

			token.SetHeight(CellZ.GND);
			
			UnitStats stats = unit.GetStats();
			stats.comp = Composition.MECH;
			stats.mhp = 10;
			stats.hp = stats.mhp;
			stats.init = 3;

			Action act = unit.actions[0];
			act.fx[0].mag = 4;
			
			
			
			
						
			TBuildGeneric.DebugUnitCreate(unit);
			return;
		}
		
		public static void Sentinel(Token token){
			token.gameObject.name = "Sentinel";
			Unit unit = TBuildGeneric.Unit(token);
	
			TokenDisplay display = token.GetDisplay();
			Texture2D sprite = Resources.Load("Images/Sprites/SP_Sentinel") as Texture2D;
			display.SetSprite(sprite);
			
			token.SetHeight(CellZ.GND);
					
			UnitStats stats = unit.GetStats();
			stats.comp = Composition.MECH;
			stats.mhp = 15;
			stats.hp = stats.mhp;
			stats.init = 2;
			
			Action act = unit.actions[0];
			act.fx[0].mag = 3;
			
			
			TBuildGeneric.DebugUnitCreate(unit);
			return;
		}
		
		public static void Moth(Token token){
			token.gameObject.name = "Moth";
			Unit unit = TBuildGeneric.Unit(token);
	
			TokenDisplay display = token.GetDisplay();
			Texture2D sprite = Resources.Load("Images/Sprites/SP_Moth") as Texture2D;
			display.SetSprite(sprite);
			
			token.SetHeight(CellZ.FLY);
					
			UnitStats stats = unit.GetStats();
			stats.comp = Composition.MECH;
			stats.mhp = 30;
			stats.hp = stats.mhp;
			stats.init = 3;

			Action act = unit.actions[0];
			act.tar = TargetingMethod.LINE;
			act.fx[0].mag = 6;
			
			
			
			TBuildGeneric.DebugUnitCreate(unit);
			return;
		}
		
		public static void Stagbot(Token token){
			token.gameObject.name = "Stagbot";
			Unit unit = TBuildGeneric.Unit(token);
	
			TokenDisplay display = token.GetDisplay();
			Texture2D sprite = Resources.Load("Images/Sprites/SP_Stagbot") as Texture2D;
			display.SetSprite(sprite);
			
			token.SetHeight(CellZ.TRM);
					
			UnitStats stats = unit.GetStats();
			stats.comp = Composition.MECH;
			stats.mhp = 50;
			stats.hp = stats.mhp;
			stats.init = 2;

			Action act = unit.actions[0];
			act.tar = TargetingMethod.LINE;
			act.fx[0].mag = 5;
			
			
			TBuildGeneric.DebugUnitCreate(unit);
			return;
		}
	}
}
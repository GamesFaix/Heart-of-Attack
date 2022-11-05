using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FBI.Actions;

namespace FBI.Tokens {
	public static class TBuildGeneric {
		public static void AddSprite(Token token){
			TokenDisplay tD = token.GetDisplay();	
			tD.CreateSprite();
		}
		
		public static Unit Unit(Token token){
			AddSprite(token);
			
			token.properties = new TokenProperties();
			token.properties.type = TokenType.UNIT;
			token.tokenOnDeath = TokenValue.CORPSE;
			
			Unit unit = token.gameObject.AddComponent("Unit") as Unit;
			unit.token = token;
			unit.stats = new UnitStats();
			unit.actions = new List<Action>();
			
			unit.actions.Add(Action.Move());
			unit.actions.Add(Action.Focus());
		
			return unit;
		}
		
		public static void Obstacle(Token token){
			AddSprite(token);
			
			TokenProperties props = token.properties;
			props.type = TokenType.OBSTACLE;
		
			token.tokenOnDeath = TokenValue.NONE;
			
			return;
		}
		
		public static void DebugUnitCreate(Unit unit){
			UnitStats stats = unit.GetStats();
			string name = unit.token.gameObject.name;
			Debug.Log(name+" built! (" +stats.comp+ "/" +unit.token.GetHeight()+" - HP"+stats.hp+" IN"+stats.init+")");
			Debug.Log("Move speed: "+unit.actions[0].fx[0].mag);
		}
	}
}

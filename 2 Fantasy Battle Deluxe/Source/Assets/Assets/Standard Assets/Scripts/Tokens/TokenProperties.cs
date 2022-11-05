using UnityEngine;
using System.Collections;
using FBI.Map;

namespace FBI.Tokens {
	public enum TokenType {UNIT, OBSTACLE, ITEM}
	
	public class TokenProperties {
		public TokenType type;
		public CellZ height;
		public bool dest = false;
		public bool rem = false;
		
		
	}
}
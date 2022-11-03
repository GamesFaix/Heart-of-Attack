using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class TokenGroup : Group<Token> {
		public TokenGroup (int capacity=4) {list = new List<Token>(capacity);}
		public TokenGroup (Token t, int capacity=4) {list = new List<Token>(capacity){t};}
		public TokenGroup (IEnumerable<Token> t) {list = new List<Token>(t);}

		public TokenGroup units {
			get {
				TokenGroup units = new TokenGroup();
				foreach (Token t in list) {if (t.TokenType.unit) {units.Add(t);} }
				return units;
			}
		}
		
		public TokenGroup obstacles {
			get {
				TokenGroup obstacles = new TokenGroup();
				foreach (Token t in list) {if (t.TokenType.obstacle) {obstacles.Add(t);} }
				return obstacles;
			}
		}
		
		public TokenGroup kings {
			get {
				TokenGroup kings = new TokenGroup();
				foreach (Token t in list) {if (t.TokenType.king) {kings.Add(t);} }
				return kings;
			}
		}
		public TokenGroup hearts {
			get {
				TokenGroup hearts = new TokenGroup();
				foreach (Token t in list) {if (t.TokenType.heart) {hearts.Add(t);} }
				return hearts;
			}
		}
		public TokenGroup destructible {
			get {
				TokenGroup destructible = new TokenGroup();
				foreach (Token t in list) {if (t.TokenType.destructible) {destructible.Add(t);} }
				return destructible;
			}	
		}
		public TokenGroup trample {
			get {
				TokenGroup trample = new TokenGroup();
				foreach (Token t in list) {if (t.TokenType.trample) {trample.Add(t);} }
				return trample;
			}	
		}

		public TokenGroup Keep (Plane plane) {
			TokenGroup output = new TokenGroup();
			foreach (Token t in list) {
				for (byte i=0; i<Plane.count; i++) {
					if (plane.planes[i] && t.Plane.planes[i]) {output.Add(t);}
				}
			}
			return output;
		}
		
		public TokenGroup Keep (Player owner) {
			TokenGroup output = new TokenGroup();
			foreach (Token t in list) {
				if (t.Owner == owner) {output.Add(t);}
			}
			return output;
		}

		public TokenGroup Keep (Token token) {
			TokenGroup output = new TokenGroup();
			foreach (Token t in list) {
				if (t == token) {output.Add(t);}
			}
			return output;
		}
		
		public TokenGroup Keep (IEnumerable<Token> tokens) {
			TokenGroup output = new TokenGroup();
			foreach (Token t in list) {
				foreach (Token token in tokens) {
					if (t == token) {output.Add(t);}
				}
			}
			return output;
		}

		public TokenGroup Keep (EToken token) {
			TokenGroup output = new TokenGroup();
			foreach (Token t in list) {
				if (t.ID.Code == token) {output.Add(t);}
			}
			return output;
		}
		
		public TokenGroup Keep (IEnumerable<EToken> tokens) {
			TokenGroup output = new TokenGroup();
			foreach (Token t in list) {
				foreach (EToken token in tokens) {
					if (t.ID.Code == token) {output.Add(t);}
				}
			}
			return output;
		}
		
		public TokenGroup Exclude (Plane plane) {
			TokenGroup output = new TokenGroup(this);
			foreach (Token t in list) {
				for (byte i=0; i<Plane.count; i++) {
					if (plane.planes[i] && t.Plane.planes[i]) {output.Remove(t);}
				}
			}
			return output;
		}
		public TokenGroup Exclude (Player owner) {
			TokenGroup output = new TokenGroup(this);
			foreach (Token t in list) {
				if (t.Owner == owner) {output.Remove(t);}
			}
			return output;
		}
		public TokenGroup Exclude (EToken token) {
			TokenGroup output = new TokenGroup(this);
			foreach (Token t in list) {
				if (t.ID.Code == token) {output.Remove(t);}
			}
			return output;
		}
		
		public TokenGroup Exclude (IEnumerable<EToken> tokens) {
			TokenGroup output = new TokenGroup(this);
			foreach (Token t in list) {
				foreach (EToken token in tokens) {
					if (t.ID.Code == token) {output.Remove(t);}
				}
			}
			return output;
		}
		public static TokenGroup operator + (TokenGroup tokens, Token other) {tokens.Add(other); return tokens;}
		public static TokenGroup operator + (TokenGroup tokens, IEnumerable<Token> others) {tokens.Add(others); return tokens;}
		
		public static TokenGroup operator - (TokenGroup tokens, Token other) {tokens.Remove(other); return tokens;}
		public static TokenGroup operator - (TokenGroup tokens, IEnumerable<Token> others) {tokens.Remove(others); return tokens;}
		
		public static TokenGroup operator - (TokenGroup tokens, Plane plane) {return tokens.Exclude(plane);}
		public static TokenGroup operator - (TokenGroup tokens, Player player) {return tokens.Exclude(player);}
		public static TokenGroup operator - (TokenGroup tokens, EToken code) {return tokens.Exclude(code);}
		public static TokenGroup operator - (TokenGroup tokens, IEnumerable<EToken> codes) {return tokens.Exclude(codes);}
		
		public static TokenGroup operator / (TokenGroup tokens, IEnumerable<Token> others) {return tokens.Keep(others);}
		public static TokenGroup operator / (TokenGroup tokens, Plane plane) {return tokens.Keep(plane);}
		public static TokenGroup operator / (TokenGroup tokens, Player player) {return tokens.Keep(player);}
		public static TokenGroup operator / (TokenGroup tokens, EToken code) {return tokens.Keep(code);}
		public static TokenGroup operator / (TokenGroup tokens, IEnumerable<EToken> codes) {return tokens.Keep(codes);}
		
		public static implicit operator TargetGroup(TokenGroup tokens) {
			TargetGroup targets = new TargetGroup();
			foreach (Token t in tokens) {targets.Add(t);}
			return targets;
		}

		public CellGroup Cells {
			get {
				CellGroup cells = new CellGroup();
				foreach (Token t in list) {
					cells.Add(t.Body.Cell);
				}
				return cells;
			}
		}
	}
}
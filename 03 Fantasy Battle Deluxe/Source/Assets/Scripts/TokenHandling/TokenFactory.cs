using System;
using System.Collections.Generic;
using HOA.Map;
using HOA.Players;
using UnityEngine;

namespace HOA.Tokens {
	
	public static class TokenFactory {
		static List<Token> tokens = new List<Token>();
		
		public static TokenGroup Tokens {
			get {
				TokenGroup tg = new TokenGroup();
				foreach (Token t in tokens) {tg.Add(t);}
				return tg;		
			}
		}
		
		public static void ClearLegal () {
			foreach (Token t in tokens) {t.Legal = false;}
		}
		
		public static void Remove (Token token) {
			if (tokens.Contains(token)) {tokens.Remove(token);}
		}
		
		public static void Reset() {tokens = new List<Token>();}
		
		public static bool Contains (Token token) {
			if (tokens.Contains(token)) {return true;}
			else {return false;}
		}
	
		public static bool Add(TTYPE code, Source s, Cell c, out Token t, bool log=true) {
			t = MakeToken(code, s);
			if (t.Enter(c)) {
				tokens.Add(t);
				if (t is Unit) {TurnQueue.Add((Unit)t);}
				t.Owner = s.Player;
				if (log && s.Player != Roster.Neutral) {
					GameLog.Out(s+" created " +t+" in cell "+c.ToString()+".");
				}
				return true;
			}
			else {
				GameLog.Debug("TokenFactory: Token cannot be created in that cell.");
				return false;
			}	
		}
		
		public static bool Add(TTYPE code, Source s, Cell c, bool log=true) {
			Token t = MakeToken(code, s);
			if (t.Enter(c)) {
				tokens.Add(t);
				if (t is Unit) {TurnQueue.Add((Unit)t);}
				t.Owner = s.Player;
				if (log && s.Player != Roster.Neutral) {
					GameLog.Out(s+" created " +t+" in cell "+c.ToString()+".");
				}
				return true;
			}
			else {
				GameLog.Debug("TokenFactory: Token cannot be created in that cell.");
				return false;
			}	
		}
		
		static Token MakeToken (TTYPE code, Source s){
			switch(code){
				case TTYPE.KATA: return new Katandroid(s);
				case TTYPE.CARA: return new CarapaceInvader(s);
				case TTYPE.MAWT: return new Mawth(s);
				case TTYPE.KABU: return new Kabutomachine(s);
				case TTYPE.HSIL: return new SiliconHOA(s);
		
				case TTYPE.DEMO: return new Demolitia(s);
				case TTYPE.MEIN: return new MeinSchutz(s);
				case TTYPE.MINE: return new Mine(s);
				case TTYPE.PANO: return new Panopticannon(s);
				case TTYPE.DECI: return new Decimatrix(s);
				case TTYPE.HSTE: return new SteelHOA(s);
	
				case TTYPE.ROOK: return new Rook(s);
				case TTYPE.SMAS: return new Smashbuckler(s);
				case TTYPE.CONF: return new Conflagragon(s);
				case TTYPE.ASHE: return new Ashes(s);	
				case TTYPE.BATT: return new BatteringRambuchet(s);
				case TTYPE.GARG: return new Gargoliath(s);	
				case TTYPE.HSTO: return new StoneHOA(s);
	
				case TTYPE.GRIZ: return new GrizzlyElder(s);
				case TTYPE.TALO: return new TalonedScout(s);
				case TTYPE.META: return new Metaterrainean(s);
				case TTYPE.ULTR: return new Ultratherium(s);
				case TTYPE.HFIR: return new FirHOA(s);
	
				case TTYPE.REVO: return new RevolvingTom(s);
				case TTYPE.PIEC: return new Piecemaker(s);
				case TTYPE.APER: return new Aperture(s);
				case TTYPE.REPR: return new Reprospector(s);
				case TTYPE.OLDT: return new OldThreeHands(s);
				case TTYPE.HBRA: return new BrassHOA(s);
	
				case TTYPE.LICH: return new Lichenthrope(s);
				case TTYPE.BEES: return new Beesassin(s);
				case TTYPE.MYCO: return new Mycolonist(s);
				case TTYPE.MART: return new MartianManTrap(s);
				case TTYPE.BLAC: return new BlackWinnow(s);
				case TTYPE.WEBB: return new Web(s);
				case TTYPE.HSLK: return new SilkHOA(s);
	
				case TTYPE.PRIS: return new PrismGuard(s);
				case TTYPE.AREN: return new ArenaNonSensus(s);
				case TTYPE.PRIE: return new PriestOfNaja(s);
				case TTYPE.DREA: return new DreamReaver(s);
				case TTYPE.HGLA: return new GlassHOA(s);
	
				case TTYPE.RECY: return new Recyclops(s);
				case TTYPE.NECR: return new Necrochancellor(s);
				case TTYPE.MOUT: return new MouthOfTheUnderworld(s);
				case TTYPE.MONO: return new Monolith(s);
				case TTYPE.HBLO: return new BloodHOA(s);
	
				case TTYPE.MNTN: return new Mountain(s);
				case TTYPE.HILL: return new Hill(s);
				case TTYPE.ROCK: return new Rock(s);
				case TTYPE.TREE: return new Tokens.Tree(s);
				case TTYPE.CORP: return new Corpse(s);
				case TTYPE.WATR: return new Water(s);
				case TTYPE.LAVA: return new Lava(s);
				
				default: return default(Token);
			}
		}
	}
}
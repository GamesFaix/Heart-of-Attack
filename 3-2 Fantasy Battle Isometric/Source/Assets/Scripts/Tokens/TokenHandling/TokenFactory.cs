using UnityEngine;
using System;
using System.Collections.Generic;

namespace HOA {
	
	public static class TokenFactory {

		static TokenGroup tokens = new TokenGroup();
		
		public static TokenGroup Tokens {get {return tokens;} }

		/*
		public static bool Contains (Token token) {
			if (tokens.Contains(token)) {return true;}
			else {return false;}
		}
		*/

		public static void Remove (Token token) {tokens.Remove(token);}

		public static bool Add(EToken code, Source s, Cell c, out Token t, bool log=true) {
			t = MakeToken(code, s);
			if (t.Body.CanEnter(c)) {
				tokens.Add(t);
				if (t is Unit) {TurnQueue.Add((Unit)t);}

				InheritOwnership (t, s);

				if (log && s.Player != Roster.Neutral) {
					GameLog.Out(s+" created " +t+" in cell "+c.ToString()+".");
				}
				t.Body.Enter(c);
				/*if (!t.Plane.Is(EPlane.SUNK)) {*/TokenDisplay.Attach(t);//}
				if (t.Plane.Is(EPlane.SUNK)) {c.EnterSunken(t);}

				return true;
			}
			else {
				GameLog.Debug("TokenFactory: Token cannot be created in that cell.");
				return false;
			}	
		}
		
		public static bool Add(EToken code, Source s, Cell c, bool log=true) {
			Token t;
			return Add(code, s, c, out t, log);
		}

		static Token MakeToken (EToken code, Source s){
			switch(code){
				case EToken.KATA: return new Katandroid(s);
				case EToken.CARA: return new CarapaceInvader(s);
				case EToken.MAWT: return new Mawth(s);
				case EToken.KABU: return new Kabutomachine(s);
				case EToken.HSIL: return new SiliconHOA(s);
		
				case EToken.DEMO: return new Demolitia(s);
				case EToken.MEIN: return new MeinSchutz(s);
				case EToken.MINE: return new Mine(s);
				case EToken.PANO: return new Panopticannon(s);
				case EToken.DECI: return new Decimatrix(s);
				case EToken.HSTE: return new SteelHOA(s);
	
				case EToken.ROOK: return new Rook(s);
				case EToken.SMAS: return new Smashbuckler(s);
				case EToken.CONF: return new Conflagragon(s);
				case EToken.ASHE: return new Ashes(s);	
				case EToken.BATT: return new BatteringRambuchet(s);
				case EToken.GARG: return new Gargoliath(s);	
				case EToken.HSTO: return new StoneHOA(s);
	
				case EToken.GRIZ: return new GrizzlyElder(s);
				case EToken.TALO: return new TalonedScout(s);
				case EToken.META: return new Metaterrainean(s);
				case EToken.ULTR: return new Ultratherium(s);
				case EToken.HFIR: return new FirHOA(s);
	
				case EToken.REVO: return new RevolvingTom(s);
				case EToken.PIEC: return new Piecemaker(s);
				case EToken.APER: return new Aperture(s);
				case EToken.REPR: return new Reprospector(s);
				case EToken.OLDT: return new OldThreeHands(s);
				case EToken.HBRA: return new BrassHOA(s);
	
				case EToken.LICH: return new Lichenthrope(s);
				case EToken.BEES: return new Beesassin(s);
				case EToken.MYCO: return new Mycolonist(s);
				case EToken.MART: return new MartianManTrap(s);
				case EToken.BLAC: return new BlackWinnow(s);
				case EToken.WEBB: return new Web(s);
				case EToken.HSLK: return new SilkHOA(s);
	
				case EToken.PRIS: return new PrismGuard(s);
				case EToken.AREN: return new ArenaNonSensus(s);
				case EToken.PRIE: return new PriestOfNaja(s);
				case EToken.DREA: return new DreamReaver(s);
				case EToken.HGLA: return new GlassHOA(s);
	
				case EToken.RECY: return new Recyclops(s);
				case EToken.NECR: return new Necrochancellor(s);
				case EToken.GATE: return new Gatecreeper(s);
				case EToken.MONO: return new Monolith(s);
				case EToken.HBLO: return new BloodHOA(s);
	
				case EToken.MNTN: return new Mountain(s);
				case EToken.HILL: return new Hill(s);
				case EToken.ROCK: return new Rock(s);
				case EToken.TREE: return new HOA.Tree(s);
				case EToken.CORP: return new Corpse(s);
				case EToken.WATR: return new Water(s);
				case EToken.LAVA: return new Lava(s);

				case EToken.TREE2: return new Tree2(s);
				case EToken.TREE3: return new Tree3(s);
				case EToken.TREE4: return new Tree4(s);
				case EToken.HOUS: return new House(s);
				case EToken.COTT: return new Cottage(s);
				case EToken.TEMP: return new Temple(s);
				case EToken.PYRA: return new Pyramid(s);
				case EToken.ANTE: return new Antenna(s);
				case EToken.PYLO: return new Pylon(s);
				case EToken.HOLE: return new Hole(s);
				case EToken.EXHA: return new Exhaust(s);
				case EToken.ICE: return new Ice(s);
				case EToken.TARG: return new Targ(s);
				case EToken.RAMP: return new Rampart(s);
				case EToken.CURS: return new Curse(s);
				case EToken.TSNK: return new TimeSink(s);
				case EToken.TWEL: return new TimeWell(s);



				default: return default(Token);
			}
		}
		
		static void InheritOwnership (Token t, Source s) {
			if (!FactionRef.Neutral().Contains(t.ID.Code)
			    && !t.Special.Is(EType.HEART)) {
				t.Owner = s.Player;
			}
		}

		public static void ClearLegal () {tokens.Legalize(false);}

		public static void Reset() {
			for (int i=tokens.Count-1; i>=0; i--) {
				tokens[i].Die(new Source(), false, false);
			}
			tokens = new TokenGroup();
		}
	}
}
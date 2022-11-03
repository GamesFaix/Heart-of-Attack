using UnityEngine;
using System;
using System.Collections.Generic;
using HOA.Tokens;

namespace HOA {
	
	public static class TokenFactory {

		public static TokenGroup Tokens {get; private set;}

		public static void Remove (Token token) {Tokens.Remove(token);}

		//Creation
		public static bool Create (Source source, EToken code, Cell cell, out Token newToken, bool log=true) {
			newToken = Instantiate(source, code);

			if (newToken.Body.CanEnter(cell)) {
				Register(newToken);
				InheritOwnership (newToken, source);
				if (log) {Log(source, newToken, cell);}
				TokenDisplay.Attach(newToken);
				newToken.Body.Enter(cell);
				if (newToken.Plane.sunken) {cell.EnterSunken(newToken);}
				return true;
			}
			else {
				GameLog.Debug("TokenFactory: Token cannot be created in that cell.");
				return false;
			}	
		}
		
		public static bool Create (Source source, EToken code, Cell cell, bool log=true) {
			Token newToken;
			return Create(source, code, cell, out newToken, log);
		}

		static Token Instantiate (Source source, EToken code) {return constructors[(int)code](source, false);}

		static void Register (Token newToken) {
			Tokens.Add(newToken);
			if (newToken is Unit) {TurnQueue.Add((Unit)newToken);}
		}

		static void InheritOwnership (Token t, Source s) {
			if (!FactionRef.Neutral().Contains(t.ID.Code)
			    && !t.TokenType.heart) {
				t.Owner = s.Player;
			}
		}
		static void Log (Source source, Token newToken, Cell cell) {
			if (source.Player != Roster.Neutral) {
				GameLog.Out(source+" created "+newToken+" in cell "+cell+".");
			}
		}

		//Resetting
		public static void ClearLegal () {((TargetGroup)Tokens).Legalize(false);}

		public static void Reset() {
			if (Tokens != null) {
				for (int i=Tokens.Count-1; i>=0; i--) {
					Tokens[i].Die(new Source(), false, false);
				}
			}
			Tokens = new TokenGroup();
		}

		//Templates
		static Token[] templates;
		public static Token Template (EToken code) {return templates[(int)code];}
		public static Token Template (Token t) {return Template(t.ID.Code);}

		//Setup
		public static void Setup () {
			LoadConstructors();
			MakeTemplates();
		}

		delegate Token TokenConstructor(Source source, bool template);
		static TokenConstructor[] constructors;

		static void LoadConstructors () {
			int tokenCount = Enum.GetValues(typeof(EToken)).Length;
			constructors = new TokenConstructor[tokenCount];
			
			constructors[(int)EToken.NONE] = NullToken;

			constructors[(int)EToken.KATA] = Katandroid.Instantiate;
			constructors[(int)EToken.CARA] = CarapaceInvader.Instantiate;
			constructors[(int)EToken.MAWT] = Mawth.Instantiate;
			constructors[(int)EToken.KABU] = Kabutomachine.Instantiate;
			
			constructors[(int)EToken.DEMO] = Demolitia.Instantiate;
			constructors[(int)EToken.MEIN] = MeinSchutz.Instantiate;
			constructors[(int)EToken.MINE] = Mine.Instantiate;
			constructors[(int)EToken.PANO] = Panopticannon.Instantiate;
			constructors[(int)EToken.DECI] = Decimatrix.Instantiate;
			
			constructors[(int)EToken.SMAS] = Smashbuckler.Instantiate;
			constructors[(int)EToken.ROOK] = Rook.Instantiate;
			constructors[(int)EToken.CONF] = Conflagragon.Instantiate;
			constructors[(int)EToken.ASHE] = Ashes.Instantiate;
			constructors[(int)EToken.BATT] = BatteringRambuchet.Instantiate;
			constructors[(int)EToken.GARG] = Gargoliath.Instantiate;
			
			constructors[(int)EToken.GRIZ] = GrizzlyElder.Instantiate;
			constructors[(int)EToken.TALO] = TalonedScout.Instantiate;
			constructors[(int)EToken.META] = Metaterrainean.Instantiate;
			constructors[(int)EToken.ULTR] = Ultratherium.Instantiate;
			
			constructors[(int)EToken.REVO] = RevolvingTom.Instantiate;
			constructors[(int)EToken.PIEC] = Piecemaker.Instantiate;
			constructors[(int)EToken.APER] = Aperture.Instantiate;
			constructors[(int)EToken.REPR] = Reprospector.Instantiate;
			constructors[(int)EToken.OLDT] = OldThreeHands.Instantiate;
			
			constructors[(int)EToken.LICH] = Lichenthrope.Instantiate;
			constructors[(int)EToken.WEBB] = Web.Instantiate;
			constructors[(int)EToken.BEES] = Beesassin.Instantiate;
			constructors[(int)EToken.MYCO] = Mycolonist.Instantiate;
			constructors[(int)EToken.MART] = MartianManTrap.Instantiate;
			constructors[(int)EToken.BLAC] = BlackWinnow.Instantiate;
			
			constructors[(int)EToken.PRIS] = PrismGuard.Instantiate;
			constructors[(int)EToken.AREN] = ArenaNonSensus.Instantiate;
			constructors[(int)EToken.PRIE] = PriestOfNaja.Instantiate;
			constructors[(int)EToken.DREA] = DreamReaver.Instantiate;
			
			constructors[(int)EToken.RECY] = Recyclops.Instantiate;
			constructors[(int)EToken.NECR] = Necrochancellor.Instantiate;
			constructors[(int)EToken.GATE] = Gatecreeper.Instantiate;
			constructors[(int)EToken.MONO] = Monolith.Instantiate;
			
			constructors[(int)EToken.HSIL] = SiliconHOA.Instantiate;
			constructors[(int)EToken.HSTE] = SteelHOA.Instantiate;
			constructors[(int)EToken.HSTO] = StoneHOA.Instantiate;
			constructors[(int)EToken.HFIR] = FirHOA.Instantiate;
			constructors[(int)EToken.HBRA] = BrassHOA.Instantiate;
			constructors[(int)EToken.HSLK] = SilkHOA.Instantiate;
			constructors[(int)EToken.HGLA] = GlassHOA.Instantiate;
			constructors[(int)EToken.HBLO] = BloodHOA.Instantiate;
			
			constructors[(int)EToken.MNTN] = Mountain.Instantiate;
			constructors[(int)EToken.HILL] = Hill.Instantiate;
			constructors[(int)EToken.ROCK] = Rock.Instantiate;
			constructors[(int)EToken.TREE] = HOA.Tokens.Tree.Instantiate;
			constructors[(int)EToken.TREE2] = Tree2.Instantiate;
			constructors[(int)EToken.TREE3] = Tree3.Instantiate;
			constructors[(int)EToken.TREE4] = Tree4.Instantiate;
			constructors[(int)EToken.HOUS] = House.Instantiate;
			constructors[(int)EToken.COTT] = Cottage.Instantiate;
			constructors[(int)EToken.CORP] = Corpse.Instantiate;
			
			constructors[(int)EToken.LAVA] = Lava.Instantiate;
			constructors[(int)EToken.WATR] = Water.Instantiate;
			constructors[(int)EToken.ICE] = Ice.Instantiate;
			constructors[(int)EToken.EXHA] = Exhaust.Instantiate;
			constructors[(int)EToken.HOLE] = Hole.Instantiate;
			constructors[(int)EToken.ANTE] = Antenna.Instantiate;
			constructors[(int)EToken.PYLO] = Pylon.Instantiate;
			
			constructors[(int)EToken.PYRA] = Pyramid.Instantiate;
			constructors[(int)EToken.TEMP] = Temple.Instantiate;
			constructors[(int)EToken.CURS] = Curse.Instantiate;
			constructors[(int)EToken.TARG] = Targ.Instantiate;
			constructors[(int)EToken.TWEL] = TimeWell.Instantiate;
			constructors[(int)EToken.TSNK] = TimeSink.Instantiate;
			constructors[(int)EToken.RAMP] = Rampart.Instantiate;
		}

		static Token NullToken (Source source, bool template) {return null;}

		static void MakeTemplates () {
			int tokenCount = Enum.GetValues(typeof(EToken)).Length;

			templates = new Token[tokenCount];

			templates[0] = null;

			for (int i=1; i< tokenCount; i++) {
				Token template = constructors[i](Source.Neutral, true);
				template.Owner = Roster.Neutral;
				TokenType type = template.TokenType;
				type.template = true;
				template.TokenType = type;
				templates[i] = template;
			}
		}		
	}
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public static class TemplateFactory {
	
		static Dictionary<TTYPE, Token> templates = new Dictionary<TTYPE, Token>();
		public static Cell c = new Cell (-1,-1);
		public static Player p = new Player ("TEMPLATE", false);
		static Source s = new Source(p);	
		
		public static void MakeTemplates () {
			
			foreach (TTYPE code in (TTYPE[])Enum.GetValues(typeof(TTYPE))) {
				if (code != TTYPE.NONE) {
					Token template = Make(code);
		//			Debug.Log(template);
					template.BuildTemplate();
					templates.Add(code, template);
				}
			}		
		}
		
		public static Token Template (TTYPE code) {
			return templates[code];
		}
		
		public static Token Template (Token t) {
			return templates[t.Code];
		}
	
		
		static Token Make (TTYPE code){
			switch(code){
				case TTYPE.KATA: return new Katandroid(s,true);
				case TTYPE.CARA: return new CarapaceInvader(s,true);
				case TTYPE.MAWT: return new Mawth(s,true);
				case TTYPE.KABU: return new Kabutomachine(s,true);
				case TTYPE.HSIL: return new SiliconHOA(s,true);
		
				case TTYPE.DEMO: return new Demolitia(s,true);
				case TTYPE.MEIN: return new MeinSchutz(s,true);
				case TTYPE.MINE: return new Mine(s,true);
				case TTYPE.PANO: return new Panopticannon(s,true);
				case TTYPE.DECI: return new Decimatrix(s,true);
				case TTYPE.HSTE: return new SteelHOA(s,true);
	
				case TTYPE.ROOK: return new Rook(s,true);
				case TTYPE.SMAS: return new Smashbuckler(s,true);
				case TTYPE.CONF: return new Conflagragon(s,true);
				case TTYPE.ASHE: return new Ashes(s,true);	
				case TTYPE.BATT: return new BatteringRambuchet(s,true);
				case TTYPE.GARG: return new Gargoliath(s,true);	
				case TTYPE.HSTO: return new StoneHOA(s,true);
	
				case TTYPE.GRIZ: return new GrizzlyElder(s,true);
				case TTYPE.TALO: return new TalonedScout(s,true);
				case TTYPE.META: return new Metaterrainean(s,true);
				case TTYPE.ULTR: return new Ultratherium(s,true);
				case TTYPE.HFIR: return new FirHOA(s,true);
	
				case TTYPE.REVO: return new RevolvingTom(s,true);
				case TTYPE.PIEC: return new Piecemaker(s,true);
				case TTYPE.APER: return new Aperture(s,true);
				case TTYPE.REPR: return new Reprospector(s,true);
				case TTYPE.OLDT: return new OldThreeHands(s,true);
				case TTYPE.HBRA: return new BrassHOA(s,true);
	
				case TTYPE.LICH: return new Lichenthrope(s,true);
				case TTYPE.BEES: return new Beesassin(s,true);
				case TTYPE.MYCO: return new Mycolonist(s,true);
				case TTYPE.MART: return new MartianManTrap(s,true);
				case TTYPE.BLAC: return new BlackWinnow(s,true);
				case TTYPE.WEBB: return new Web(s,true);
				case TTYPE.HSLK: return new SilkHOA(s,true);
	
				case TTYPE.PRIS: return new PrismGuard(s,true);
				case TTYPE.AREN: return new ArenaNonSensus(s,true);
				case TTYPE.PRIE: return new PriestOfNaja(s,true);
				case TTYPE.DREA: return new DreamReaver(s,true);
				case TTYPE.HGLA: return new GlassHOA(s,true);
	
				case TTYPE.RECY: return new Recyclops(s,true);
				case TTYPE.NECR: return new Necrochancellor(s,true);
				case TTYPE.MOUT: return new MouthOfTheUnderworld(s,true);
				case TTYPE.MONO: return new Monolith(s,true);
				case TTYPE.HBLO: return new BloodHOA(s,true);
	
				case TTYPE.MNTN: return new Mountain(s,true);
				case TTYPE.HILL: return new Hill(s,true);
				case TTYPE.ROCK: return new Rock(s,true);
				case TTYPE.TREE: return new HOA.Tree(s,true);
				case TTYPE.CORP: return new Corpse(s,true);
				case TTYPE.WATR: return new Water(s,true);
				case TTYPE.LAVA: return new Lava(s,true);
	
				default: return default(Token);
			}
		}
	}
}

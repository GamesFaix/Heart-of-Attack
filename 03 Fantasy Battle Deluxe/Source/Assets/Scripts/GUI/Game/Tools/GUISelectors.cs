using UnityEngine;
using System.Collections;

namespace HOA {

	public class GUISelectors : MonoBehaviour {

		public static void Reset () {
			token = EToken.NONE;
			instance = default(Token);	
			tokenScroll = new Vector2 (0,0);
			instanceScroll = new Vector2 (0,0);
			cell = default(Cell);
			Board.ClearLegal();
			TokenFactory.ClearLegal();
			waitForCell = false;
			waitForInstance = false;
			request = default(Request);
		}
		
		static float btnW = 400/9;
			
		//Token Types
		static EToken token = EToken.NONE;
		public static EToken Token {
			get {return token;}
			set {token = value;}
		}

		static Vector2 tokenScroll = new Vector2 (0,0);
		
		public static void TokenGrid (Panel p){
			GUI.Box (p.FullBox, "");
			
			float internalW = btnW * FactionRef.Count;
			float internalH = btnW * FactionRef.LargestSize;
			float x3 = p.x2;
			
			tokenScroll = GUI.BeginScrollView (p.FullBox, tokenScroll, new Rect(p.X, p.Y, internalW, internalH));
			
			for (int i=0; i<FactionRef.Count; i++){
				Faction faction = FactionRef.Index(i);
				
				for (int j=0; j<faction.Count; j++){
					p.x2 = x3;
					Rect box = new Rect(p.x2, p.y2, btnW, btnW);
					if (GUI.Button(box, Thumbs.CodeToThumb(faction[j]))){
						token = faction[j];
					}
					p.y2 += btnW;
				}
				p.ResetY();
				x3 += btnW;
			}
			GUI.EndScrollView();
		}

		//Token Instances
		static Token instance;
		public static Token Instance {
			get {return instance;}
			set {instance = value;}
		}

		static Vector2 instanceScroll = new Vector2 (0,0);

		public static void InstanceGrid (Panel p){
			GUI.Box (p.FullBox, "");
			
			float internalW = btnW * Roster.Count(true);
			float internalH = btnW * Roster.LargestTeamSize;
			float x3 = p.x2;
			
			instanceScroll = GUI.BeginScrollView (p.FullBox, instanceScroll, new Rect(p.X, p.Y, internalW, internalH));

			//Debug.Log(Roster.Count(true));

			for (int i=0; i<Roster.Count(true); i++){
				Player player = Roster.Players(true)[i];
				//Debug.Log(player);

				p.x2 = x3+10;
				GUI.Label(p.Box(btnW), player.ToString());
				p.NextLine();
				
				foreach (Token t in player.OwnedUnits){
					p.x2 = x3;
					Rect box = new Rect(p.x2, p.y2, btnW, btnW);
					if (GUI.Button(box, "")) {Instance = t;
						Debug.Log("instance selected");}
					p.ResetX();
					t.Draw(box);
					
					p.y2 += btnW;
				}
				p.ResetY();
				x3 += btnW;

			}

			GUI.EndScrollView();
		}
		
		static Cell cell = default(Cell);

		public static Cell Cell {
			get {return cell;}
			set {cell = value;}
		}

		//waiting
		static bool waitForCell = false;
		public static bool WaitForCell {
			get {return waitForCell;}
			set {waitForCell = value;}
		}

		static bool waitForInstance = false;
		public static bool WaitForInstance {
			get {return waitForInstance;}
			set {waitForInstance = value;}
		}

		static Request request;
		
		public static void DoWithCell (Request r) {
			request = r;
			waitForCell = true;	
		}

		public static void DoWithInstance (Request r) {
			request = r;
			waitForInstance = true;
		}
		
		static bool LegalCellSelection () {
			if (cell != default(Cell) && cell.IsLegal()) {
				return true;
			}
			return false;
		}
		
		static bool LegalInstanceSelection () {
			if (instance !=default(Token) && instance.IsLegal()) {
				return true;	
			}
			return false;
		}
		
		public void Update () {
			if (waitForCell && LegalCellSelection()) {
				if (request is RCellSelect) {
					RCellSelect r2 = (RCellSelect)request;
					r2.cell = cell;
					InputBuffer.Submit(r2);
				//	Reset();
				}
			}
			
			if (waitForInstance && LegalInstanceSelection()) {
				if (request is RInstanceSelect) {
					RInstanceSelect r2 = (RInstanceSelect)request;
					r2.instance = instance;
					InputBuffer.Submit(r2);
				//	Reset();
				}
			}
		}
	}
}
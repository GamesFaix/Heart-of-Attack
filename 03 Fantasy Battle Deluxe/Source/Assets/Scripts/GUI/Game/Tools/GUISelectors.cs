using UnityEngine;
using System.Collections;
using HOA.Tokens;
using HOA.Map;
using HOA.Players;

public class GUISelectors : MonoBehaviour {
	public static void Reset () {
		token = TTYPE.NONE;
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
	static TTYPE token = TTYPE.NONE;
	public static TTYPE Token () {return token;}
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
	public static Token Instance () {return instance;}
	static Vector2 instanceScroll = new Vector2 (0,0);
	public static void SelectInstance (Token t) {instance = t;}
	
	public static void InstanceGrid (Panel p){
		GUI.Box (p.FullBox, "");
		
		int playerCount = Roster.Count;
		float internalW = btnW * playerCount;
		float internalH = btnW * Roster.LargestTeamSize;
		float x3 = p.x2;
		
		instanceScroll = GUI.BeginScrollView (p.FullBox, instanceScroll, new Rect(p.X, p.Y, internalW, internalH));
		
		for (int i=0; i<playerCount; i++){
			p.x2 = x3+10;
			GUI.Label(p.Box(btnW), Roster.Players(true)[i].ToString());
			p.NextLine();
			
			TokenGroup team = Roster.Players(true)[i].OwnedUnits;
			
			foreach (Token t in team){
				p.x2 = x3;
				Rect box = new Rect(p.x2, p.y2, btnW, btnW);
				if (GUI.Button(box, "")) {SelectInstance(t);}
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
	public static void SelectCell (Cell newCell) {cell = newCell;}
	public static Cell Cell () {return cell;}
	
	//waiting
	static bool waitForCell = false;
	static bool waitForInstance = false;
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
		if (cell != default(Cell) && cell.Legal) {
			return true;
		}
		return false;
	}
	
	static bool LegalInstanceSelection () {
		if (instance !=default(Token) && instance.Legal) {
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
				Reset();
			}
		}
		
		if (waitForInstance && LegalInstanceSelection()) {
			if (request is RInstanceSelect) {
				RInstanceSelect r2 = (RInstanceSelect)request;
				r2.instance = instance;
				InputBuffer.Submit(r2);
				Reset();
			}
		}
	}
}

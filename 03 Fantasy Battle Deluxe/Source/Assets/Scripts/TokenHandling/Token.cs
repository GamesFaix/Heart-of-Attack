using System.Collections.Generic;
using UnityEngine;
using HOA.Map;
using HOA.Players;
using HOA.Tokens.Components;

namespace HOA.Tokens {
	public enum PLANE {SUNK, GND, AIR, ETH}
	public enum SPECIAL {NONE, KING, TRAM, DEST, REM, HOA}
	
	public abstract class Token {
		protected bool isTemplate = false;
		public void BuildTemplate () {isTemplate = true;}
		public bool IsTemplate () {return isTemplate;}
		public Token Template () {
			if (IsTemplate()) {return this;}
			return TemplateFactory.Template(Code);
		}
		
		protected Label label;
		protected Body body;
		protected HOASprite sprite;
			
		protected void NewLabel (TTYPE code, Source s, bool unique=false, bool template=false) {label = new Label(this, code, s, unique, template);}
		protected void NewBody (PLANE p, SPECIAL s=SPECIAL.NONE) {body = new Body(this, p, s);}
		protected void NewBody (PLANE p, SPECIAL[] s) {body = new Body(this, p, s);}
		protected void NewBody (PLANE[] p, SPECIAL s=SPECIAL.NONE) {body = new Body(this, p, s);}
		protected void NewBody (PLANE[] p, SPECIAL[] s) {body = new Body(this, p, s);}		
		
		public abstract string Notes ();
		//name/instance
		public override string ToString () {return label.FullName;}
		public string FullName {get {return label.FullName;} }
		public TTYPE Code {get {return label.Code;} }
		public string CodeInst {get {return label.CodeInst;} }
		public string Name {get {return label.Name;} }
		public char Instance {get {return label.Instance;} }
		public bool Unique {get {return label.Unique;} }
		//possession
		public Player Owner {
			get {return label.Owner;} 
			set {label.Owner = value;}
		}
		//graphics
		public Texture2D Thumb {get {return sprite.Thumb;} }
		public void Draw (Rect rect) {sprite.Draw(rect);}
		public void SpriteEffect (EFFECT e) {sprite.Effect(e);}
		
		//plane
		public List<PLANE> Plane {get {return body.Plane;} }
		public void SetPlane (PLANE p, bool log=true) {body.SetPlane(p, log);}
		public void SetPlane (PLANE[] ps, bool log=true) {body.SetPlane(ps, log);}
		public string PlaneString {get {return body.PlaneString;} }
		public bool IsPlane (PLANE p) {return body.IsPlane(p);}
		//special
		public void SetSpecial (SPECIAL s, bool log=true) {body.SetSpecial(s, log);}
		public void SetSpecial (SPECIAL[] ss, bool log=true) {body.SetSpecial(ss, log);}
		public void AddSpecial (SPECIAL s) {body.AddSpecial(s);}
		public void RemoveSpecial (SPECIAL s) {body.RemoveSpecial(s);}
		public bool IsSpecial (SPECIAL s) {return body.IsSpecial(s);}
		public string SpecialString {get {return body.SpecialString;} }
		//on death
		public TTYPE OnDeath {
			get {return body.OnDeath;}
			set {body.OnDeath = value;}
		}
		
		//location
		public TokenGroup Neighbors (bool cellMates = false) {return body.Neighbors(cellMates);}
		public TokenGroup CellMates {get {return body.CellMates;} }
		public Cell Cell {get {return body.Cell;} }
		public bool CanEnter (Cell cell) {return body.CanEnter(cell);}
		public bool Enter (Cell cell) {return body.Enter(cell);}
		public void Exit () {body.Exit();}
		
		public void Die (Source s, bool corpse=true, bool log=true) {
			if (this is Unit) {TurnQueue.Remove((Unit)this);}
			TokenFactory.Remove(this);
			Cell oldCell = Cell;
			Exit();
			if (corpse) {CreateRemains(oldCell);}
			if (IsSpecial(SPECIAL.KING)) {Owner.Kill();}
			if (log) {GameLog.Out(s.Token+" killed "+this+".");}
			
		}
		
		void CreateRemains (Cell oldCell) {
			if (OnDeath != TTYPE.NONE) {
				Token remains = default(Token);
				if (TokenFactory.Add(OnDeath, new Source(this), oldCell, out remains, false)) {
					GameLog.Out(this+" left "+remains);
				}
			}
		}
		
		bool legal = false;
		public bool Legal {
			get {return legal;} 
			set {legal = value;}
		}
	}
}
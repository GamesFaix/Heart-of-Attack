using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Token : ITargetable {

		protected bool isTemplate = false;
		public void BuildTemplate () {isTemplate = true;}
		public bool IsTemplate () {return isTemplate;}
		public Token Template () {
			if (IsTemplate()) {return this;}
			return TemplateFactory.Template(Code);
		}
		
		protected Label label;
		protected Body body;
		protected HOA.Sprite sprite;
			
		protected void NewLabel (EToken code, Source s, bool unique=false, bool template=false) {label = new Label(this, code, s, unique, template);}
		protected void NewBody (EPlane p, EClass s) {body = new Body(this, p, s);}
		protected void NewBody (EPlane p, List<EClass> s) {body = new Body(this, p, s);}
		protected void NewBody (List<EPlane> p, EClass s) {body = new Body(this, p, s);}
		protected void NewBody (List<EPlane> p, List<EClass> s) {body = new Body(this, p, s);}	

		public Body Body {get {return body;} }
		
		public abstract string Notes ();
		//name/instance
		public override string ToString () {return label.FullName;}
		public string FullName {get {return label.FullName;} }
		public EToken Code {get {return label.Code;} }
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
		public void SpriteEffect (EEffect e) {sprite.Effect(e);}
		public void SpriteMove (Cell c) {sprite.Move(c);}

		//plane
		public List<EPlane> Plane {get {return body.Plane;} }
		public void SetPlane (EPlane p) {body.SetPlane(p);}
		public void SetPlane (List<EPlane> p) {body.SetPlane(p);}
		public bool IsPlane (EPlane p) {return body.IsPlane(p);}
		//special
		public void SetClass (EClass c) {body.SetClass(c);}
		public void SetClass (List<EClass> c) {body.SetClass(c);}
		public void AddClass (EClass c) {body.AddClass(c);}
		public void RemoveClass (EClass c) {body.RemoveClass(c);}
		public bool IsClass (EClass c) {return body.IsClass(c);}
		//on death
		public EToken OnDeath {
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
		public bool Swap (Token other) {return body.Swap(other);}
		
		public virtual void Die (Source s, bool corpse=true, bool log=true) {
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}

			//Debug.Log(Name+" is dying");
			bool top = false;
			if (this == TurnQueue.Top) {top = true;}
			if (this is Unit) {
				TurnQueue.Remove((Unit)this);
				//Debug.Log("Removing "+Name+" from queue");
			}
			if (top) {TurnQueue.PrepareNewTop(TurnQueue.Top);} 

			TokenFactory.Remove(this);
			Cell oldCell = Cell;
			Exit();
			if (corpse) {CreateRemains(oldCell);}
			if (IsClass(EClass.KING)) {Owner.Kill();}
			if (log && !IsClass(EClass.HEART)) {
				if (s.Token != default(Token)) {GameLog.Out(s.Token.ToString()+" killed "+this+".");}
				else {GameLog.Out(this+" has been killed.");}
			}
		}
		
		protected void CreateRemains (Cell oldCell) {
			if (OnDeath != EToken.NONE) {
				Token remains = default(Token);
				if (TokenFactory.Add(OnDeath, new Source(this), oldCell, out remains, false)) {
					GameLog.Out(this+" left "+remains);
				}
				if (remains.IsClass(EClass.HEART)) {
					remains.Owner = Owner;

				}
			}
		}
		
		//ITargetable
		public virtual void Select (Source s) {
			GUISelectors.Instance = this;

		}
		bool legal = false;
		public bool IsLegal() {return legal;}
		public void Legalize (bool l=true) {legal = l;}

		public List<Timer> timers = new List<Timer>();
	}
}
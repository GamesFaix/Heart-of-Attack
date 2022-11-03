using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public abstract class Token : ITargetable{

		protected ID id;
		public ID ID {get {return id;} }

		public override string ToString () {return id.FullName;}
		public abstract string Notes ();

		public Player Owner {
			get {return id.Owner;} 
			set {id.Owner = value;}
		}

		//templates
		protected bool isTemplate = false;
		public void BuildTemplate () {isTemplate = true;}
		public bool IsTemplate () {return isTemplate;}
		public Token Template () {
			if (IsTemplate()) {return this;}
			return TemplateFactory.Template(id.Code);
		}

		//
		protected Plane plane;
		public Plane Plane {get {return plane;} }
		
		protected Type type;
		public Type Type {get {return type;} }
		
		protected EToken onDeath = EToken.NONE;
		public EToken OnDeath {
			get {return onDeath;}
			set {onDeath = value;}
		}
		protected Body body;
		public Body Body {get {return body;} }

		//graphics
		Vector3 scale = new Vector3 (2.5f, 1, 2.5f);
		public Vector3 SpriteScale {
			get {return scale;}
			set {scale = value;}
		}
		protected void ScaleSmall () {SpriteScale = new Vector3 (1.75f, 1, 1.75f);}
		protected void ScaleMedium () {SpriteScale = new Vector3 (2f, 1, 2f);}
		protected void ScaleLarge () {SpriteScale = new Vector3 (2.5f, 1, 2.5f);}
		protected void ScaleJumbo () {SpriteScale = new Vector3 (3f, 1, 3f);}
		protected void ScaleTall () {SpriteScale = new Vector3 (3f, 1, 4.5f);}
		protected void ScaleQuad () {SpriteScale = new Vector3 (5f, 1, 5f);}
 
		TokenDisplay display = default(TokenDisplay);
		public TokenDisplay Display {
			get {return display;}
			set {display = value;}
		}
		//
		public virtual void Die (Source s, bool corpse=true, bool log=true) {
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}

			GameObject.Destroy(Display.gameObject);

			//Debug.Log(Name+" is dying");
			bool top = false;
			if (this == TurnQueue.Top) {top = true;}
			if (this is Unit) {
				TurnQueue.Remove((Unit)this);
				//Debug.Log("Removing "+Name+" from queue");
			}
			if (top) {TurnQueue.PrepareNewTop(TurnQueue.Top);} 

			TokenFactory.Remove(this);
			Cell oldCell = Body.Cell;
			Body.Exit();
			if (corpse) {CreateRemains(oldCell);}
			if (Type.Is(EClass.KING)) {Owner.Kill();}
			if (log && !Type.Is(EClass.HEART)) {
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
				if (remains.Type.Is(EClass.HEART)) {
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
		public void Legalize (bool l=true) {
			legal = l;
			Display.SetLegal(legal);
		}

		public List<Timer> timers = new List<Timer>();


	}
}
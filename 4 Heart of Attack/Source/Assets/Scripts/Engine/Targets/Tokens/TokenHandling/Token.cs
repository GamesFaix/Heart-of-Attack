using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOA {

    public abstract partial class Token : Target
    {

        public virtual TokenID ID { get; protected set; }
        public virtual Plane Plane { get; set; }
        public virtual Species OnDeath { get; protected set; }
        public virtual Body Body { get; protected set; }

        public override string ToString() { return ID.FullName; }
        public Func<string> Notes;
        public WatchList WatchList;
        public Action<Source, bool, bool> Destroy;

        public Token(Source source, Species species, string name, bool unique = false, bool template = false)
            : base()
        {
            ID = new TokenID(source, this, species, name, unique, template);
            Notes = () => { return ""; };
            Destroy = DefaultDestroy;
        }

        public virtual Player Owner
        {
            get { return ID.Owner; }
            set { ID.Owner = value; }
        }

        //templates
        public Token Template() { return (ID.Template ? this : TokenRegistry.Templates[ID.Species]); }

        //graphics
        public Vector3 SpriteScale = new Vector3(2.5f, 1, 2.5f);
        protected void ScaleSmall() { SpriteScale = new Vector3(1.75f, 1, 1.75f); }
        protected void ScaleMedium() { SpriteScale = new Vector3(2f, 1, 2f); }
        protected void ScaleLarge() { SpriteScale = new Vector3(2.5f, 1, 2.5f); }
        protected void ScaleJumbo() { SpriteScale = new Vector3(3f, 1, 3f); }
        protected void ScaleTall() { SpriteScale = new Vector3(3f, 1, 4.5f); }

        //
        public void DefaultDestroy(Source s, bool Corpse = true, bool log = true)
        {
            if (this == GUIInspector.Inspected) 
                GUIInspector.Inspected = null;
            GameObject.Destroy(Display.GO);
            TokenRegistry.Remove(this);
            Cell oldCell = Body.Cell;
            Body.Exit();
            if (Corpse) CreateRemains(oldCell); 
            if (this is King) Owner.Kill();
            if (log && !(this is Heart))
            {
                if (s.Token != default(Token)) GameLog.Out(s.Token.ToString() + " killed " + this + ".");
                else { GameLog.Out(this + " has been killed."); }
            }
        }

        protected void CreateRemains(Cell oldCell)
        {
            if (OnDeath != Species.None)
            {
                Token remains = TokenFactory.Create(new Source(this), OnDeath, oldCell, false);
                GameLog.Out(this + " left " + remains);
            }
        }

        public List<Timer> timers = new List<Timer>();
    }

}
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOA {

    public abstract class Token : Target
    {

        public virtual TokenID ID { get; protected set; }
        public virtual Plane Plane { get; set; }
        public virtual EToken OnDeath { get; protected set; }
        public Body Body { get; protected set; }

        public override string ToString() { return ID.FullName; }
        public Func<string> Notes;
        public WatchList WatchList;

        public Token() : base()
        {
            TargetClass += TargetClasses.Token;
            Notes = () => { return ""; };
        }


        public Unit Unit
        {
            get
            {
                if (this is Unit) 
                    return (Unit)this;
                return null;
            }
        }

        public virtual Player Owner
        {
            get { return ID.Owner; }
            set { ID.Owner = value; }
        }

        public bool IsUnit(out Unit unit)
        {
            unit = null;
            if (this is Unit)
            {
                unit = (Unit)this;
                return true;
            }
            return false;
        }

        //templates
        public bool IsTemplate { get; set; }
        public Token Template()
        {
            if (IsTemplate) return this;
            return TokenFactory.Template(ID.Code);
        }

        public void DisplayThumbNameTemplate(Panel p)
        {
            if (GUI.Button(p.FullBox, ""))
                if (GUIInspector.LeftClick)
                    GUIInspector.Inspected = Template();
            GUI.Box(p.Box(p.LineH), Thumbs.CodeToThumb(ID.Code), p.s);
            p.NudgeX();
            GUI.Label(p.Box(0.8f), ID.Name);
        }

        public void DisplayThumbName(Panel p)
        {
            if (GUI.Button(p.FullBox, ""))
            {
                if (GUIInspector.LeftClick)
                {
                    GUIInspector.Inspected = this;
                    AVEffect.Highlight.Play(this);
                    CameraPanner.MoveTo(this);
                }
                //				if (GUIInspector.RightClick) {GUIInspector.ToolTip("Name");}
            }
            GUI.Box(p.Box(p.LineH), Thumbs.CodeToThumb(ID.Code), p.s);
            p.NudgeX();
            p.NudgeY();
            FancyText.Highlight(p.Box(150), ID.FullName, p.s, Owner.Colors);
        }

        public void DisplayOnDeath(Panel p)
        {
            if (GUI.Button(p.FullBox, ""))
            {
                if (GUIInspector.LeftClick) { GUIInspector.Inspected = TokenFactory.Template(OnDeath); }
                if (GUIInspector.RightClick) { TipInspector.Inspect(ETip.ONDEATH); }
            }
            GUI.Box(p.Box(p.LineH), Icons.ONDEATH(), p.s);
            p.NudgeX();
            if (OnDeath == EToken.NONE)
            {
                GUI.Label(p.Box(250), "(Leaves no remains)");
            }
            else
            {
                TokenFactory.Template(OnDeath).DisplayThumbNameTemplate(new Panel(p.Box(250), p.LineH, p.s));
            }
        }

        //graphics
        Vector3 scale = new Vector3(2.5f, 1, 2.5f);
        public Vector3 SpriteScale
        {
            get { return scale; }
            set { scale = value; }
        }
        protected void ScaleSmall() { SpriteScale = new Vector3(1.75f, 1, 1.75f); }
        protected void ScaleMedium() { SpriteScale = new Vector3(2f, 1, 2f); }
        protected void ScaleLarge() { SpriteScale = new Vector3(2.5f, 1, 2.5f); }
        protected void ScaleJumbo() { SpriteScale = new Vector3(3f, 1, 3f); }
        protected void ScaleTall() { SpriteScale = new Vector3(3f, 1, 4.5f); }

        //
        public virtual void Die(Source s, bool corpse = true, bool log = true)
        {
            if (this == GUIInspector.Inspected) 
                GUIInspector.Inspected = null;
            GameObject.Destroy(Display.GO);

            bool top = false;
            if (this == TurnQueue.Top) top = true;
            if (this is Unit) TurnQueue.Remove((Unit)this);
            if (top) TurnQueue.PrepareNewTop(TurnQueue.Top);

            TokenFactory.Remove(this);
            Cell oldCell = Body.Cell;
            Body.Exit();
            if (corpse) CreateRemains(oldCell); 
            if (TargetClass[TargetClasses.King]) Owner.Kill();
            if (log && !TargetClass[TargetClasses.Heart])
            {
                if (s.Token != default(Token)) GameLog.Out(s.Token.ToString() + " killed " + this + ".");
                else { GameLog.Out(this + " has been killed."); }
            }
        }

        protected void CreateRemains(Cell oldCell)
        {
            if (OnDeath != EToken.NONE)
            {
                Token remains = default(Token);
                if (TokenFactory.Create(new Source(this), OnDeath, oldCell, out remains, false))
                    GameLog.Out(this + " left " + remains);
                if (remains.TargetClass[TargetClasses.Heart]) 
                    remains.Owner = Owner;
            }
        }

        public List<Timer> timers = new List<Timer>();
    }

}
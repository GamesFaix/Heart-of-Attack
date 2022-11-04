using UnityEngine;
using System;

namespace HOA {

    public partial class Timer : TokenComponent, IInspectable
    {
        public delegate bool TimerCondition(TurnChangeEventArgs args);

#region //Properties

        public string Name          { get; private set; }
        public Func<string> Desc    { get; private set; }
        public Source Source        { get; private set; }
        public int Modifier         { get; private set; }
        public Ability Ability      { get; private set; }
        public int Turns            { get; private set; }
        public TimerCondition Test  { get; private set; }
        public Action Activate      { get; private set; }
        
#endregion

#region //Constructors

        private Timer(Source source, Unit parent, Ability ability) : base (parent) { }

        private Timer(Source source, Unit parent, int modifier = 0, Ability ability = null) : base (parent)
        {
            Name = "Default Timer name.";
            Desc = () => { return "Default Timer description."; };
            this.Source = source;
            this.Modifier = modifier;
            this.Ability = ability;
            Turns = 0;
            Test = DefaultTest;
            Activate = () => { Debug.Log("Default Timer Activation."); };
            TurnQueue.TurnChangeEvent += TurnChangeSubscribe;
        }
 
#endregion

        public void TurnChangeSubscribe(object sender, TurnChangeEventArgs args) 
        { 
            if (Test(args)) Tick(); 
        }

#region //TimerConditions

        public bool DefaultTest(TurnChangeEventArgs args) 
        {
            Debug.Log("Timer Test set to default.");
            return false; 
        }

        public bool EveryTurnTest(TurnChangeEventArgs args) { return true; }
        
        public bool ParentTurnBeginTest(TurnChangeEventArgs args)
        {
            return (args.NewUnit == Parent ? true : false);
        }

        public bool ParentTurnEndTest(TurnChangeEventArgs args)
        {
           return (args.LastUnit == Parent ? true : false);
        }

#endregion

        public void Tick()
        {
            Turns--;
            if (Turns == 0)
            {
                Activate();
                if (Turns == 0) { Destroy(); }

            }
        }

        private void Destroy()
        {
            TurnQueue.TurnChangeEvent -= TurnChangeSubscribe;
            Parent.timers.Remove(this);
        }


        public static Texture2D Icon { get { return Icons.TIMER(); } }

        public override void Draw(Panel p) { InspectorInfo.Timer(this, p); }
    }
}
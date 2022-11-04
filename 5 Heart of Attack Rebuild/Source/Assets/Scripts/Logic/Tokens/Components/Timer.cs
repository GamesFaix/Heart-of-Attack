﻿using System;
using HOA.Ab;
using HOA.Ef;

namespace HOA.To
{
    
    public partial class Timer : TokenComponent, ISourced
    {
        #region //Properties
       
        public Source source { get; private set; }
       
        public string Name { get; private set; }
        public Description Desc { get; private set; }
        public int Modifier { get; private set; }
        public Closure Ability { get; private set; }
        public int Turns { get; private set; }
        public Predicate<TurnChangeEventArgs> Test { get; private set; }
        public Action Activate { get; private set; }

        #endregion

        #region Constructors



        private Timer(IEffect source, Token thisToken, int modifier = 0, Closure ability = null)
            : base(thisToken)
        {
            this.source = new Source(source); 
            
            Name = "[Timer]";
            Desc = Scribe.Write("[Timer description.]");
            this.Modifier = modifier;
            this.Ability = ability;
            Turns = 0;
            Test = DefaultTest;
            Activate = () => { Log.Debug("[Timer Activation.]"); };
            Session.Active.Queue.TurnChangeEvent += OnTurnChange;
        }

        private Timer(IEffect source, Token thisToken, Closure ability)
            : this(source, thisToken, 0, ability) { }

        #endregion

        public void OnTurnChange(object sender, TurnChangeEventArgs args)
        {
            if (Test(args)) Tick();
        }

        #region //TimerConditions

        public bool DefaultTest(TurnChangeEventArgs args)
        {
            Log.Debug("Timer Test set to default.");
            return false;
        }

        public bool EveryTurnTest(TurnChangeEventArgs args) { return true; }

        public bool ParentTurnBeginTest(TurnChangeEventArgs args)
        {
            return (args.NewUnit == ThisToken ? true : false);
        }

        public bool ParentTurnEndTest(TurnChangeEventArgs args)
        {
            return (args.LastUnit == ThisToken ? true : false);
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
            Session.Active.Queue.TurnChangeEvent -= OnTurnChange;
            ThisToken.timers.Remove(this);
        }

        public override string ToString() { return Name; }

    }
}
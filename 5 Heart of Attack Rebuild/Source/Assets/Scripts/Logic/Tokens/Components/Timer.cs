using System;
using HOA.Abilities;

namespace HOA.Tokens
{
    
    public partial class Timer : TokenComponent, IEffectUser
    {
        #region //Properties

        public string Name { get; private set; }
        public Description Desc { get; private set; }
        public IEffect Source { get; private set; }
        public int Modifier { get; private set; }
        public Ability Ability { get; private set; }
        public int Turns { get; private set; }
        public Predicate<TurnChangeEventArgs> Test { get; private set; }
        public Action Activate { get; private set; }

        #endregion

        #region Constructors

        private Timer(IEffect source, Token thisToken, Ability ability) : base(thisToken) { }

        private Timer(IEffect source, Token thisToken, int modifier = 0, Ability ability = null)
            : base(thisToken)
        {
            Name = "[Timer]";
            Desc = Scribe.Write("[Timer description.]");
            this.Source = source;
            this.Modifier = modifier;
            this.Ability = ability;
            Turns = 0;
            Test = DefaultTest;
            Activate = () => { Debug.Log("[Timer Activation.]"); };
            Session.Active.Queue.TurnChangeEvent += OnTurnChange;
        }

        #endregion

        public void OnTurnChange(object sender, TurnChangeEventArgs args)
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

        public Ability ToAbility() { return (Source as Effect).User.ToAbility(); }
        public IAbilityUser ToAbilityUser() { return ToAbility().User; }
        public ITokenCreator ToTokenCreator() { return ToAbilityUser().ToTokenCreator(); }
    }
}
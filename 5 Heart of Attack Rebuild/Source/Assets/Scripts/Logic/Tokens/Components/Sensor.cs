using System;
using HOA.Abilities;

namespace HOA.Tokens
{

    public partial class Sensor : TokenComponent, ISourced
    {
        #region Properties
        
        public Source source { get; private set; }
        
        /// <summary> Name of sensor </summary>
        public string Name { get; private set; }
        /// <summary> Generates description </summary>
        public Description Desc { get; private set; }
        /// <summary> Cells that Sensor is sensitive to </summary>
        public Set<Cell> Subscriptions { get; private set; }
        /// <summary> Tokens that pass thru the filter must 
        /// stop on subscribed cells (no thru movement)</summary>
        public Predicate<IEntity> Trap { get; private set; }
        /// <summary> Tokens that pass thru the filter will trigger sensor effects </summary>
        public Predicate<IEntity> Trigger { get; private set; }

        /// <summary> Effects done to a cell when ThisToken enters it. </summary>
        public Action<Cell> OnThisEnter { get; private set; }
        /// <summary> Effects done to a cell when ThisToken exits it. </summary>
        public Action<Cell> OnThisExit { get; private set; }
        /// <summary> Effects done to a token when it enters a subscribed cell. </summary>
        public Action<Token> OnOtherEnter { get; private set; }
        /// <summary> Effects done to a token when it exits a subscribed cell. </summary>
        public Action<Token> OnOtherExit { get; private set; }

        #endregion

        #region Constructors

        private Sensor(Token thisToken)
            : base(thisToken)
        {
            this.source = new Source(thisToken); 

            Name = "[Sensor]";
            Desc = Scribe.Write("[Sensor description]");
            Subscriptions = new Set<Cell>(1);
            Trigger = Filter.False;
            Trap = Filter.False;
            OnThisEnter = (c) =>
            {
                Subscribe(c);
                foreach (Token t in c.occupants)
                    OnOtherEnter(t);
            };
            OnThisExit = (c) =>
            {
                foreach (Token t in c.occupants)
                    OnOtherExit(t);
                Unsubscribe(c);
            };
            OnOtherEnter = (t) => { };
            OnOtherExit = (t) => { };
        }

        private Sensor(Token thisToken, string name)
            : this(thisToken)
        { Name = name; }

        private Sensor(Token thisToken, string name, Predicate<IEntity> trigger)
            : this(thisToken, name)
        { Trigger = trigger; }

        private Sensor(Token thisToken, string name, Predicate<IEntity> trigger, Predicate<IEntity> trap)
            : this(thisToken, name, trigger)
        { Trap = trap; }

        #endregion

        public override string ToString() { return Name; }

        public void Subscribe(Cell c)
        {
            Subscriptions.Add(c);
            c.OccupationEvent += this.OccupationSubscribe;
            c.Subscribers.Add(this);
        }

        public void Unsubscribe(Cell c)
        {
            Subscriptions.Remove(c);
            c.OccupationEvent -= this.OccupationSubscribe;
            c.Subscribers.Remove(this);
        }

        void UnsubscribeAll()
        {
            for (int i = Subscriptions.Count; i >= 0; i--)
            {
                Cell c = Subscriptions[i] as Cell;
                c.OccupationEvent -= this.OccupationSubscribe;
                c.Subscribers.Remove(this);
            }
            Subscriptions = new Set<Cell>();
        }

        public void OccupationSubscribe(object sender, OccupationEventArgs args)
        {
            if (Subscriptions.Contains(args.Cell)
                && !Trigger.AnyTrue(args.Token))
            {
                if (args.Enter)
                    OnOtherEnter(args.Token);
                else
                    OnOtherExit(args.Token);
            }
        }


    }

}
using System;

namespace HOA.Tokens
{

    public class Sensor : TokenComponent
    {
        /// <summary>
        /// Name of sensor
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Generates description
        /// </summary>
        public Func<string> Desc { get; private set; }
        /// <summary>
        /// Cells that Sensor is sensitive to
        /// </summary>
        public CellSet Subscriptions { get; private set; }
        /// <summary>
        /// Tokens that pass thru the filter must stop on subscribed cells (no thru movement)
        /// </summary>
        public EntityFilter Trap { get; private set; }
        /// <summary>
        /// Tokens that pass thru the filter will trigger sensor effects
        /// </summary>
        public EntityFilter Trigger { get; private set; }

        /// <summary>
        /// Effects done to a cell when ThisToken enters it.
        /// </summary>
        public Action<Cell> OnThisEnter { get; private set; }
        /// <summary>
        /// Effects done to a cell when ThisToken exits it.
        /// </summary>
        public Action<Cell> OnThisExit { get; private set; }
        /// <summary>
        /// Effects done to a token when it enters a subscribed cell.
        /// </summary>
        public Action<Token> OnOtherEnter { get; private set; }
        /// <summary>
        /// Effects done to a token when it exits a subscribed cell.
        /// </summary>
        public Action<Token> OnOtherExit { get; private set; }

        private Sensor(Token thisToken)
            : base(thisToken)
        {
            Name = "[Sensor]";
            Desc = () => { return "[Sensor description]"; };
            Subscriptions = new CellSet(1);
            Trigger = EntityFilter.None;
            Trap = EntityFilter.None;
            OnThisEnter = (c) =>
            {
                Subscribe(c);
                foreach (Token t in c.Occupants)
                    OnOtherEnter(t);
            };
            OnThisExit = (c) =>
            {
                foreach (Token t in c.Occupants)
                    OnOtherExit(t);
                Unsubscribe(c);
            };
            OnOtherEnter = (t) => { };
            OnOtherExit = (t) => { };
        }

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
                Cell c = Subscriptions[i];
                c.OccupationEvent -= this.OccupationSubscribe;
                c.Subscribers.Remove(this);
            }
            Subscriptions = new CellSet();
        }

        public void OccupationSubscribe(object sender, OccupationEventArgs args)
        {
            if (Subscriptions.Contains(args.Cell)
                && Trigger.Test(args.Token))
            {
                if (args.Enter)
                    OnOtherEnter(args.Token);
                else
                    OnOtherExit(args.Token);
            }
        }

    }

}
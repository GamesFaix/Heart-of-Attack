using System;
using System.Collections;
using System.Collections.Generic;

namespace HOA.Ab.Aim
{

    public partial class Plan : IEnumerable<Stage>, ISourced
    {
        public Source source { get; private set; }
        public Ability ability { get { return source.Last<Ability>(); } }
        List<Stage> stages;
        public int Count { get { return stages.Count; } }

        public Stage last { get { return stages[Count - 1]; } }

        public Plan(Ability ability, params Stage[] stages)
        {
            source = new Source(ability);
            this.stages = new List<Stage>(stages);
        }

        public Stage this[int i] { get { return stages[i]; } }
        public int IndexOf(Stage stage) { return stages.IndexOf(stage); }

        public IEnumerator<Stage> GetEnumerator() { return stages.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return stages.GetEnumerator(); }

        public void Add(Stage stage) { stages.Add(stage); }
        public bool Remove(Stage stage) { return stages.Remove(stage); }
        public void Insert(int i, Stage stage) { stages.Insert(i, stage); }

        public static Plan operator +(Plan plan, Stage stage)
        {
            plan.Add(stage);
            return plan;
        }

        public static Plan operator -(Plan plan, Stage stage)
        {
            plan.Remove(stage);
            return plan;
        }


	}
}
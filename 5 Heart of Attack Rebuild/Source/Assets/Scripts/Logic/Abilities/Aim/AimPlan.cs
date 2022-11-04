using System;
using System.Collections;
using System.Collections.Generic;

namespace HOA.Ab
{

    public partial class AimPlan : IEnumerable<AimStage>, ISourced
    {
        public Source source { get; private set; }
        public Ability ability { get { return source.Last<Ability>(); } }
        List<AimStage> stages;
        public int Count { get { return stages.Count; } }

        public AimStage last { get { return stages[Count - 1]; } }

        public AimPlan(Ability ability, params AimStage[] stages)
        {
            source = new Source(ability);
            this.stages = new List<AimStage>(stages);
        }

        public AimStage this[int i] { get { return stages[i]; } }
        public int IndexOf(AimStage stage) { return stages.IndexOf(stage); }

        public IEnumerator<AimStage> GetEnumerator() { return stages.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return stages.GetEnumerator(); }

        public void Add(AimStage stage) { stages.Add(stage); }
        public bool Remove(AimStage stage) { return stages.Remove(stage); }
        public void Insert(int i, AimStage stage) { stages.Insert(i, stage); }

        public static AimPlan operator +(AimPlan plan, AimStage stage)
        {
            plan.Add(stage);
            return plan;
        }

        public static AimPlan operator -(AimPlan plan, AimStage stage)
        {
            plan.Remove(stage);
            return plan;
        }


	}
}
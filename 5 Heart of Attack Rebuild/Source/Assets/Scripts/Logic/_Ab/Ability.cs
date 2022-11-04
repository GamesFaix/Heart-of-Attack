using System;
using System.Collections;
using System.Collections.Generic;
using HOA.Ab.Aim;

namespace HOA.Ab
{
    
    public partial class Ability : ICloseable<Args>
    {
        
        public string name { get; set; }
        public Rank rank { get; private set; }

        public Plan Aims { get; private set; }
        
        public UseTest Usable { get; private set; }

        private Action PostEffects;
        private Action<Args, NestedList<IEntity>> MainEffects;

        public Adjustment Update;
        
        private Ability(string name, Rank rank)
        {
            this.name = name;
            this.rank = rank;
            
            Usable += UseTests.UserInQueue;
            Usable += UseTests.UserIsTop;
            Usable += UseTests.Unused;
            Usable += UseTests.Affordable;
            Usable += UseTests.AlreadyProcessing;
            
            Aims = new Plan(this);

            MainEffects = (args, tar) => { };
            PostEffects = () => { };

            Update = Adjustments.Standard;
        }

        public void Execute(Closure source, NestedList<IEntity> targets)
        {
            MainEffects(source.args, targets);
            PostEffects();
        }

        public override string ToString() { return name; }
    }
}
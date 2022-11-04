using System;
using System.Collections;
using System.Collections.Generic;


namespace HOA.Ab
{
    
    public partial class Ability : ICloseable<AbilityArgs>
    {
        
        public string name { get; set; }
        public Rank rank { get; private set; }

        public AimPlan Aims { get; private set; }
        
        public UsabilityTest Usable { get; private set; }

        private Action PostEffects;
        private Action<AbilityArgs, NestedList<IEntity>> MainEffects;

        public Adjustment Update;
        
        private Ability(string name, Rank rank)
        {
            this.name = name;
            this.rank = rank;
            
            Usable += UsabilityTests.UserInQueue;
            Usable += UsabilityTests.UserIsTop;
            Usable += UsabilityTests.Unused;
            Usable += UsabilityTests.Affordable;
            Usable += UsabilityTests.AlreadyProcessing;
            
            Aims = new AimPlan(this);

            MainEffects = (args, tar) => { };
            PostEffects = () => { };

            Update = Adjustments.None;
        }

        public void Execute(Closure source, NestedList<IEntity> targets)
        {
            MainEffects(source.args, targets);
            PostEffects();
        }

        public override string ToString() { return name; }
    }
}
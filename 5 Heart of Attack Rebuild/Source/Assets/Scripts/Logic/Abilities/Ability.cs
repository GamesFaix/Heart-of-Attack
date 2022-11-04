using System;
using System.Collections;
using System.Collections.Generic;

namespace HOA.Abilities
{
    
    public partial class Ability : ICloseable<AbilityArgs>
    {
        
        public string name { get; set; }
        public AbilityRank rank { get; private set; }

        public AimPlan Aims { get; private set; }
        
        public UseTest Usable { get; private set; }

        private Action PostEffects;
        private Action<AbilityArgs, NestedList<IEntity>> MainEffects;

        public Adjustment Update;
        
        private Ability(string name, AbilityRank rank)
        {
            this.name = name;
            this.rank = rank;
            
            Usable += UseTests.UserInQueue;
            Usable += UseTests.UserIsTop;
            Usable += UseTests.Unused;
            Usable += UseTests.Affordable;
            Usable += UseTests.AlreadyProcessing;
            
            Aims = new AimPlan(this);

            MainEffects = (args, tar) => { };
            PostEffects = () => { };

            Update = Adjustments.Standard;
        }

        public void Execute(AbilityClosure source, NestedList<IEntity> targets)
        {
            MainEffects(source.args, targets);
            PostEffects();
        }

        public override string ToString() { return name; }
    }
}
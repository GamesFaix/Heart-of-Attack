using System;
using HOA.Collections;

namespace HOA.Abilities
{
    
    public partial class Ability
    {
        
        public string name { get; set; }
        public AbilityRank rank { get; private set; }

        public AimPlan Aims { get; private set; }
        
        public AbilityCondition Usable { get; private set; }

        private Action PostEffects;
        private Action<AbilityArgs, NestedList<IEntity>> MainEffects;

        public Adjustment Update;
        
        private Ability(string name, AbilityRank rank)
        {
            this.name = name;
            this.rank = rank;
            
            Usable += AbilityConditions.UserInQueue;
            Usable += AbilityConditions.UserIsTop;
            Usable += AbilityConditions.Unused;
            Usable += AbilityConditions.Affordable;
            Usable += AbilityConditions.AlreadyProcessing;
            
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
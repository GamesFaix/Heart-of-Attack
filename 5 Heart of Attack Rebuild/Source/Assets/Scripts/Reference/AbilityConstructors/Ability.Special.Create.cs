using HOA.Tokens;
using System;

namespace HOA.Abilities
{
    
    public partial class Ability
    {

        public static Ability Recycle(Unit parent, Price price)
        {
            return TransformOther(parent,
                Scribe.Write("Recycle Recyclops"), 
                Abilities.Rank.Create,
                price,
                Species.Recyclops,
                Filter.Corpse);
        }


        public static Ability Arise(Unit parent)
        {
            Ability a = new Ability(parent, new AbilityArgs("Arise", Rank.Special, new Price(2, 0), Species.Conflagragon));
            a.desc = Scribe.Write("Transform {0} into a {1}. \n(New {1} starts with {0}'s health.)", a.sourceUnit, a.args.species);
            a.Aims += AimStage.Self(a.Aims);
            a.MainEffects = t =>
            {   
                int hp = a.sourceUnit.Health;
                EffectConstructor e = (src, ar) => 
                    Effect.SetStat(src, new EffectArgs(ar.unit, hp, Stats.Health));
                EffectQueue.Add(Effect.DestroyUnit(a, new EffectArgs(a.sourceUnit)));
                EffectQueue.Add(Effect.Create(a, new EffectArgs(a.sourceUnit.Cell, a.args.species, e)));
            };

            a.Usable += a.AirClear;
            return a;
        }
        
       
           /* 
        public static Ability CreateAren(Unit parent)
        {
            Ability a = new Ability(parent, "Create Arena", 5, new Price(1, 1), Species.Arena);
            a.Desc = () => { return "Create Arena in Target cell."; };
            a.Aims.Add(Aim.CreateAren());
            a.MainEffects = Targets =>
                EffectQueue.Add(Effect.Create(new Source(a.Parent), (Cell)Targets[0], Species.Arena));
           
            return a;
        }*/
        
       
    
     }
    
}
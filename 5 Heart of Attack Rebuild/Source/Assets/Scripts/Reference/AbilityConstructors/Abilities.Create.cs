using HOA.Tokens;
using System;

namespace HOA.Abilities
{
    
    public partial class Ability
    {
        public static Ability Animate(Unit parent)
        {
            return Transmute(parent, 
                Scribe.Write("Animate Metaterrainean"), 
                new Price(1,2), 
                Species.Metaterrainean, 
                Filter.DestNotCorpse);
        }

        public static Ability Recycle(Unit parent, Price price)
        {
            return Transmute(parent,
                Scribe.Write("Recycle Recyclops"),
                price,
                Species.Recyclops,
                Filter.Corpse);
        }


        public static Ability Arise(Unit parent)
        {
            Ability a = new Ability(parent, "Arise", Rank.Special, new Price(2, 0), new AbilityArgs(Species.Conflagragon));
            a.Desc = Scribe.Write("Transform {0} into a {1}. \n(New {1} starts with {0}'s health.)", a.sourceUnit, a.Args.species);
            a.Aims += AimStage.Self(a.Aims);
            a.MainEffects = Targets =>
            {   
                int hp = a.sourceUnit.Health;
                EffectConstructor e = (src, ar) => 
                { 
                    return Effect.SetStat(src, new EffectArgs(ar.unit, hp, Stats.Health));
                };
                EffectQueue.Add(Effect.DestroyUnit(a, new EffectArgs(a.sourceUnit)));
                EffectQueue.Add(Effect.Create(a, new EffectArgs(a.sourceUnit.Cell, a.Args.species, e)));
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
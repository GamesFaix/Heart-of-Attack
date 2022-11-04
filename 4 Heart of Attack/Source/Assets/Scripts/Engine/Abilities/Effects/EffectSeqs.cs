using UnityEngine;
using System.Collections.Generic;

namespace HOA.Effects {
 

	public class Detonate : EffectSeq {
        public Detonate (Source s, Token t) : base(s,t){
			Name = "Detonate";
			list = new List<EffectGroup>();
			EffectGroup group = new EffectGroup();
			group.Add(Effect.Detonate2 (new Source(Source.Token, this), (Token)Target));
			list.Add(group);
		}
	}

    public class Explosion : EffectSeq
    {
        public Explosion(Source s, Cell c, int n, bool selfImmune = false)
            : base(s, c, n)
        {
            Name = "Explosion Sequence";

            list = new List<EffectGroup>();

            TargetGroup affected = new TargetGroup();
            TargetGroup thisRad = new TargetGroup(Target);
            TargetGroup nextRad = new TargetGroup();

            int currentDmg = Modifier;

            int i = 0;
            while (currentDmg > 0 && i <= 2)
            {
                EffectGroup group = new EffectGroup();
                for (int j = 0; j < thisRad.Count; j++)
                {
                    Cell next;
                    if (thisRad[j].IsCell(out next) && !affected.Contains(next))
                    {
                        if (next.Occupants.Count > 0)
                        {
                            group.Add(Effect.ExplosionIndividual(
                                new Source(Source.Token, this), next, currentDmg, selfImmune));
                        }
                        else
                        {
                            group.Add(Effect.ExplosionDummy(new Source(Source.Token, this), next));
                        }
                        foreach (Cell cell in next.Neighbors()) nextRad.Add(cell);
                        affected.Add(next);
                    }
                }
                thisRad = nextRad;
                nextRad = new TargetGroup();
                currentDmg = (int)Mathf.Floor(currentDmg * 0.5f);
                list.Add(group);
                i++;
            }
        }
    }
}
using HOA.Abilities;
using HOA.Tokens;
using Pred = System.Predicate<HOA.IEntity>;

namespace HOA
{
    public delegate Pred FilterBuilder(AbilityClosure a);

    public static class FilterBuilders
    {
        public static readonly FilterBuilder
           Friendly,
           Enemy,
           Identity,
           ExceptSelf,
           False,
           Cell,
           Token,
           Unit,
           Ob,
           Terrain,
           King,
           Heart,
           Destructible,
           Corpse,
           DestNotCorpse,
           Trample,
           UnitDest,
           Legal;

        static FilterBuilders()
        {
            Friendly = (a) => { return Filter.Owner(a.sourceUnit.owner, true); };
            Enemy = (a) => { return Filter.Owner(a.sourceUnit.owner, false); };
            Identity = (a) => { return Filter.identity(a.sourceUnit, true); };
            ExceptSelf = (a) => { return Filter.identity(a.sourceUnit, false); };
            Cell = (a) => { return Filter.Cell; };
            Token = (a) => { return Filter.Token; };
            Unit = (a) => { return Filter.Unit; };
            Ob = (a) => { return Filter.Ob; };
            Terrain = (a) => { return Filter.Terrain; };
            King = (a) => { return Filter.King; };
            Heart = (a) => { return Filter.Heart; };
            Destructible = (a) => { return Filter.Destructible; };
            Corpse = (a) => { return Filter.Corpse; };
            DestNotCorpse = (a) => { return Filter.DestNotCorpse; };
            Trample = (a) => { return Filter.Trample; };
            UnitDest = (a) => { return Filter.UnitDest; };
            Legal = (a) => { return Filter.Legal; };
        }
    }

}
using System;

namespace HOA.Fargo
{
	

    public enum FO : byte
    {
        None = 0,
        ExcludeSelf,
        Secondary,
        Toggle,
        Toggle1,
        Toggle2

	}

    public enum FN : byte
    {
        None = 0,
        Amount,
        Boost,
        Boost1,
        Damage,
        Damage1,
        Damage2,
        Decay,
        Decay1,
        MaxCells,
        Stun,
        Defense,
        Energy,
        Focus,
        Health,
        Initiative,
        Stat,
        Stat1,
        Stat2
    }
    public enum FS : byte
    {
        None = 0,
        Amount,
        Boost,
        Damage,
        Damage1,
        Damage2,
        Price,
        Range0,
        Range1,
        Range2,
        Select0,
        Select1,
        Select2,
        Stun,
        Defense,
        Energy,
        Focus,
        Health,
        Initiative
    }

    public enum FX : byte
    {

        None = 0,
        Name,
        Stat,
        Stat1,
        Stat2
    }

    public enum FT : byte
    {
        None = 0,
        Start,
        Destination,
        Location,
        Mover,
        Damaged,
        User,

        Cell,
        Cell1,
        Cell2,
        Cell3,
        Cell4,
        Cell5,
        Token,
        Token1,
        Token2,
        Token3,
        Token4,
        Token5,
        Unit,
        Unit1,
        Unit2,
        Unit3,
        Unit4,
        Unit5
    }

    public enum FF : byte
    {
        None = 0,
        Filter0,
        Filter1,
        Filter2
    }

    public enum FE : byte
    {
        None = 0,
        Birth,
        Death,
        Effect,
        Effect1,
        Effect2
    }
}
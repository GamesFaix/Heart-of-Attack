using System;

namespace HOA.Abilities
{

    public interface IEffect
    {

        Action Process { get; }
        string ToString();
    }
}
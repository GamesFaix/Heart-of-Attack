using System;

namespace HOA.Effects
{

    public interface IEffect
    {

        Action Process { get; }
        string ToString();
    }
}
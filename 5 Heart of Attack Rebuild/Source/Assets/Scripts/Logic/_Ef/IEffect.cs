using System;

namespace HOA.Ef
{

    public interface IEffect
    {

        Action Process { get; }
        string ToString();
    }
}
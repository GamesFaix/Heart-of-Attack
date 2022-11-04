using System;

namespace HOA.Ab
{

    public interface IEffect
    {

        Action Process { get; }
        string ToString();
    }
}
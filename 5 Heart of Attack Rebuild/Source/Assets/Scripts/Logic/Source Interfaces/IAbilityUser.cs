using System;

namespace HOA.Abilities
{
    public interface IAbilityUser
    {
        string ToString();

        Token ToToken();
        Tokens.ITokenCreator ToTokenCreator();
    }
}
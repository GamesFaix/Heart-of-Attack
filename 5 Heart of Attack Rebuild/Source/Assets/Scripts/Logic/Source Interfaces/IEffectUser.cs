namespace HOA.Abilities
{

    public interface IEffectUser
    {
        string ToString();

        Ability ToAbility();
        IAbilityUser ToAbilityUser();
        Tokens.ITokenCreator ToTokenCreator();

    }

}
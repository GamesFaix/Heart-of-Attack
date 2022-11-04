namespace HOA.Reference
{
    public static class Main
    {
        public static void Load()
        {
            Factions.Load();
            Players.Load();
            HOA.Tokens.Arsenal.Load();
            Tokens.Load();
            //GUI.AbilityRequester.Load();
            Abilities.AbilityProcessor.Load();
            GUI.TargetSelector.Load();
        }
    }

}
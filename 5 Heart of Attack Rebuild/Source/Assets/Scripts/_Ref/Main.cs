namespace HOA.Content
{
    public static class Main
    {
        public static void Load()
        {
            Content.Factions.Load();
            Content.Players.Load();
            HOA.Tokens.Arsenal.Load();
            Content.Abilities.Load(); 
            Content.Tokens.Load();
            //GUI.AbilityRequester.Load();
            HOA.Abilities.AbilityProcessor.Load();
            GUI.TargetSelector.Load();
        }
    }

}
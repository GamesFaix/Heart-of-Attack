namespace HOA.Ref
{
    public static class Main
    {
        public static void Load()
        {
            Factions.Load();
            Players.Load();
            HOA.To.Arsenal.Load();
            Abilities.Load(); 
            Tokens.Load();
            //GUI.AbilityRequester.Load();
            Ab.Processor.Load();
            GUI.TargetSelector.Load();
        }
    }

}
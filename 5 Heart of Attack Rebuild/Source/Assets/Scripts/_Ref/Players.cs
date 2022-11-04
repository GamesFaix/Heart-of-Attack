using System.Collections.Generic;

namespace HOA.Content 
{
	

    public static class Players
    {
        public static List<string> defaultNames { get; private set; }

        public static int max { get; private set; }

        public static void Load()
        {
            max = 8;

            defaultNames = new List<string>(40) { 
            "Dingus", "Cromdor", "Nixon's Ghost", 
            "Mediocrates", "Butters", "The Almighty Atheismo", 
            "B4PH0M3T", "Heabert Jiebersen", "Dr. Noobenstein",
            "Darth Tater", "Bro Chi Minh", "Nematode",
            "MISSINGNO", "Notch", "Awnawld Pawlmawr",
            "Sensei Wut", "Geff Joldblum", "Z. Rex",
            "Samsquanch", "=^_^=", "Molloch, who", "Gorgon Freeman",
            "Jebediah Kerman", "Kebediah German", "Sheogorath",
            "Good News, Everyone!", "#72904835", "Vic Torus", 
            "Yesferatu", "Vespene Geyser Depleted", "Those About to Rock",
            "Awesome-O"};
        }
	}
}
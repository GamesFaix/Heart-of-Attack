using UnityEngine; 

namespace HOA { 

    public partial class Alias 
    {
        public static Alias Arena(Source source, bool template = false)
        {
            Alias a = new Alias(source);
            a.Body = new Body(a);
            a.ScaleMedium();
            return a;
        }
    }
}

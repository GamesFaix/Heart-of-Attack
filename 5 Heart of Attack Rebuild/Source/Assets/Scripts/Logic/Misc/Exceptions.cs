using System;

namespace HOA
{
    /// <summary>
    /// To be thrown when a property refers to the only
    /// item in a collection, but the collection contains
    /// more than one item fitting the property.
    /// </summary>
    public class AmbiguousReferenceException : ApplicationException
    {
        public AmbiguousReferenceException(string message = "")
            : base("The property you are trying to access may refer "
                + "to more than one item in the underlying collection." 
               + "\n" + message)
        { }

    }


}
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

    /// <summary>
    /// To be thrown by classes implementing ISourceRestricted 
    /// when they are instantiated from an invalid type.
    /// </summary>
    public class InvalidSourceException : ApplicationException
    {
        public InvalidSourceException(string message = "")
            : base("The type you are trying to instantiate must be "
            + "cannot be instantiated from the current type."
            + "\n" + message)
        { }
    }


}
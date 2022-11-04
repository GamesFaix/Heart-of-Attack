using System;

namespace HOA 
{
	public interface ISourceRestricted 
    {
        Type[] validSources { get; }
        bool IsValidSource(object obj);
	}
}
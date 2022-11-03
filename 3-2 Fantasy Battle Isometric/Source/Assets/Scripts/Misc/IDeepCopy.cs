namespace HOA { 

	public interface IDeepCopy<T> {
		T DeepCopy ();
	}

	public interface IDeepCopyToken<T> {
		T DeepCopy(Token parent);
	}

	public interface IDeepCopyUnit<T> {
		T DeepCopy(Unit parent);
	}
}

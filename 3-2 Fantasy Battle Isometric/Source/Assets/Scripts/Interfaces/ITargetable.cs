
namespace HOA {

	public interface ITarget {

		void Select (Source s);
		void Legalize (bool l=true);
		bool IsLegal ();
	}
}
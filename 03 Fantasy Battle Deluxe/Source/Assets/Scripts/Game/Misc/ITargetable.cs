
namespace HOA {

	public interface ITargetable {

		void Select (Source s);
		void Legalize (bool l=true);
		bool IsLegal ();
	}
}
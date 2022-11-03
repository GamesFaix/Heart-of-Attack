using System.Collections.Generic;

namespace HOA {

	public interface ISource {

		ISource Source();
		List<ISource> SourceList();

	}

}
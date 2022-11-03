namespace HOA { 

	public partial class Aim {

		public static Aim Self () {
			Aim a = new Aim();
			a.Trajectory = ETraj.SELF;

			return a;
		}

		public static Aim Free (Special special, EPurp purpose) {
			Aim a = new Aim();
			a.Trajectory = ETraj.FREE;
			a.Special = special;
			a.Purpose = purpose;
			a.Targets = a.FindFree;
			return a;
		}

		public static Aim Radial (Special special) {
			Aim a = new Aim();
			a.Trajectory = ETraj.RADIAL;
			a.Special = special;
			a.Purpose = EPurp.ATTACK;
			a.Targets = a.FindRadial;
			return a;
		}

		public static Aim AttackArc (Special special, int minRange, int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.ARC;
			a.Special = special;
			a.Purpose = EPurp.ATTACK;
			a.Range = range;
			a.MinRange = minRange;
			a.Targets = a.FindArc;
			return a;
		}

		public static Aim AttackLine (Special special, int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.LINE;
			a.Special = special;
			a.Purpose = EPurp.ATTACK;
			a.Range = range;
			a.Targets = a.FindLine;
			return a;
		}

		public static Aim AttackNeighbor (Special special) {
			Aim a = new Aim();
			a.Trajectory = ETraj.NEIGHBOR;
			a.Special = special;
			a.Purpose = EPurp.ATTACK;
			a.Targets = a.FindNeighbor;
			return a;
		}

		public static Aim AttackPath (Special special, int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.PATH;
			a.Special = special;
			a.Purpose = EPurp.ATTACK;
			a.Range = range;
			a.Targets = a.FindPath;
			return a;
		}

		public static Aim CreateArc (int minRange, int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.ARC;
			a.Special = Special.Cell;
			a.Purpose = EPurp.CREATE;
			a.Range = range;
			a.MinRange = minRange;
			a.Targets = a.FindArc;
			return a;
		}

		public static Aim CreateNeighbor () {
			Aim a = new Aim();
			a.Trajectory = ETraj.NEIGHBOR;
			a.Special = Special.Cell;
			a.Purpose = EPurp.CREATE;
			a.Targets = a.FindNeighbor;
			return a;
		}

		public static Aim MoveArc (int minRange, int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.ARC;
			a.Special = Special.Cell;
			a.Purpose = EPurp.MOVE;
			a.Range = range;
			a.MinRange = minRange;
			a.Targets = a.FindArc;
			return a;
		}

		public static Aim MoveLine (int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.LINE;
			a.Special = Special.Cell;
			a.Purpose = EPurp.MOVE;
			a.Range = range;
			a.Targets = a.FindLine;
			return a;
		}

		public static Aim MoveNeighbor () {
			Aim a = new Aim();
			a.Trajectory = ETraj.NEIGHBOR;
			a.Special = Special.Cell;
			a.Purpose = EPurp.MOVE;
			a.Targets = a.FindNeighbor;
			return a;
		}

		public static Aim MovePath (int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.PATH;
			a.Special = Special.Cell;
			a.Purpose = EPurp.MOVE;
			a.Range = range;
			a.Targets = a.FindPath;
			return a;
		}

		public static Aim CreateAren () {
			Aim a = new Aim();
			a.Trajectory = ETraj.NEIGHBOR;
			a.Special = Special.Cell;
			a.Purpose = EPurp.CREATE;
			a.Targets = a.FindCreateAren;
			return a;
		}

	}
}

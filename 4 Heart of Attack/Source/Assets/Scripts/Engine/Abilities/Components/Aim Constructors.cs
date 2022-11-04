namespace HOA { 

	public partial class Aim {

		public static Aim Self () {
			Aim a = new Aim();
			a.Trajectory = ETraj.SELF;

			return a;
		}

		public static Aim Free (TargetFilter filter, EPurp purpose) {
			Aim a = new Aim();
			a.Trajectory = ETraj.FREE;
			a.Filter = filter;
			a.Purpose = purpose;
			a.Targets = a.FindFree;
			return a;
		}

        public static Aim Radial(TargetFilter filter)
        {
			Aim a = new Aim();
			a.Trajectory = ETraj.RADIAL;
            a.Filter = filter;
			a.Purpose = EPurp.ATTACK;
			a.Targets = a.FindRadial;
			return a;
		}

        public static Aim AttackArc(TargetFilter filter, int minRange, int range)
        {
			Aim a = new Aim();
			a.Trajectory = ETraj.ARC;
            a.Filter = filter;
			a.Purpose = EPurp.ATTACK;
			a.Range = range;
			a.MinRange = minRange;
			a.Targets = a.FindArc;
			return a;
		}

        public static Aim AttackLine(TargetFilter filter, int range)
        {
			Aim a = new Aim();
			a.Trajectory = ETraj.LINE;
            a.Filter = filter;
			a.Purpose = EPurp.ATTACK;
			a.Range = range;
			a.Targets = a.FindLine;
			return a;
		}

        public static Aim AttackNeighbor(TargetFilter filter)
        {
			Aim a = new Aim();
			a.Trajectory = ETraj.NEIGHBOR;
            a.Filter = filter;
			a.Purpose = EPurp.ATTACK;
			a.Targets = a.FindNeighbor;
			return a;
		}

        public static Aim AttackPath(TargetFilter filter, int range)
        {
			Aim a = new Aim();
			a.Trajectory = ETraj.PATH;
            a.Filter = filter;
			a.Purpose = EPurp.ATTACK;
			a.Range = range;
			a.Targets = a.FindPath;
			return a;
		}

		public static Aim CreateArc (int minRange, int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.ARC;
			a.Filter = TargetFilter.Cell;
			a.Purpose = EPurp.CREATE;
			a.Range = range;
			a.MinRange = minRange;
			a.Targets = a.FindArc;
			return a;
		}

		public static Aim CreateNeighbor () {
			Aim a = new Aim();
			a.Trajectory = ETraj.NEIGHBOR;
            a.Filter = TargetFilter.Cell;
			a.Purpose = EPurp.CREATE;
			a.Targets = a.FindNeighbor;
			return a;
		}

		public static Aim MoveArc (int minRange, int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.ARC;
            a.Filter = TargetFilter.Cell;
			a.Purpose = EPurp.MOVE;
			a.Range = range;
			a.MinRange = minRange;
			a.Targets = a.FindArc;
			return a;
		}

		public static Aim MoveLine (int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.LINE;
            a.Filter = TargetFilter.Cell;
			a.Purpose = EPurp.MOVE;
			a.Range = range;
			a.Targets = a.FindLine;
			return a;
		}

		public static Aim MoveNeighbor () {
			Aim a = new Aim();
			a.Trajectory = ETraj.NEIGHBOR;
            a.Filter = TargetFilter.Cell;
			a.Purpose = EPurp.MOVE;
			a.Targets = a.FindNeighbor;
			return a;
		}

		public static Aim MovePath (int range) {
			Aim a = new Aim();
			a.Trajectory = ETraj.PATH;
            a.Filter = TargetFilter.Cell;
			a.Purpose = EPurp.MOVE;
			a.Range = range;
			a.Targets = a.FindPath;
			return a;
		}

		public static Aim CreateAren () {
			Aim a = new Aim();
			a.Trajectory = ETraj.NEIGHBOR;
            a.Filter = TargetFilter.Cell;
			a.Purpose = EPurp.CREATE;
			a.Targets = a.FindCreateAren;
			return a;
		}

	}
}

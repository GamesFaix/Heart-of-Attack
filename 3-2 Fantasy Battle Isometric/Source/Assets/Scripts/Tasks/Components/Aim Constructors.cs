namespace HOA { 

	public partial class Aim {

		private Aim () {
			trajectory = Trajectory.Self;
			Find = null;
			Filter = Filters.None;
			range = 0;
			minRange = 0;
			Extend = None;
			recursiveTarget = false;
			optional = false;
		}

		public static Aim Self () {return new Aim();}

		public static Aim Free (Filter filter) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Free;
			a.Find = a.FindFree;
			a.Filter = filter;
			return a;
		}

		public static Aim Radial (Filter filter) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Radial;
			a.Find = a.FindRadial;
			a.Filter = filter;
			return a;
		}

		public static Aim AttackArc (Filter filter, int minRange, int range) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Arc;
			a.Filter = filter;
			a.range = range;
			a.minRange = minRange;
			a.Find = a.FindArc;
			return a;
		}

		public static Aim AttackLine (Filter filter, int range) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Line;
			a.Filter = filter;
			a.range = range;
			a.Find = a.FindLine;
			return a;
		}

		public static Aim AttackNeighbor (Filter filter) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Neighbor;
			a.Filter = filter;
			a.Find = a.FindNeighbor;
			return a;
		}

		public static Aim AttackPath (Filter filter, int range) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Path;
			a.Filter = filter;
			a.range = range;
			a.Find = a.FindPath;
			return a;
		}

		public static Aim CreateArc (int minRange, int range) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Arc;
			a.Filter = Filters.Create;
			a.range = range;
			a.minRange = minRange;
			a.Find = a.FindArc;
			return a;
		}

		public static Aim CreateNeighbor () {
			Aim a = new Aim();
			a.trajectory = Trajectory.Neighbor;
			a.Filter = Filters.Create;
			a.Find = a.FindNeighbor;
			return a;
		}

		public static Aim MoveArc (int minRange, int range) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Arc;
			a.Filter = Filters.Move;
			a.range = range;
			a.minRange = minRange;
			a.Find = a.FindArc;
			return a;
		}

		public static Aim MoveLine (int range) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Line;
			a.Filter = Filters.Move;
			a.range = range;
			a.Find = a.FindLine;
			return a;
		}

		public static Aim MoveNeighbor () {
			Aim a = new Aim();
			a.trajectory = Trajectory.Neighbor;
			a.Filter = Filters.Move;
			a.Find = a.FindNeighbor;
			return a;
		}

		public static Aim MovePath (int range) {
			Aim a = new Aim();
			a.trajectory = Trajectory.Self;
			a.Filter = Filters.Move;
			a.range = range;
			a.Extend = a.MovePath;
			return a;
		}

		public static Aim CreateAren () {
			Aim a = new Aim();
			a.trajectory = Trajectory.Neighbor;
			a.Filter = Filters.Create;
			a.Find = a.FindCreateAren;
			return a;
		}

	}
}

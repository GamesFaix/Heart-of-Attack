namespace HOA { 

	public partial class Aim {
	
		public static void None (Aim aim, AimSeq aims) {}

		public static void MovePath (Aim aim, AimSeq aims) {
			int index = aims.IndexOf(aim);

			Aim a = Aim.MoveNeighbor();
			a.recursiveTarget = true;
			aims.Insert(index+1, a);

			for (byte i=1; i<aim.range; i++) {
				a = Aim.MoveNeighbor();
				a.recursiveTarget = true;
				a.optional = true;
				aims.Insert(index+2, a);
			}
		}
	
	
	
	}
}

using UnityEngine;
using System.Collections.Generic;
namespace HOA {
	public enum ETraj {CELLMATE, NEIGHBOR, PATH, LINE, ARC, FREE, SELF, GLOBAL, RADIAL, OTHER}
	public enum EPurp {MOVE, CREATE, ATTACK, OTHER}

	public partial class Aim : IInspectable, IDeepCopy<Aim>{

		public ETraj Trajectory {get; protected set;}
		public TargetFilter Filter {get; set;}
		public EPurp Purpose {get; protected set;}
		public int Range {get; set;}
		public int MinRange {get; set;}
		
		public delegate TargetSet TargetFinder (Token actor, Cell center, Token other);
		public TargetFinder Targets {get; private set;}

		private Aim () {
            Filter = new TargetFilter( (t) => {return false;} );
		}

		public Aim DeepCopy () {
			Aim a = new Aim();
			a.Trajectory = this.Trajectory;
			a.Filter = this.Filter;
			a.Purpose = this.Purpose;
			a.Range = this.Range;
			a.MinRange = this.MinRange;
			a.Targets = this.Targets;
			return a;
		}

		public string RangeString {
			get {
				if (Trajectory == ETraj.PATH || Trajectory == ETraj.LINE) {return Range+"";}	
				else if (Trajectory == ETraj.ARC) {
					if (MinRange > 0) {return MinRange+"-"+Range;}
					return Range+"";
				}
				return "";
			}
		}

		Texture2D[] TargetIcon {
			get {
				/*if (Filter != TargetFilter.None()) {
					Texture2D[] texs = new Texture2D[TargetClass.Count];
					for (byte i=0; i<texs.Length; i++) {
						texs[i] = Icons.TargetClass(TargetClass[(TargetClasses)i]);
					}
					return texs;
				}
				else
                 * 
                 * */
                Debug.Log("Aim.TargetIcon disabled.");
                return new Texture2D[0];
			}
		}

        public void Draw(Panel p) { InspectorInfo.Aim(this, p); }
	}
}

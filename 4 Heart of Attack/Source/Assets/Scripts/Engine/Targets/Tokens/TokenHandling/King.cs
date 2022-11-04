using UnityEngine; 

namespace HOA { 

    public class King : Unit {

        public King(Source source, Species species, string name, bool unique = false, bool template = false)
            : base (source, species, name, unique, template)
        {
            ScaleJumbo();
            Wallet = new Wallet(this, 3);
        }

        public static King BlackWinnow(Source s, bool template = false)
        {
            King u = new King(s, Species.BlackWinnow, "Black Winnow", true, template);
            u.Plane = Plane.Ground;
            u.OnDeath = Species.SilkHeart;
            u.Health = new Health(u, 75);
            u.Watch = new Watch(u, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Sting(u, 15),
				Ability.CreateLich(u),
				Ability.WebShot(u)
			});
            u.Arsenal.Sort();
            return u;
        }
       
        public static King Decimatrix(Source s, bool template = false)
        {
            King u = new King(s, Species.Decimatrix, "Decimatrix", true);
            u.Plane = Plane.Ground;
            u.Body.Trample = true;
            u.OnDeath = Species.SteelHeart;
            u.Health = new Health(u, 85);
            u.Watch = new Watch(u, 2);
            u.Arsenal.Add(new Ability[]{
				Ability.Tread(u),
				Ability.Shoot(u, 3, 15),
				Ability.Pierce(u, new Price(1,1), 15),
				Ability.Mortar(u),
				//new ADeciFortify(u),
				Ability.Create(u, new Price(1,0), Species.Demolitia),
				Ability.Create(u, new Price(1,1), Species.MeinSchutz),
				Ability.Create(u, new Price(2,2), Species.Panopticannon)
			});
            u.Arsenal.Sort();
            return u;
        }
        
        public static King DreamReaver(Source s, bool template = false)
        {
            King u = new King(s, Species.DreamReaver, "Dream Reaver", true, template);
            u.Plane = Plane.Air;
            u.OnDeath = Species.GlassHeart;
            u.Health = new Health(u, 75, 2);
            u.Watch = new Watch(u, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.PsiBeam(u),
				Ability.Dislocate(u),
				Ability.Create(u, Price.Cheap, Species.PrismGuard),
				Ability.CreateAren(u),
				Ability.Create(u, new Price(1,2), Species.Priest)
			});
            u.Arsenal.Sort();
            return u;
        }	

        public static King Gargoliath(Source s, bool template = false)
        {
            King u = new King(s, Species.Gargoliath, "Gargoliath", true, template);
            u.Plane = Plane.Air;
            u.OnDeath = Species.StoneHeart;
            u.Health = new Health(u, 75);
            u.Watch = new Watch(u, 3);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.Strike(u, 18),
				Ability.Land(u),
				Ability.Petrify(u),
				Ability.CreateRook(u),
				Ability.Create(u, Price.Cheap, Species.Smashbuckler),
				Ability.Create(u, new Price(1,1), Species.Conflagragon),
				Ability.Create(u, new Price(2,2), Species.Rambuchet)
			});
            u.Arsenal.Sort();
            return u;
        }
        
        public static King Kabutomachine(Source s, bool template = false)
        {
            King u = new King(s, Species.Kabutomachine, "Kabutomachine", true, template);
            u.Plane = Plane.Air;
            u.OnDeath = Species.SiliconHeart;
            u.Health = new Health(u, 75);
            u.Watch = new Watch(u, 4);
            u.Arsenal.Add(new Ability[]{
				Ability.Dart(u, 5),
				Ability.Strike(u, 16),
				Ability.Warp(u),
				Ability.GammaBurst(u),
				Ability.Create(u, Price.Cheap, Species.Katandroid),
				Ability.Create(u, new Price(2,1), Species.Carapace),
				Ability.Create(u, new Price(2,2), Species.Mawth)
			});
            u.Arsenal.Sort();
            return u;
        }

        public static King Monolith(Source s, bool template = false)
        {
            King u = new King(s, Species.Monolith, "Monolith", true, template);
            u.Plane = Plane.Tall;
            u.OnDeath = Species.BloodHeart;
            u.ScaleTall();
            u.Health = new Health(u, 100);
            u.Watch = new Watch(u, 2);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 4),
				Ability.Rage(u, 20),
				Ability.DeathField(u),
				Ability.BloodAltar(u),
				Ability.Create(u, new Price(1,0), Species.Recyclops),
				Ability.Recycle(u, new Price(1,0)),
				Ability.Create(u, new Price(2,1), Species.Necro),
				Ability.CreateArc(u, new Price(1,2), Species.Gatecreeper, 3,3)
			});
            u.Arsenal.Sort();
            return u;
        }
        
        public static King OldThreeHands(Source s, bool template = false)
        {
            King u = new King(s, Species.OldThreeHands, "Old Three Hands", true, template);
            u.Plane = Plane.Ground;
            u.OnDeath = Species.BrassHeart;
            u.Health = new Health(u, 85, 2);
            u.Watch = new Watch(u, 2);
            u.Arsenal.Add(new Ability[] {
				Ability.Move(u, 2), 
				Ability.Lob(u, 3, 15),
				Ability.HourSaviour(u),
				Ability.MinuteWaltz(u),
				Ability.SecondInCommand(u),
				Ability.Create(u, Price.Cheap, Species.RevolvingTom),
				Ability.Create(u, new Price(2,0), Species.Piecemaker),
				Ability.Create(u, new Price(2,1), Species.Reprospector)
			});
            u.Arsenal.Sort();
            return u;
        }		

        public static King Ultratherium(Source s, bool template = false)
        {
            King u = new King(s, Species.Ultratherium, "Ultratherium", true, template);
            u.Plane = Plane.Ground;
            u.Body.Trample = true;
            u.OnDeath = Species.FirHeart;
            u.Health = new Health(u, 80);
            u.Watch = new Watch(u, 2);
            u.Arsenal.Add(new Ability[]{
				Ability.Move(u, 3),
				Ability.Strike(u, 16),
				Ability.ThrowTerrain(u),
				Ability.IceBlast(u),
				Ability.Create(u, Price.Cheap, Species.Grizzly),
				Ability.Create(u, new Price(1,1), Species.TalonedScout),
				Ability.Animate(u)
			});
            u.Arsenal.Sort();
            return u;
        }
    }
}

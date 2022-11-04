#define DEBUG

using System;
using System.Collections.Generic;
using HOA.Ab;

namespace HOA.Ref
{
    public delegate Ability AbilityConstructor();

    public static class Abilities
    {
        #region Properties

        public static Ability Move {get; private set;}
        public static Ability Dart {get; private set;}
        public static Ability Embark { get; private set; }
        public static Ability Teleport { get; private set; }
        public static Ability Sprint { get; private set; }
        public static Ability Burrow { get; private set; }

        public static Ability Focus { get; private set; }
        public static Ability End { get; private set; }

        public static Ability Strike { get; private set; }
        public static Ability Shoot { get; private set; }
        public static Ability Lob { get; private set; }

        public static Ability Leech { get; private set; }
        public static Ability Inhale { get; private set; }
        public static Ability Rage { get; private set; }
        public static Ability Donate { get; private set; }
        public static Ability Heal { get; private set; }
        public static Ability Restore { get; private set; }

        public static Ability Create { get; private set; }
        public static Ability Drop { get; private set; }
        public static Ability Summon { get; private set; }
        public static Ability Conjure { get; private set; }
        public static Ability Spawn { get; private set; }
        public static Ability Transmute { get; private set; }
        public static Ability Evolve { get; private set; }

        public static Ability _Add { get; private set; }
        public static Ability _Set { get; private set; }
        public static Ability _Shift { get; private set; }
        public static Ability _Create { get; private set; }
        public static Ability _Destroy { get; private set; }
        public static Ability _Move { get; private set; }
        public static Ability _Capture { get; private set; }
        public static Ability _End { get; private set; }

        #endregion

        public static void Load()
        {
            Log.Start("Abilities loading...");
            Move = Ability.MovePath();
            Dart = Ability.MoveLine();
            Embark = Ability.MovePathFocusBoost();
            Teleport = Ability.Teleport();
            Burrow = Ability.Burrow();
            Sprint = Ability.Sprint();

            Focus = Ability.Focus();
            End = Ability.EndTurn();
            
            Strike = Ability.AttackNeighbor();
            Shoot = Ability.AttackLine();
            Lob = Ability.AttackArc();
            
            Leech = Ability.LeechNeighbor();
            Inhale = Ability.LeechArc();
            Rage = Ability.RageNeighbor();
            Donate = Ability.DonateNeighbor();
            Heal = Ability.HealNeighbor();
            Restore = Ability.HealArc();
            
            Create = Ability.Create();
            Drop = Ability.CreateDrop();
            Summon = Ability.CreateArc();
            Conjure = Ability.CreateFree();
            Spawn = Ability.CreateMulti();
            Transmute = Ability.TransformNeighbor();
            Evolve = Ability.TransformSelf();

            _Add = Ability.ManualAdd();
            _Set = Ability.ManualSet();
            _Shift = Ability.ManualShift();
            _Capture = Ability.ManualOwner();
            _Create = Ability.ManualCreate();
            _Move = Ability.ManualMove();
            _Destroy = Ability.ManualDestroy();
            _End = Ability.ManualEnd();
            Log.Start("...complete.");
        }



    }
}
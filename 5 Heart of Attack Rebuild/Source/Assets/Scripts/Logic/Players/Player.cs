using System;
using UnityEngine;
using System.Collections.Generic;

namespace HOA
{
    /// <summary>
    /// A player.
    /// </summary>
    public class Player
    {
        #region Properties

        /// <summary>
        /// Player name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Player IP address.
        /// </summary>
        public string IP { get; private set; }

        /// <summary>
        /// Is player alive?
        /// </summary>
        public bool Alive { get; private set; }

        /// <summary>
        /// Player faction
        /// </summary>
        public Faction Faction { get; set; }

        #endregion

        /// <summary>
        /// Create player with custom name
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="alive">Is player alive?  Default to true.</param>
        public Player(string name, bool alive = true)
        {
            Name = name;
            Alive = alive;
        }

        /// <summary>
        /// Set alive to false.
        /// </summary>
        public void Kill()
        {
            if (Alive)
                Alive = false;
            else
                throw new Exception(Name + " is already dead.");
        }

        /// <summary>
        /// Returns name.
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return Name; }


        public Set<IEntity> Tokens
        {
            get
            {
                return Session.Active.tokens / Filter.Owner(this, true);
            }
        }

        public void Capture(Player captive)
        {
            foreach (Token t in captive.Tokens)
                t.Owner = this;
        }

    }
}
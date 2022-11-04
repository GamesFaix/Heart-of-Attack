using System;
using System.Collections.Generic;
using HOA.Collections;

namespace HOA
{
    /// <summary> A player. </summary>
    public class Player
    {
        #region Properties

        /// <summary> Player name  </summary>
        public string name { get; set; }
        
        /// <summary> Player IP address. </summary>
        public string ipAddress { get; private set; }

        /// <summary> Is player alive? </summary>
        public bool alive { get; private set; }

        /// <summary> Player faction </summary>
        public Faction faction { get; set; }

        #endregion

        /// <summary> Create player with custom name </summary>
        public Player(string name, bool alive = true)
        {
            this.name = name;
            this.alive = alive;
        }

        /// <summary>  Set alive to false.  </summary>
        public void Kill()
        {
            if (alive)
                alive = false;
            else
                throw new Exception(name + " is already dead.");
        }

        /// <summary> Returns name.  </summary>
        public override string ToString() { return name; }


        public Set<IEntity> Tokens
        {
            get
            {
                return Sessions.Session.Active.tokens / Filter.Owner(this, true);
            }
        }

        public void Capture(Player captive)
        {
            foreach (Tokens.Token t in captive.Tokens)
                t.owner = this;
        }

    }
}
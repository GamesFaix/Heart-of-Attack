using System;
using System.Collections.Generic;


namespace HOA 
{

    public class Session 
    {
        public static Session Active { get; private set; }

        #region Properties

        public Board board { get; private set; }
        private FactionRegistry factionReg { get; set; }
        private PlayerRegistry playerReg { get; set; }
        private Tokens.TokenRegistry tokenReg { get; set; }
        public TurnQueue Queue {get; private set;}
        public bool paused { get; private set; }


        #endregion

        #region Constructors

        public Session()
        {
            Debug.Log("New session started.");
            Active = this;
            factionReg = new FactionRegistry(this);
            playerReg = new PlayerRegistry(this);
            tokenReg = new Tokens.TokenRegistry(this);
            Queue = new TurnQueue(this);
        }

        #endregion

        #region Board

        public void CreateBoard(size2 size) { board = new Board(this, size); }
        public Set<Cell> cells { get { return board.Cells; } }

        #endregion

        #region Players

        public List<Player> players { get { return playerReg.Players; } }
        public void AutoPopulate() { playerReg.AutoPopulate(); }

        #endregion

        #region Factions

        public List<FactionEnum> factions { get { return factionReg.Free; } }
        public void Release(FactionEnum f) { factionReg.Release(f); }
        public Faction Take(FactionEnum f) { return factionReg.Take(f); }

        #endregion

        #region Tokens

        public Set<IEntity> tokens { get { return tokenReg.Tokens; } }
        public int NextAvailableInstance(Tokens.Species s) { return tokenReg.NextAvailableInstance(s); }

        public Token Create(Tokens.ITokenCreator creator, Tokens.Species species, Cell cell)
        {
            return tokenReg.Create(creator, species, cell);
        }

        #endregion

        
        public void ClearLegal()
        {
            tokens.ForEach((e) => { e.Legal = false; });
            cells.ForEach((e) => { e.Legal = false; });
        }
	}
}
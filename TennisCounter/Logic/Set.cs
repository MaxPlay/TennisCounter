using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisCounter.Logic
{
    public class Set
    {

        public event EventHandler<EventArgs> TogglePlayer1Serves;
        public void OnTogglePlayer1Serves()
        {
            if (TogglePlayer1Serves != null)
                TogglePlayer1Serves(this, new EventArgs());
        }
        #region Protected Fields

        protected Winner winner;

        #endregion Protected Fields

        #region Private Fields

        private int currentGame;
        private List<Game> games;
        private int maxGamesPerSet;
        private MatchSettings settings;
        private bool tiebreak;

        #endregion Private Fields

        #region Public Constructors

        public Set(MatchSettings settings)
        {
            this.maxGamesPerSet = settings.MaxGames;
            this.settings = settings;
            winner = Winner.None;
            games = new List<Game>();
            games.Add(new Game(settings));
            games[currentGame].TogglePlayer1Serves+=Set_TogglePlayer1Serves;
        }

        private void Set_TogglePlayer1Serves(object sender, EventArgs e)
        {
            OnTogglePlayer1Serves();
        }

        #endregion Public Constructors

        #region Public Properties

        public int CurrentGame
        {
            get { return currentGame + 1; }
        }

        public int PointsPlayer1 { get { return games.Count(g => g.Winner == Winner.Player1); } }
        public int PointsPlayer2 { get { return games.Count(g => g.Winner == Winner.Player2); } }
        public Winner Winner { get { return winner; } }

        #endregion Public Properties

        #region Public Methods

        public virtual string GetCurrentGamePlayer1Progress()
        {
            return games[currentGame].GetPlayer1Progress();
        }

        public virtual string GetCurrentGamePlayer2Progress()
        {
            return games[currentGame].GetPlayer2Progress();
        }

        #endregion Public Methods

        #region Internal Methods

        internal virtual void IncreasePointPlayer1()
        {
            games[currentGame].IncreasePointPlayer1();
            GetWinner();
        }

        internal virtual void IncreasePointPlayer2()
        {
            games[currentGame].IncreasePointPlayer2();
            GetWinner();
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void GetWinner()
        {
            if (games[currentGame].Winner == Winner.None)
                return;

            if (tiebreak)
                if (games[currentGame].Winner != Winner.None)
                    this.winner = games[currentGame].Winner;
                else
                    return;

            if (Math.Abs(PointsPlayer1 - PointsPlayer2) >= 2 && (PointsPlayer1 == 6 || PointsPlayer2 == 6))
                this.winner = games[currentGame].Winner;

            if (PointsPlayer1 == 6 && PointsPlayer2 == 6)
            {
                games.Add(new Tiebreak(settings));
                tiebreak = true;
                currentGame++;
                games[currentGame].TogglePlayer1Serves += Set_TogglePlayer1Serves;
                return;
            }

            games.Add(new Game(settings));
            currentGame++;
            games[currentGame].TogglePlayer1Serves += Set_TogglePlayer1Serves;
        }

        #endregion Protected Methods
    }
}
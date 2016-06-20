using System.Collections.Generic;

namespace TennisCounter.Logic
{
    public class Match
    {
        #region Private Fields

        private int currentSet;
        private int maxSetsPerMatch;
        private bool player1Serves;
        private List<Set> sets;
        private MatchSettings settings;

        private Winner winner;

        #endregion Private Fields

        #region Public Constructors

        public Match(MatchSettings settings)
        {
            this.settings = settings;
            maxSetsPerMatch = settings.MaxSets;
            sets = new List<Set>();
            winner = Winner.None;
        }

        #endregion Public Constructors

        #region Public Properties

        public int CurrentSet
        {
            get
            {
                return this.currentSet + 1;
            }
        }

        public bool Player1Serves
        {
            get { return player1Serves; }
            set { player1Serves = value; }
        }

        public MatchSettings Settings
        {
            get { return settings; }
        }

        public Winner Winner
        {
            get { return winner; }
        }

        #endregion Public Properties

        #region Public Methods

        public string GetPlayer1GameProgress()
        {
            return sets[currentSet].GetCurrentGamePlayer1Progress();
        }

        public string GetPlayer1Games(int set)
        {
            if (set < currentSet)
                return "";

            return sets[set].PointsPlayer1.ToString();
        }

        public string GetPlayer2GameProgress()
        {
            return sets[currentSet].GetCurrentGamePlayer2Progress();
        }

        public string GetPlayer2Games(int set)
        {
            if (set < currentSet)
                return "";

            return sets[set].PointsPlayer2.ToString();
        }

        public void IncreasePointPlayer1()
        {
            sets[currentSet].IncreasePointPlayer1();
            GetWinner();
        }

        public void IncreasePointPlayer2()
        {
            sets[currentSet].IncreasePointPlayer2();
            GetWinner();
        }

        #endregion Public Methods

        #region Private Methods

        private void GetWinner()
        {
            if (sets[currentSet].Winner == Winner.None)
                return;

            if (currentSet == maxSetsPerMatch && sets[currentSet].Winner != Winner.None)
                this.winner = sets[currentSet].Winner;

            if (currentSet == maxSetsPerMatch - 1 && settings.SuperTiebreak)
                sets.Add(new SuperTiebreak(settings));

            sets.Add(new Set(settings.MaxGames, settings));
            currentSet++;
        }

        #endregion Private Methods
    }
}
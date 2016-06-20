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
            if (maxSetsPerMatch == 1 && settings.SuperTiebreak)
                sets.Add(new SuperTiebreak(settings));
            else
                sets.Add(new Set(settings));
            winner = Winner.None;

            sets[currentSet].TogglePlayer1Serves += Match_TogglePlayer1Serves;
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
            if (set > currentSet)
                return "";

            return sets[set].PointsPlayer1.ToString();
        }

        public string GetPlayer2GameProgress()
        {
            return sets[currentSet].GetCurrentGamePlayer2Progress();
        }

        public string GetPlayer2Games(int set)
        {
            if (set > currentSet)
                return "";

            return sets[set].PointsPlayer2.ToString();
        }

        public Winner GetSetResult(int set)
        {
            if (set >= sets.Count)
                return Winner.None;

            return sets[set].Winner;
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

            if (IsThereAWinner())
                this.winner = sets[currentSet].Winner;

            if (this.winner != Logic.Winner.None)
                return;

            if (currentSet == maxSetsPerMatch - 1 && settings.SuperTiebreak)
            {
                sets.Add(new SuperTiebreak(settings));
                currentSet++;
                sets[currentSet].TogglePlayer1Serves += Match_TogglePlayer1Serves;
                return;
            }

            sets.Add(new Set(settings));
            currentSet++;
            sets[currentSet].TogglePlayer1Serves += Match_TogglePlayer1Serves;
        }

        private bool IsThereAWinner()
        {
            if (maxSetsPerMatch / 2 > currentSet + 1)
                return false;

            int Player1WinCount = 0;
            int Player2WinCount = 0;
            for (int i = 0; i < currentSet + 1; i++)
            {
                if (sets[i].Winner == Logic.Winner.Player1)
                    Player1WinCount++;

                if (sets[i].Winner == Logic.Winner.Player2)
                    Player2WinCount++;
            }

            if (maxSetsPerMatch / 2 < Player1WinCount || maxSetsPerMatch / 2 < Player2WinCount)
                return true;

            return false;
        }

        private void Match_TogglePlayer1Serves(object sender, System.EventArgs e)
        {
            player1Serves = !player1Serves;
        }

        #endregion Private Methods
    }
}
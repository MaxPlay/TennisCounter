using System;

namespace TennisCounter.Logic
{
    public class Game
    {
        #region Protected Fields

        protected bool noAdvantage;
        protected int player1;
        protected int player2;
        protected Winner winner;

        #endregion Protected Fields

        #region Private Fields

        private string advantageText;

        #endregion Private Fields

        #region Public Constructors

        public Game(MatchSettings settings)
        {
            this.noAdvantage = settings.NoAdvantage;
            this.advantageText = settings.AdvantageText;
            winner = Winner.None;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<EventArgs> TogglePlayer1Serves;

        #endregion Public Events

        #region Public Properties

        public bool NoAdvantage
        {
            get { return noAdvantage; }
        }

        public int PointsPlayer1 { get { return player1; } }

        public int PointsPlayer2 { get { return player2; } }

        public Winner Winner { get { return winner; } }

        #endregion Public Properties

        #region Public Methods

        public void OnTogglePlayer1Serves()
        {
            if (TogglePlayer1Serves != null)
                TogglePlayer1Serves(this, new EventArgs());
        }

        #endregion Public Methods

        #region Internal Methods

        internal virtual string GetPlayer1Progress()
        {
            return ProgressParser(player1);
        }

        internal virtual string GetPlayer2Progress()
        {
            return ProgressParser(player2);
        }

        internal virtual void IncreasePointPlayer1()
        {
            player1++;
            if (!noAdvantage && player1 == 4 && player2 == 4)
            {
                player2--;
                player1--;
            }
            GetWinner();
        }

        internal virtual void IncreasePointPlayer2()
        {
            player2++;
            if (!noAdvantage && player1 == 4 && player2 == 4)
            {
                player2--;
                player1--;
            }
            GetWinner();
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void GetWinner()
        {
            if (player1 < 4 && player2 < 4)
                return;

            if (Math.Abs(player1 - player2) > 1)
                if (player1 > player2)
                {
                    winner = Winner.Player1;
                    OnTogglePlayer1Serves();
                    return;
                }
                else
                {
                    winner = Winner.Player2;
                    OnTogglePlayer1Serves();
                    return;
                }

            if (player1 == 4 && NoAdvantage)
            {
                winner = Winner.Player1;
                OnTogglePlayer1Serves();
                return;
            }

            if (player2 == 4 && NoAdvantage)
            {
                winner = Winner.Player2;
                OnTogglePlayer1Serves();
                return;
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private string ProgressParser(int playervalue)
        {
            switch (playervalue)
            {
                case 1:
                    return "15";

                case 2:
                    return "30";

                case 3:
                    return "40";

                case 4:
                    return advantageText;

                default:
                    return "0";
            }
        }

        #endregion Private Methods
    }
}
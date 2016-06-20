using System;

namespace TennisCounter.Logic
{
    internal class SuperTiebreak : Set, ITiebreak
    {
        #region Private Fields

        private int minLength;
        private int player1;
        private int player2;

        #endregion Private Fields

        #region Public Constructors

        public SuperTiebreak(MatchSettings settings)
            : base(0, settings)
        {
            this.minLength = settings.SuperTiebreakLength;
        }

        #endregion Public Constructors

        #region Public Properties

        public new int PointsPlayer1
        {
            get
            {
                return player1;
            }
        }

        public new int PointsPlayer2
        {
            get
            {
                return player2;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override string GetCurrentGamePlayer1Progress()
        {
            return player1.ToString();
        }

        public override string GetCurrentGamePlayer2Progress()
        {
            return player2.ToString();
        }

        #endregion Public Methods

        #region Internal Methods

        internal override void IncreasePointPlayer1()
        {
            player1++;
            GetWinner();
        }

        internal override void IncreasePointPlayer2()
        {
            player2++;
            GetWinner();
        }

        #endregion Internal Methods

        #region Protected Methods

        protected override void GetWinner()
        {
            if (player1 < minLength && player2 < minLength)
                return;

            if (Math.Abs(player1 - player2) >= 2)
                winner = player1 > player2 ? Winner.Player1 : Winner.Player2;
        }

        #endregion Protected Methods
    }
}
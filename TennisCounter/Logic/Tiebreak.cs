using System;

namespace TennisCounter.Logic
{
    public class Tiebreak : Game, ITiebreak
    {
        #region Public Constructors

        public Tiebreak(MatchSettings settings)
            : base(settings)
        {
        }

        #endregion Public Constructors

        #region Internal Methods

        internal override string GetPlayer1Progress()
        {
            return player1.ToString();
        }

        internal override string GetPlayer2Progress()
        {
            return player2.ToString();
        }

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
            if (player1 < 7 && player2 < 7)
                return;

            if (Math.Abs(player1 - player2) >= 2)
                winner = player1 > player2 ? Winner.Player1 : Winner.Player2;
        }

        #endregion Protected Methods
    }
}
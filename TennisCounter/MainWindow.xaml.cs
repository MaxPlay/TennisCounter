using System.Windows;
using System.Windows.Media;
using TennisCounter.Logic;

namespace TennisCounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields

        private SolidColorBrush defaultColor;
        private Match match;
        private SolidColorBrush winningColor;

        #endregion Private Fields

        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
            MatchSettings settings = new MatchSettings();
            settings.AdvantageText = "Ad";
            settings.MaxGames = 6;
            settings.MaxSets = 3;
            settings.NoAdvantage = false;
            settings.NoTiebreakInFinalSet = true;
            settings.SuperTiebreak = false;
            settings.SuperTiebreakLength = 10;
            match = new Match(settings);
            defaultColor = new SolidColorBrush(Color.FromRgb(37, 37, 38));
            winningColor = new SolidColorBrush(Color.FromRgb(38, 77, 37));

            UpdateGui();
        }

        #endregion Public Constructors

        #region Private Methods

        private void AddPointPlayer1_Click(object sender, RoutedEventArgs e)
        {
            match.IncreasePointPlayer1();
            UpdateGui();
        }

        private void AddPointPlayer2_Click(object sender, RoutedEventArgs e)
        {
            match.IncreasePointPlayer2();
            UpdateGui();
        }

        private void UpdateGui()
        {
            Player1GameState.Content = match.GetPlayer1GameProgress();
            Player2GameState.Content = match.GetPlayer2GameProgress();

            Player1Set1.Content = match.GetPlayer1Games(0);
            Player1Set1.Background = match.GetSetResult(0) == Winner.Player1 ? winningColor : defaultColor;
            Player1Set2.Content = match.GetPlayer1Games(1);
            Player1Set2.Background = match.GetSetResult(1) == Winner.Player1 ? winningColor : defaultColor;
            Player1Set3.Content = match.GetPlayer1Games(2);
            Player1Set3.Background = match.GetSetResult(2) == Winner.Player1 ? winningColor : defaultColor;

            Player2Set1.Content = match.GetPlayer2Games(0);
            Player2Set1.Background = match.GetSetResult(0) == Winner.Player2 ? winningColor : defaultColor;
            Player2Set2.Content = match.GetPlayer2Games(1);
            Player2Set2.Background = match.GetSetResult(1) == Winner.Player2 ? winningColor : defaultColor;
            Player2Set3.Content = match.GetPlayer2Games(2);
            Player2Set3.Background = match.GetSetResult(2) == Winner.Player2 ? winningColor : defaultColor;

            Player1Serve.Content = match.Player1Serves ? "●" : "";
            Player2Serve.Content = match.Player1Serves ? "" : "●";

            if (match.Winner != Winner.None)
            {
                AddPointPlayer1.IsEnabled = false;
                AddPointPlayer2.IsEnabled = false;
            }
        }

        #endregion Private Methods
    }
}
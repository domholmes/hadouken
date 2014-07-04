using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Blackjack.Helpers;
using Blackjack.ViewModels;
using Blackjack.Models;

namespace BlackJack.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private int? playerScore;
        private Deck deck;
        private readonly Scorer scorer;
        private bool gameHasStarted;

        public ICommand NewGameCommand { get { return new DelegateCommand(OnNewGame); } }
        public ICommand TwistCommand { get { return new DelegateCommand(OnTwist, () => PlayerCanTwist); } }

        public int? PlayersScore
        {
            get { return playerScore; }
            private set
            {
                if (playerScore != value)
                {
                    playerScore = value;
                    RaisePropertyChanged(() => PlayersScore);
                    RaisePropertyChanged(() => GameStatus);
                }
            }
        }

        public string GameStatus
        {
            get 
            { 
                return !gameHasStarted ? "Welcome" : 
                    (PlayerIsBust ? "Bust!" : "Your turn"); 
            }
        }

        public ObservableCollection<Card> PlayersHand
        { 
            get; 
            private set; 
        }

        private bool PlayerCanTwist
        {
            get { return !PlayerIsBust; }
        }

        private bool PlayerIsBust
        {
            get { return !this.PlayersScore.HasValue; }
        }

        public MainWindowViewModel()
        {
            this.scorer = new Scorer();
            this.PlayersHand = new ObservableCollection<Card>();
        }

        private void OnNewGame()
        {
            this.gameHasStarted = true;
            this.PlayersScore = 0;
            this.PlayersHand.Clear();
            this.deck = new Deck();

            OnTwist();
            OnTwist();
        }

        private void OnTwist()
        {
            PlayersHand.Add(this.deck.DrawCard());

            this.PlayersScore = this.scorer.Score(PlayersHand);
        }
    }
}
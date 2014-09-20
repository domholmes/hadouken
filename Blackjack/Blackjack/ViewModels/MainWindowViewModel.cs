using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Blackjack.Helpers;
using Blackjack.ViewModels;
using Blackjack.Models;
using System.Threading;

namespace BlackJack.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private const int MAX_HAND = 5;
        private int? player1Score;
        private int? player2Score;
        private Deck deck;
        private readonly Scorer scorer;
        private bool firstGameStarted;
        private bool playerHasStuck;
        private bool cpuHasFinished;

        private Player player1;
        private Player player2;
        private Player currentPlayer;

        public ICommand NewGameCommand { get { return new DelegateCommand(OnNewGame); } }
        public ICommand TwistCommand { get { return new DelegateCommand(OnTwist, () => PlayerCanTwist); } }
        public ICommand StickCommand { get { return new DelegateCommand(OnStick, () => PlayerCanStick); } }

        public int? PlayersScore
        {
            get { return player1Score; }
            private set
            {
                if (player1Score != value)
                {
                    player1Score = value;
                    RaisePropertyChanged(() => PlayersScore);
                }
            }
        }

        public int? CpusScore
        {
            get { return player2Score; }
            private set
            {
                if (player2Score != value)
                {
                    player2Score = value;
                    RaisePropertyChanged(() => CpusScore);
                }
            }
        }

        public string GameStatus
        {
            get 
            { 
                return !firstGameStarted ? "Welcome" : 
                    (PlayerIsBust ? "Bust!" : 
                    playerHasStuck ?
                    cpuHasFinished ? (this.player2Score == null || this.player1Score >= this.player2Score ? "You won!" : "You lost") : "You have stuck on " + PlayersScore : "Your turn"); 
            }
        }

        public ObservableCollection<Card> p1Hand
        { 
            get; 
            private set; 
        }

        public ObservableCollection<Card> p2Hand
        {
            get;
            private set;
        }

        private bool PlayerCanTwist
        {
            get { return IsHumansTurn && !PlayerIsBust && !playerHasStuck; }
        }

        private bool PlayerCanStick
        {
            get { return firstGameStarted && !PlayerIsBust && !playerHasStuck; }
        }

        private bool PlayerIsBust
        {
            get { return !this.PlayersScore.HasValue; }
        }

        private bool PlayerHasMaxHand
        {
            get { return this.p1Hand.Count >= 5; }
        }

        private bool CpuIsBust
        {
            get { return !this.CpusScore.HasValue; }
        }
        
        private bool IsHumansTurn
        {
            get { return this.currentPlayer is HumanPlayer; }
        }

        public MainWindowViewModel()
        {
            this.scorer = new Scorer();
            this.p1Hand = new ObservableCollection<Card>();
            this.p2Hand = new ObservableCollection<Card>();
        }

        private void OnNewGame()
        {
            this.firstGameStarted = true;
            
            //this.playerHasStuck = false;
            //this.PlayersScore = 0;
            //this.PlayersHand.Clear();

            //this.CpusScore = 0;
            //this.CpusHand.Clear();
            
            this.deck = new Deck();

            this.player1 = new HumanPlayer();
            this.player2 = new CpuPlayer();

            this.currentPlayer = this.player1;

            OnTwist();
            OnTwist();
        }

        private void OnTwist()
        {
            p1Hand.Add(this.deck.DrawCard());

            this.PlayersScore = this.scorer.Score(p1Hand);

            RaisePropertyChanged(() => GameStatus);

            if(PlayerHasMaxHand && !PlayerIsBust)
            {
                OnStick();
            }
        }

        private void OnStick()
        {
            playerHasStuck = true;

            RaisePropertyChanged(() => GameStatus);

            CpuPlay();

            cpuHasFinished = true;

            RaisePropertyChanged(() => GameStatus);
        }

        private void CpuPlay()
        {
            p2Hand.Add(this.deck.DrawCard());
            p2Hand.Add(this.deck.DrawCard());
            this.CpusScore = this.scorer.Score(p2Hand);

            while(!CpuIsBust && this.player2Score <= this.player1Score)
            {
                p2Hand.Add(this.deck.DrawCard());
                this.CpusScore = this.scorer.Score(p2Hand);
            }
        }
    }
}
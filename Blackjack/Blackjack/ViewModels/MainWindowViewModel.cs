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
        private const int MaxHand = 5;
        private int? playerScore;
        private int? cpusScore;
        private Deck deck;
        private readonly Scorer scorer;
        private readonly CpuPlayer cpuPlayer;
        private bool firstGameStarted;
        private bool playerHasStuck;
        private bool cpuHasFinished;

        public ICommand NewGameCommand { get { return new DelegateCommand(OnNewGame); } }
        public ICommand TwistCommand { get { return new DelegateCommand(OnTwist, () => PlayerCanTwist); } }
        public ICommand StickCommand { get { return new DelegateCommand(OnStick, () => PlayerCanStick); } }

        public int? PlayersScore
        {
            get { return playerScore; }
            private set
            {
                if (playerScore != value)
                {
                    playerScore = value;
                    RaisePropertyChanged(() => PlayersScore);
                }
            }
        }

        public int? CpusScore
        {
            get { return cpusScore; }
            private set
            {
                if (cpusScore != value)
                {
                    cpusScore = value;
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
                    cpuHasFinished ? (this.cpusScore == null || this.playerScore >= this.cpusScore ? "You won!" : "You lost") : "You have stuck on " + PlayersScore : "Your turn"); 
            }
        }

        public ObservableCollection<Card> PlayersHand
        { 
            get; 
            private set; 
        }

        public ObservableCollection<Card> CpusHand
        {
            get;
            private set;
        }

        private bool PlayerCanTwist
        {
            get { return !PlayerIsBust && !playerHasStuck; }
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
            get { return this.PlayersHand.Count >= 5; }
        }

        private bool CpuIsBust
        {
            get { return !this.CpusScore.HasValue; }
        }

        public MainWindowViewModel()
        {
            this.scorer = new Scorer();
            this.cpuPlayer = new CpuPlayer();
            this.PlayersHand = new ObservableCollection<Card>();
            this.CpusHand = new ObservableCollection<Card>();
        }

        private void OnNewGame()
        {
            this.firstGameStarted = true;
            
            this.playerHasStuck = false;
            this.PlayersScore = 0;
            this.PlayersHand.Clear();

            this.CpusScore = 0;
            this.CpusHand.Clear();
            
            this.deck = new Deck();

            OnTwist();
            OnTwist();
        }

        private void OnTwist()
        {
            PlayersHand.Add(this.deck.DrawCard());

            this.PlayersScore = this.scorer.Score(PlayersHand);

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
            CpusHand.Add(this.deck.DrawCard());
            CpusHand.Add(this.deck.DrawCard());
            this.CpusScore = this.scorer.Score(CpusHand);

            while(!CpuIsBust && this.cpusScore <= this.playerScore)
            {
                CpusHand.Add(this.deck.DrawCard());
                this.CpusScore = this.scorer.Score(CpusHand);
            }
        }
    }
}
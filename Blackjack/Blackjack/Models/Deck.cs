using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Models
{
    public class Deck
    {
        private readonly IList<Card> deck;
        
        public Deck()
        {
            this.deck = new List<Card>();

            BuildDeck();
        }

        private void BuildDeck()
        {
            foreach(Suits suit in Enum.GetValues(typeof(Suits)))
            {
                this.deck.Add(new Card(suit, Faces.Jack));
                this.deck.Add(new Card(suit, Faces.King));
                this.deck.Add(new Card(suit, Faces.Queen));
                this.deck.Add(new Card(suit, Faces.Ace));

                for(int i = 2; i <= 10; i++)
                {
                    this.deck.Add(new Card(suit, i));
                }
            }
        }

        public bool CanDeal
        {
            get { return this.deck.Any(); }
        }

        public Card DrawCard()
        {
            if(!this.deck.Any())
            {
                return null;
            }
            
            int randomIndex = new Random().Next(this.deck.Count - 1);

            var card = this.deck[randomIndex];
            this.deck.RemoveAt(randomIndex);

            return card;
        }
    }
}

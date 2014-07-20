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
        private readonly Random random;

        public Deck()
        {
            this.deck = new List<Card>();
            this.random = new Random();

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

        public Card DrawCard()
        {
            if(!this.deck.Any())
            {
                return null;
            }
            
            int randomIndex = this.random.Next(this.deck.Count - 1);

            var card = this.deck[randomIndex];
            this.deck.RemoveAt(randomIndex);

            return card;
        }
    }
}

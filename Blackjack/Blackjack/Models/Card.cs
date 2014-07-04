using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Models
{
    public class Card
    {
        public Card(Suits suit, Faces face)
        {
            this.Suit = suit;
            this.Face = face;
        }

        public Card(Suits suit, int number)
        {
            this.Suit = suit;
            this.Number = number;
        }

        public Suits Suit { get; private set; }

        public Faces? Face { get; private set; }

        public int? Number { get; private set; }

        public bool IsFace { get { return Face.HasValue; } }

        public override string ToString()
        {
            return (IsFace ? Face.Value.ToString() : Number.Value.ToString()) + " of " + Suit.ToString();
        }
    }
}

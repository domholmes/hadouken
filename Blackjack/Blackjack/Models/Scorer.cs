using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.Models
{
    public class Scorer
    {
        private const int TARGET_SCORE = 21;
        private const int FACE_SCORE = 10;
        private const int ACES_HIGH = FACE_SCORE;
        private const int ACES_LOW = 1;

        public int? Score(IEnumerable<Card> hand)
        {
            int? score = 0;

            foreach(Card card in hand)
            {
                if(card.IsFace)
                {
                    if (card.Face == Faces.Ace)
                    {
                        if ((score + ACES_HIGH) > TARGET_SCORE)
                        {
                            score += ACES_LOW;
                        }
                        else
                        {
                            score += ACES_HIGH;
                        }
                    }
                    else
                    {
                        score += FACE_SCORE;
                    }
                }
                else
                {
                    score += card.Number;
                }
            }

            if(score > TARGET_SCORE)
            {
                return null;
            }

            return score;
        }
    }
}

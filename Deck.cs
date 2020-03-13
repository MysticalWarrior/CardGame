using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _205704CardGame2
{
    class Deck
    {
        private Random mRandom = new Random();
        public Card[] cards;
        private int locationInDeck = 0;

        public Deck()
        {
            cards = new Card[52];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = new Card(i);
            }
            //MessageBox.Show(outputCards());
        }

        public Deck(int i)
        {
            cards = new Card[i];
            for (int ii = 0; ii < cards.Length; ii++)
            {
                cards[ii] = new Card(ii);
            }
            //MessageBox.Show(outputCards());
        }

        public Card dealCard()
        {
            Card c = cards[locationInDeck];
            cards[locationInDeck].isDealt = true;
            //MessageBox.Show(deck.cards[locationInDeck].ToString());
            locationInDeck++;
            return c;
        }

        public void shuffleDeck()
        {
            for(int i = 0; i < cards.Length; i++)
            {
                int toBeChanged = mRandom.Next(cards.Length);
                Card temp = cards[i];
                cards[i] = cards[toBeChanged];
                cards[toBeChanged] = temp;
            }
        }

        public string outputCards()
        {
            string output = "";
            foreach(Card card in cards) { output += card.ToString() + '\r' + "isDealt=" + card.isDealt.ToString(); }
            return output;
        }
    }
}

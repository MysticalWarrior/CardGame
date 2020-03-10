using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _205704CardGame2
{
    class Card
    {
        private int mCardValue = -1;
        public enum mSuit { Heart, Diamond, Club, Spade };
        public enum mFace { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
        public Card()
        {
            mCardValue = 0;
        }
        public Card(int i)
        {
            mCardValue = i;
        }
        public mSuit Suit(){ return (mSuit)(mCardValue / 13); }
        public mFace Face(){ return (mFace)(mCardValue % 13); }

        public override string ToString()
        {
            return Face().ToString() +" of " + Suit().ToString() + "s";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _205704CardGame2
{
    class Card
    {
        private int mCardValue = -1;
        public enum mSuit { Club, Diamond, Heart, Spade };
        public enum mFace { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };
        public bool isDealt = false;
        public Card()
        {
            mCardValue = 0;
        }
        public Card(int i)
        {
            mCardValue = i;
        }
        public mSuit Suit() { return (mSuit)(mCardValue / 13); }
        public mFace Face() { return (mFace)(mCardValue % 13); }
        public int Value
        {
            get
            {
                return mCardValue;
            }
        }

        public override string ToString()
        {
            return Face().ToString() +" of " + Suit().ToString() + "s";
        }
        
    }
}

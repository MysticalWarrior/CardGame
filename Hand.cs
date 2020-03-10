using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _205704CardGame2
{
    class Hand : List<Card>
    {
        private string name;

        public Hand(string n) : base()
        {
            name = n;
        }

        public override string ToString()
        {
            string output = name + "'s Hand:" + '\r';
            foreach (Card card in this) { output += card.ToString() + '\r'; }
            return output;
        }
    }
}

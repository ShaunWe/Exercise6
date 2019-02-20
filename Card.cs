using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise6
{
    class Card
    {
        char _card;
        int _value;
        ConsoleColor _cardColor;

        public char CardCode
        {
            get { return _card; }
        }

        public int Value
        {
            get { return _value; }
        }

        public ConsoleColor CardColor
        {
            get { return _cardColor; }
        }

        public Card (char card, int value, ConsoleColor color)
        {
            _card = card;
            _value = value;
            _cardColor = color;
        }
    }
}

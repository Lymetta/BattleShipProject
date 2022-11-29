using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsProject
{
    public struct Coordinate
    {
        public char Letter { get; }
        public int Number { get; }

        public Coordinate(char letter, int number)
        {
            Letter = letter;
            Number = number;
        }
    }
}

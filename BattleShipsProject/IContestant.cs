using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsProject
{
    internal interface IContestant
    {
        List<List<Coordinate>> GetShipPlacement();

        Coordinate SalvoAt();
        
        void SalvoResults(Coordinate at, bool hit, bool sunk);
        
        void IncomingSalvo(Coordinate at);
    }
}

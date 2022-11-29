using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsProject
{
    internal class BattleShip : IContestant
    {
        private Random random = new Random();

        private List<char> freeLetters = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

        private List<int> freeNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        private List<Coordinate> freeSpaces = new List<Coordinate>(); // my free spaces

        private List<Coordinate> notShotAtYet = new List<Coordinate>(); // oppenents field

        private List<Coordinate> coordsOfSunkShips = new List<Coordinate>(); // coord of opponent ships;

        private Coordinate coordOfLastSuccessfulHit = new Coordinate('Z', 50);
        private Coordinate resetCoord = new Coordinate('Z', 50);
        private bool lastHitSuccess = false;
        private bool NewGameStart = false;

        private bool foundShip = false;
        private Coordinate initialFirstHit = new Coordinate();


        private Coordinate nextCoordToShoot = new Coordinate('Z', 50);

        public List<Coordinate> initialShots = new List<Coordinate>();

        public BattleShip()
        {

        }


        public List<List<Coordinate>> GetShipPlacement()
        {
            var shipList = new List<List<Coordinate>>();

            NewGameStart = true;
            freeSpaces.Clear();
            notShotAtYet.Clear();
            coordsOfSunkShips.Clear();
            FillFreeSpacesAndNotYetShot();
            coordOfLastSuccessfulHit = resetCoord;
            nextCoordToShoot = resetCoord;

            if (initialShots.Count > 0)
            {
                initialShots.Clear();
            }


            FillInitialShotList();

            shipList.Add(CreateShip(4));
            shipList.Add(CreateShip(3));
            shipList.Add(CreateShip(3));
            shipList.Add(CreateShip(2));
            shipList.Add(CreateShip(2));
            shipList.Add(CreateShip(2));
            shipList.Add(CreateShip(1));
            shipList.Add(CreateShip(1));
            shipList.Add(CreateShip(1));
            shipList.Add(CreateShip(1));



            return shipList;
        }

        public Coordinate SalvoAt()
        {
            Coordinate coord = new Coordinate();

            if (NewGameStart)
            {
                coord = initialShots[5];

                NewGameStart = false;
            }
            else
            {

                if (foundShip)
                {
                    coord = nextCoordToShoot;
                }

                else if (initialShots.Count > 0)
                {
                    coord = initialShots[random.Next(initialShots.Count)];


                }
                else
                {
                    coord = ShootRandom();
                }

            }

            initialShots.Remove(coord);
            notShotAtYet.Remove(coord);
            return coord;
        }

        public void SalvoResults(Coordinate at, bool hit, bool sunk)
        {
            if (sunk)
            {
                coordsOfSunkShips.Add(at);
                RemoveSurroundingAfterSink(coordsOfSunkShips);

                foundShip = false;
                coordOfLastSuccessfulHit = resetCoord;

                initialFirstHit = resetCoord;




            }
            else if (hit && !sunk)
            {
                if (foundShip)
                {
                    var targetList = new List<Coordinate>();
                    if (at.Letter == coordOfLastSuccessfulHit.Letter)
                    {
                        targetList = GetVertical(at);

                    }
                    else if (at.Number == coordOfLastSuccessfulHit.Number)
                    {
                        targetList = GetHorizontal(at);
                    }

                    if (targetList.Count > 0)
                    {
                        nextCoordToShoot = targetList[0];
                    }
                    else
                    {
                        if (at.Letter == initialFirstHit.Letter)
                        {
                            targetList = GetVertical(initialFirstHit);

                        }
                        else if (at.Number == initialFirstHit.Number)
                        {
                            targetList = GetHorizontal(initialFirstHit);
                        }

                        if (targetList.Count > 0)
                        {
                            nextCoordToShoot = targetList[0];
                        }
                        else

                        {
                            if (at.Letter == initialFirstHit.Letter)
                            {
                                targetList = GetHorizontal(initialFirstHit);

                            }
                            else if (at.Number == initialFirstHit.Number)
                            {
                                targetList = GetVertical(initialFirstHit);
                            }

                            nextCoordToShoot = targetList[0];

                        }

                    }

                }
                else
                {
                    var targetList = GetCross(at);
                    nextCoordToShoot = targetList[0];

                    initialFirstHit = at;
                    foundShip = true;
                }

                coordsOfSunkShips.Add(at);

                coordOfLastSuccessfulHit = at;
            }
            else // miss
            {
                if (foundShip)
                {
                    var targetList = new List<Coordinate>();
                    if (at.Letter == coordOfLastSuccessfulHit.Letter)
                    {
                        targetList = GetHorizontal(coordOfLastSuccessfulHit);
                    }
                    else if (at.Number == coordOfLastSuccessfulHit.Number)
                    {
                        targetList = GetVertical(coordOfLastSuccessfulHit);
                    }


                    if (targetList.Count > 0)
                    {
                        nextCoordToShoot = targetList[0];
                    }
                    else
                    {
                        if (coordOfLastSuccessfulHit.Letter == initialFirstHit.Letter)
                        {
                            targetList = GetVertical(initialFirstHit);

                        }
                        else if (coordOfLastSuccessfulHit.Number == initialFirstHit.Number)
                        {
                            targetList = GetHorizontal(initialFirstHit);
                        }

                        if (targetList.Count > 0)
                        {
                            nextCoordToShoot = targetList[0];
                        }
                        else
                        {
                            if (coordOfLastSuccessfulHit.Letter == initialFirstHit.Letter)
                            {
                                targetList = GetHorizontal(initialFirstHit);

                            }
                            else if (coordOfLastSuccessfulHit.Number == initialFirstHit.Number)
                            {
                                targetList = GetVertical(initialFirstHit);
                            }
                            nextCoordToShoot = targetList[0];
                        }

                    }



                }
                else
                {
                    nextCoordToShoot = ShootRandom();
                    lastHitSuccess = false;
                }
            }

        }


        public void IncomingSalvo(Coordinate at)
        {

        }


        private void FillInitialShotList()
        {

            foreach (var letter in freeLetters)
            {
                foreach (var num in freeNumbers)
                {
                    if (freeLetters.IndexOf(letter) == freeNumbers.IndexOf(num))
                    {
                        Coordinate coord = new Coordinate(letter, num);
                        if (!initialShots.Contains(coord))
                        {
                            initialShots.Add(coord);
                        }

                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                Coordinate coord = new Coordinate(freeLetters[i], freeNumbers[9 - i]);

                if (!initialShots.Contains(coord))
                {
                    initialShots.Add(coord);
                }
            }

        }

        private Coordinate ShootRandom()
        {

            var nextShot = random.Next(notShotAtYet.Count);
            return notShotAtYet[nextShot];

        }

        private List<Coordinate> CreateShip(int length)
        {

            var horizVert = random.NextDouble();
            var coords = new List<Coordinate>();
            bool fits = false;


            while (!fits)
            {
                var tempList = new List<Coordinate>();
                if (horizVert < 0.5) // ship goes horizontal
                {
                    var FirstShipCoord = random.Next(freeSpaces.Count);
                    var coord1 = freeSpaces[FirstShipCoord];
                    tempList.Add(coord1);

                    if (coord1.Letter + length - 1 < 75)
                    {
                        for (int i = 0; i < length - 1; i++)
                        {
                            tempList.Add(new Coordinate(Convert.ToChar(coord1.Letter + 1 + i), coord1.Number));

                        }

                    }
                    else
                    {
                        for (int i = 0; i < length - 1; i++)
                        {
                            tempList.Add(new Coordinate(Convert.ToChar(coord1.Letter - 1 - i), coord1.Number));
                        }
                    }



                }
                else // goes vertical
                {
                    var FirstShipCoord = random.Next(freeSpaces.Count);
                    var coord1 = freeSpaces[FirstShipCoord];
                    tempList.Add(coord1);

                    if (coord1.Number + length - 1 < 11)
                    {
                        for (int i = 0; i < length - 1; i++)
                        {
                            tempList.Add(new Coordinate(coord1.Letter, coord1.Number + 1 + i));
                        }

                    }
                    else
                    {
                        for (int i = 0; i < length - 1; i++)
                        {
                            tempList.Add(new Coordinate(coord1.Letter, coord1.Number - 1 - i));
                        }
                    }
                }

                int counting = 0;
                foreach (var temp in tempList)
                {
                    if (freeSpaces.Contains(temp))
                    {
                        counting++;
                    }
                }

                if (counting == length)
                {
                    fits = true;

                    foreach (var item in tempList)
                    {
                        coords.Add(item);
                    }
                }

            }


            RemoveSurroundAfterCreation(coords);
            return coords;

        }


        private void RemoveSurroundingAfterSink(List<Coordinate> sunkShips) //// remove surrounding squares after ship is sunk
        {

            var toRemove = new List<List<Coordinate>>();
            foreach (var coord in sunkShips)
            {
                toRemove.Add(GetSurrounding(coord));
            }

            var removeAll = new List<Coordinate>();
            foreach (var ship in toRemove)
            {
                foreach (var coord in ship)
                {
                    removeAll.Add(coord);

                }
            }


            foreach (var remove in removeAll)
            {
                notShotAtYet.Remove(remove);
                initialShots.Remove(remove);
            }


        }



        private void RemoveSurroundAfterCreation(List<Coordinate> occupied)
        {

            var toRemove = new List<List<Coordinate>>();
            foreach (var coord in occupied)
            {
                toRemove.Add(GetSurrounding(coord));
            }

            var removeAll = new List<Coordinate>();
            foreach (var ship in toRemove)
            {
                foreach (var coord in ship)
                {
                    removeAll.Add(coord);

                }
            }


            foreach (var remove in removeAll)
            {
                freeSpaces.Remove(remove);
            }

        }



        private void FillFreeSpacesAndNotYetShot()
        {
            for (int i = 65; i < 75; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    char letter = Convert.ToChar(i);

                    Coordinate coord = new Coordinate(letter, j);

                    freeSpaces.Add(coord);
                    notShotAtYet.Add(coord);
                }
            }
        }

        private List<Coordinate> GetVertical(Coordinate coord)
        {
            var list = new List<Coordinate>();

            var coord1 = new Coordinate(coord.Letter, coord.Number - 1);
            var coord2 = new Coordinate(coord.Letter, coord.Number + 1);

            if (notShotAtYet.Contains(coord1))
            {
                list.Add(coord1);
            }
            if (notShotAtYet.Contains(coord2))
            {
                list.Add(coord2);
            }


            return list;
        }

        private List<Coordinate> GetHorizontal(Coordinate coord)
        {
            var list = new List<Coordinate>();
            var coord1 = new Coordinate(Convert.ToChar(coord.Letter - 1), coord.Number);
            var coord2 = new Coordinate(Convert.ToChar(coord.Letter + 1), coord.Number);

            if (notShotAtYet.Contains(coord1))
            {
                list.Add(coord1);
            }
            if (notShotAtYet.Contains(coord2))
            {
                list.Add(coord2);
            }

            return list;
        }

        private List<Coordinate> GetCross(Coordinate coord)
        {
            var list = new List<Coordinate>();

            list.Add(new Coordinate(Convert.ToChar(coord.Letter - 1), coord.Number));
            list.Add(new Coordinate(Convert.ToChar(coord.Letter + 1), coord.Number));
            list.Add(new Coordinate(coord.Letter, coord.Number - 1));
            list.Add(new Coordinate(coord.Letter, coord.Number + 1));

            var returnlist = new List<Coordinate>();
            foreach (var item in list)
            {
                if (notShotAtYet.Contains(item))
                {
                    returnlist.Add(item);
                }
            }

            return returnlist;
        }

        private List<Coordinate> GetSurrounding(Coordinate coord)
        {
            var list = new List<Coordinate>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    list.Add(new Coordinate(Convert.ToChar(coord.Letter + i), coord.Number + j));

                }
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BattleShip shipsAhoy = new BattleShip();

            int games = 0;

            var field = new char[10, 10];


            while (games < 100)
            {

                resetField(field);

                var shiplist = shipsAhoy.GetShipPlacement();

                var initialShots = shipsAhoy.initialShots;

                var coordsOfShips = new List<Coordinate>();

                var coordsOfMisses = new List<Coordinate>();

                var ship1 = shiplist[0];
                var ship2 = shiplist[1];
                var ship3 = shiplist[2];
                var ship4 = shiplist[3];
                var ship5 = shiplist[4];
                var ship6 = shiplist[5];
                var ship7 = shiplist[6]; ;
                var ship8 = shiplist[7];
                var ship9 = shiplist[8];
                var ship10 = shiplist[9];




                foreach (var ship in shiplist)
                {
                    foreach (var coord in ship)
                    {
                        coordsOfShips.Add(coord);

                    }
                }


                var sunkShips = new List<Coordinate>();


                while (coordsOfShips.Count > 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {

                            var tempCoord = new Coordinate(Convert.ToChar(j + 65), i + 1);

                            if (sunkShips.Contains(tempCoord))
                            {
                                field[i, j] = 'O';
                            }
                            else if (coordsOfShips.Contains(tempCoord))
                            {
                                field[i, j] = 'S';
                            }
                            else if (coordsOfMisses.Contains(tempCoord))
                            {
                                field[i, j] = 'x';
                            }



                        }
                    }
                    Console.WriteLine("Missed at: ");
                    foreach (var item in coordsOfMisses)
                    {
                        Console.Write($"{item.Letter}{item.Number}  ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Ships hit: ");

                    foreach (var item in sunkShips)
                    {
                        Console.Write($"{item.Letter}{item.Number}  ");
                    }
                    Console.WriteLine();
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            Console.Write(field[i, j] + " ");
                        }

                        Console.WriteLine();
                    }

                    var salvoCoord = shipsAhoy.SalvoAt();


                    if (coordsOfShips.Contains(salvoCoord))
                    {
                        coordsOfShips.Remove(salvoCoord);// remove 
                        sunkShips.Add(salvoCoord);

                        if (ship1.Contains(salvoCoord))
                        {
                            ship1.Remove(salvoCoord);
                            if (ship1.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }
                        }
                        else if (ship2.Contains(salvoCoord))
                        {
                            ship2.Remove(salvoCoord);
                            if (ship2.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }
                        }
                        else if (ship3.Contains(salvoCoord))
                        {
                            ship3.Remove(salvoCoord);
                            if (ship3.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }
                        }
                        else if (ship4.Contains(salvoCoord))
                        {
                            ship4.Remove(salvoCoord);
                            if (ship4.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }
                        }
                        else if (ship5.Contains(salvoCoord))
                        {
                            ship5.Remove(salvoCoord);
                            if (ship5.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }
                        }
                        else if (ship6.Contains(salvoCoord))
                        {
                            ship6.Remove(salvoCoord);
                            if (ship6.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }

                        }
                        else if (ship7.Contains(salvoCoord))
                        {
                            ship7.Remove(salvoCoord);
                            if (ship7.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }
                        }
                        else if (ship8.Contains(salvoCoord))
                        {
                            ship8.Remove(salvoCoord);
                            if (ship8.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }

                        }
                        else if (ship9.Contains(salvoCoord))
                        {
                            ship9.Remove(salvoCoord);
                            if (ship9.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }

                        }
                        else if (ship10.Contains(salvoCoord))
                        {
                            ship10.Remove(salvoCoord);
                            if (ship10.Count == 0)
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, true);
                            }
                            else
                            {
                                shipsAhoy.SalvoResults(salvoCoord, true, false);
                            }
                        }





                    }
                    else
                    {
                        shipsAhoy.SalvoResults(salvoCoord, false, false);
                        coordsOfMisses.Add(salvoCoord);


                    }


                    Console.WriteLine("Incoming at " + salvoCoord.Letter + salvoCoord.Number);

                    Console.ReadLine();
                    Console.Clear();

                }
                games++;
                /*if(games%10 == 0)
                    {
                    Console.WriteLine("Battle is over");
                        Console.WriteLine("games played " + games);
                        Console.ReadLine();
                    }
                */
                Console.WriteLine("Battle is over");
                Console.WriteLine("games played " + games);

                Console.ReadLine();

            }
            Console.WriteLine("Battle is over");
            Console.WriteLine("Number of games played: " + games);
            Console.ReadLine();
        }

        public static void resetField(char[,] field)
        {
            int beginChar = 65;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    field[i, j] = '.';


                }
                beginChar++;
            }
        }
    }
}

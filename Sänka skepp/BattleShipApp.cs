
namespace Sänka_skepp
{
    internal class BattleShipApp
    {
        public RoundHandler Board;

        public BattleShipApp(RoundHandler board)
        {
            Board = board;
        }


        internal void TestGuess(int x, int y)
        {
            if (Board.boardPosisions[x, y] != ' ')
            {
                Console.WriteLine("Du har redan skjutut här. Försök att skjuta på en annan position. Tryck på valfri tangent för att fortsätta");
                Console.ReadKey();
                PrintBoard();
                return;
            }

            Board.RoundCounter++;
            Board.UpdateBoard(x, y);
            PrintBoard();
        }


        internal void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine("\n              A   B   C   D   E   F   G   H   I   J");
            for (int j = 0; j < 21; j++)
            {
                if (j % 19 == 0 && j != 0)
                {
                    Console.Write($"         10 ");
                }
                else if (j % 2 == 1)
                {
                    Console.Write($"          {1 + j / 2} ");
                }
                else
                {
                    Console.Write($"            ");

                }
                for (int i = 0; i < 41; i++)
                {
                    if (j % 2 == 0)
                    {
                        if (i % 4 == 0)
                            Console.Write('+');
                        else if (i % 2 == 0)
                            Console.Write("-");
                        else
                            Console.Write(" ");
                    }
                    else if (j % 2 == 1)
                    {
                        if (i % 4 == 0)
                            Console.Write('|');
                        else if (i % 2 == 0)
                        {
                            PrintPosition((i - 2) / 4, (j - 1) / 2);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine();
            }
            GetGuess();
        }

        private void PrintPosition(int x, int y)
        {
            switch (Board.boardPosisions[x, y])
            {
                case ' ':
                    Console.Write(' ');
                    break;
                case 'O':
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write('O');
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case '*':
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('*');
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 'X':
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('X');
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
            }
        }

        private void GetGuess()
        {
            Console.WriteLine($"\nDu har skjutit {Board.RoundCounter} gånger och antalet skepp som seglar är {Board.ShipList.Count}");
            if (Board.ShipList.Count == 0)
            {
                Console.WriteLine("Grattis! Nu är alla skepp sänkta");
                Console.ReadKey();
                return;
            }

            Console.Write("\nVilken kolumn (A-J) vill du sikta på?: ");
            String guessColumn = Console.ReadLine() ?? "";
            if (guessColumn.Length < 1)
            {
                GetGuess();
                return;
            }

            int xpos = char.ToUpper(guessColumn[0]) - 64 - 1;
            Console.Write("\nVilken rad (1-10) vill du sikta på?: ");
            String guessRow = Console.ReadLine() ?? "";
            int ypos = Convert.ToInt32(guessRow) - 1;


            if (-1 < xpos && xpos < 10 && -1 < ypos && ypos < 10)
            {
                TestGuess(xpos, ypos);
            }
            else
            {
                GetGuess();
                return;
            }

        }
    }
}

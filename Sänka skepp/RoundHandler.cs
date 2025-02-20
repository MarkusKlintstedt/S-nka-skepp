
namespace Sänka_skepp
{
    internal class RoundHandler
    {
        internal char[,] boardPosisions = new char[10, 10];
        public int RoundCounter = 0;
        public BattleShipApp battleShipApp;
        internal List<Ship> ShipList = new List<Ship>();



        public RoundHandler()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    boardPosisions[i, j] = ' ';
                }
            }
            battleShipApp = new BattleShipApp(this);

            AddNewShip(5);
            AddNewShip(5);
            AddNewShip(5);
        }


        internal void StartGame()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            battleShipApp.PrintBoard();
        }

        internal void AddNewShip(int shipLength)
        {
            bool added = false;
            Ship ship;

            while (!added)
            {
                ship = new Ship(this, shipLength);
                added = ship.Sailing;
                if (added)
                    ShipList.Add(ship);
            }
        }



        internal bool IsShip(int x, int y)
        {
            if (ShipList.Count == 0)
                return false;
            foreach (Ship s in ShipList)
            {
                foreach (var p in s.ShipCoordinates)
                {
                    if (p.x == x && p.y == y)
                        return true;
                }
            }
            return false;
        }

        internal void UpdateBoard(int x, int y)
        {
            for (int i = 0; i < ShipList.Count; i++)
            {
                foreach (var p in ShipList[i].ShipCoordinates)
                {
                    if (p.x == x && p.y == y)
                    {
                        boardPosisions[x, y] = '*';
                        if (!ShipList[i].IsShipStillSailing())
                            ShipList.RemoveAt(i);
                        return;
                    }
                }
            }
            boardPosisions[x, y] = 'O';
        }

    }
}

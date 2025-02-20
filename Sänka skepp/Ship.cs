namespace Sänka_skepp
{
    enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    internal class Ship
    {
        internal (int x, int y)[] ShipCoordinates;
        internal bool Sailing = true;
        internal List<Direction> PossibleDirections = new List<Direction>();
        internal Direction SetDirection;
        internal RoundHandler Board;



        internal Ship(RoundHandler board, int shipLength)
        {
            ShipCoordinates = new (int, int)[shipLength];
            Board = board;
            var rand = new Random();
            ShipCoordinates[0] = ((int)rand.Next(0, 10), (int)rand.Next(0, 10));
            SetPossibleDirections(shipLength);

            if (PossibleDirections.Count > 0)
            {
                int direction = (int)rand.Next(0, PossibleDirections.Count);
                SetDirection = PossibleDirections[direction];
                switch (SetDirection)
                {
                    case Direction.Up:
                        for (int i = 1; i < shipLength; i++)
                        {
                            ShipCoordinates[i] = (ShipCoordinates[0].x, ShipCoordinates[0].y - i);
                        }
                        break;
                    case Direction.Right:
                        for (int i = 1; i < shipLength; i++)
                        {
                            ShipCoordinates[i] = (ShipCoordinates[0].x + i, ShipCoordinates[0].y);
                        }
                        break;
                    case Direction.Down:
                        for (int i = 1; i < shipLength; i++)
                        {
                            ShipCoordinates[i] = (ShipCoordinates[0].x, ShipCoordinates[0].y + i);
                        }
                        break;
                    case Direction.Left:
                        for (int i = 1; i < shipLength; i++)
                        {
                            ShipCoordinates[i] = (ShipCoordinates[0].x - i, ShipCoordinates[0].y);
                        }
                        break;
                }
            }
            else
            {
                Sailing = false;
            }
        }

        internal bool IsShipStillSailing()
        {
            for (int i = 0; i < ShipCoordinates.Length; i++)
            {
                if (Board.boardPosisions[ShipCoordinates[i].x, ShipCoordinates[i].y] == ' ')
                {
                    return true;
                }
            }
            Sailing = false;
            for (int i = 0; i < ShipCoordinates.Length; i++)
            {
                Board.boardPosisions[ShipCoordinates[i].x, ShipCoordinates[i].y] = 'X';
            }
            return false;

        }

        internal void SetPossibleDirections(int shipLength)
        {
            if (ShipCoordinates[0].x <= 9 - shipLength + 1 && !ShipCollision(shipLength, Direction.Right))
            {
                PossibleDirections.Add(Direction.Right);
            }
            if (ShipCoordinates[0].y <= 9 - shipLength + 1 && !ShipCollision(shipLength, Direction.Down))
            {
                PossibleDirections.Add(Direction.Down);
            }
            if (ShipCoordinates[0].x >= shipLength - 1 && !ShipCollision(shipLength, Direction.Left))
            {
                PossibleDirections.Add(Direction.Left);
            }
            if (ShipCoordinates[0].y >= shipLength - 1 && !ShipCollision(shipLength, Direction.Up))
            {
                PossibleDirections.Add(Direction.Up);
            }
        }

        internal bool ShipCollision(int shipLength, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (Board.IsShip(ShipCoordinates[0].x, ShipCoordinates[0].y - i))
                            return true;
                    }
                    break;
                case Direction.Right:
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (Board.IsShip(ShipCoordinates[0].x + i, ShipCoordinates[0].y))
                            return true;
                    }
                    break;
                case Direction.Down:
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (Board.IsShip(ShipCoordinates[0].x, ShipCoordinates[0].y + i))
                            return true;
                    }
                    break;
                case Direction.Left:
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (Board.IsShip(ShipCoordinates[0].x - i, ShipCoordinates[0].y))
                            return true;
                    }
                    break;
            }
            return false;
        }






    }
}

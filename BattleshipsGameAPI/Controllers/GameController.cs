using BattleshipsGameDotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipsGameAPI.Controllers
{
    public class GameController : ControllerBase
    {
        private readonly Random _random;
        public GameController()
        {
            _random = new Random();
        }
        [HttpGet]
        [Route("CreatePlayer/{playername}")]
        public ActionResult CreatePlayer(string playername)
        {
            Player player = new();
            CreateBoard(player);
            SaveBoard(player.Board, @$"{Consts.FilePath}\{playername}.json");
            return Json(player.Board);
        }
        private void CreateBoard(Player player)
        {
            player.Board = new List<Point>();                              //Basic board, with all fields set to empty
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    player.Board.Add(new Point
                    {
                        Field = Field.Empty,
                        X = x,
                        Y = y
                    });
                }
            }
        }
        [HttpGet]
        [Route("GetBoard/{playername}")]
        public ActionResult GetBoard(string playername)
        {
           return Json(LoadBoard(@$"{Consts.FilePath}\{playername}.json"));
        }
        [HttpGet]
        [Route("InsertShip/{playername}/{shipLength}/{direction}")]
        public ActionResult InsertShip(int shipLength, Direction direction, string playername, List<Point> ?board, out List<Point> newBoard)
        {
            if (board is null)
            {
                board = LoadBoard(@$"{Consts.FilePath}\{playername}.json");
            }
            var isValid = true;                                                                                         //To know if the ship can be placed or not (because of map size or other ship beeing already in here)
            var lengthX = board.Max(point => point.X);
            var lengthY = board.Max(point => point.Y);
            int startX, startY;                                                                                         //For keeping the ship 'drawing' starting point
            do
            {
                startX = new Random().Next(0, lengthX + 1);
                startY = new Random().Next(0, lengthY + 1);                                                             //Random starting coorditanes of new ship

                if (board.Single(p => p.X == startX && p.Y == startY).Field != Field.Empty)                             //This if will check, if field on which we try to place new ship is empty
                {
                    isValid = false;
                    continue;
                }

                isValid = true;
                for (var i = 0; i < shipLength; i++)
                {
                    switch (direction)
                    {
                        case Direction.Left:
                            var currentLeftPoint = board.SingleOrDefault(p => p.X == startX - i && p.Y == startY);      //Getting current 'sector' of ship on board
                            if (currentLeftPoint is null ||
                                currentLeftPoint.Field != Field.Empty ||
                                board.SingleOrDefault(p => p.X == (currentLeftPoint.X) + 1 && p.Y == currentLeftPoint.Y) is null ||
                                board.SingleOrDefault(p => p.X == (currentLeftPoint.X) - 1 && p.Y == currentLeftPoint.Y) is null ||
                                board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y + 1) is null ||
                                board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y - 1) is null ||
                                board.SingleOrDefault(p => p.X == (currentLeftPoint.X) + 1 && p.Y == currentLeftPoint.Y).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == (currentLeftPoint.X) - 1 && p.Y == currentLeftPoint.Y).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y + 1).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y - 1).Field == Field.ShipPlaced)

                            {
                                isValid = false;
                            }
                            break;
                        case Direction.Right:
                            var currentRightPoint = board.SingleOrDefault(p => p.X == startX + i && p.Y == startY);
                            if (currentRightPoint is null ||
                                currentRightPoint.Field != Field.Empty ||
                                board.SingleOrDefault(p => p.X == (currentRightPoint.X) + 1 && p.Y == currentRightPoint.Y) is null ||
                                board.SingleOrDefault(p => p.X == (currentRightPoint.X) - 1 && p.Y == currentRightPoint.Y) is null ||
                                board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y + 1) is null ||
                                board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y - 1) is null ||
                                board.SingleOrDefault(p => p.X == (currentRightPoint.X) + 1 && p.Y == currentRightPoint.Y).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == (currentRightPoint.X) - 1 && p.Y == currentRightPoint.Y).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y + 1).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y - 1).Field == Field.ShipPlaced)
                            {
                                isValid = false;
                            }
                            break;
                        case Direction.Up:
                            var currentUpPoint = board.SingleOrDefault(p => p.X == startX && p.Y == startY + i);
                            if (currentUpPoint is null || currentUpPoint.Field != Field.Empty ||
                                board.SingleOrDefault(p => p.X == currentUpPoint.X + 1 && p.Y == (currentUpPoint.Y)) is null ||
                                board.SingleOrDefault(p => p.X == currentUpPoint.X - 1 && p.Y == (currentUpPoint.Y)) is null ||
                                board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) + 1) is null ||
                                board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) - 1) is null ||
                                board.SingleOrDefault(p => p.X == currentUpPoint.X + 1 && p.Y == (currentUpPoint.Y)).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentUpPoint.X - 1 && p.Y == (currentUpPoint.Y)).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) + 1).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) - 1).Field == Field.ShipPlaced)
                            {
                                isValid = false;
                            }
                            break;
                        case Direction.Down:
                            var currentDownPoint = board.SingleOrDefault(p => p.X == startX && p.Y == startY - i);
                            if (currentDownPoint is null || currentDownPoint.Field != Field.Empty ||
                                board.SingleOrDefault(p => p.X == currentDownPoint.X + 1 && p.Y == (currentDownPoint.Y)) is null ||
                                board.SingleOrDefault(p => p.X == currentDownPoint.X - 1 && p.Y == (currentDownPoint.Y)) is null ||
                                board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) + 1) is null ||
                                board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) - 1) is null ||
                                board.SingleOrDefault(p => p.X == currentDownPoint.X + 1 && p.Y == (currentDownPoint.Y)).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentDownPoint.X - 1 && p.Y == (currentDownPoint.Y)).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) + 1).Field == Field.ShipPlaced ||
                                board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) - 1).Field == Field.ShipPlaced)
                            {
                                isValid = false;
                            }
                            break;
                    }
                }

            } while (!isValid);

            for (var i = 0; i < shipLength; i++)                                                    //Actual ship insertion to the board
            {
                switch (direction)
                {
                    case Direction.Left:
                        var pointLeft = board.Single(p => p.X == startX - i && p.Y == startY);
                        pointLeft.Field = Field.ShipPlaced;
                        break;
                    case Direction.Right:
                        var pointRight = board.Single(p => p.X == startX + i && p.Y == startY);
                        pointRight.Field = Field.ShipPlaced;
                        break;
                    case Direction.Up:
                        var pointUp = board.Single(p => p.X == startX && p.Y == startY + i);
                        pointUp.Field = Field.ShipPlaced;
                        break;
                    case Direction.Down:
                        var pointDown = board.Single(p => p.X == startX && p.Y == startY - i);
                        pointDown.Field = Field.ShipPlaced;
                        break;
                }
            }

            newBoard = board;
            return Json(newBoard);
        }
        [HttpGet]
        [Route("InsertStartingShips/{playerName}")]
        public ActionResult InsertStartingShips(string playerName)
        {
            InsertShip(5, (Direction)_random.Next(0, 4), playerName, null, out var board);
            InsertShip(4, (Direction)_random.Next(0, 4), playerName, board, out board);
            InsertShip(3, (Direction)_random.Next(0, 4), playerName, board, out board);
            InsertShip(2, (Direction)_random.Next(0, 4), playerName, board, out board);
            InsertShip(2, (Direction)_random.Next(0, 4), playerName, board, out board);
            InsertShip(1, (Direction)_random.Next(0, 4), playerName, board, out board);
            InsertShip(1, (Direction)_random.Next(0, 4), playerName, board, out board);
            SaveBoard(board, @$"{Consts.FilePath}\{playerName}.json");
            return Json(board);
        }
        [HttpGet]
        [Route("Fire/{shooterName}/{targetName}")]
        public ActionResult Fire(string shooterName, string targetName)
        {
            var shooter = LoadBoard(@$"{Consts.FilePath}\{shooterName}.json");
            var target = LoadBoard(@$"{Consts.FilePath}\{targetName}.json");

            //if (shooter.Score < Consts.MaxScore)
            for(int i = 0; i < 2; i++)                                                                                    //TEMP SOLUTION!!!!!
            {
                int x, y;
                do
                {
                    x = new Random().Next(0, 10);
                    y = new Random().Next(0, 10);
                } while (!target.Any(p => p.X == x && p.Y == y && p.Field != Field.Miss && p.Field != Field.ShipHit));      //because 'AI' knows previous shots 

                var aimedPoint = target.First(p => p.X == x && p.Y >= y);                                                   //actual shooting point
                if (aimedPoint.Field != Field.ShipHit && aimedPoint.Field != Field.Miss)
                {
                    if (aimedPoint.Field == Field.ShipPlaced)
                    {
                        aimedPoint.Field = Field.ShipHit;
                        //shooter.Hits.Add(aimedPoint);
                        //shooter.Score++;
                    }
                    else
                    {
                        aimedPoint.Field = Field.Miss;
                       // shooter.Hits.Add(aimedPoint);
                    }
                }
            }
            SaveBoard(shooter, @$"{Consts.FilePath}\{shooterName}.json");
            SaveBoard(target, @$"{Consts.FilePath}\{shooterName}.json");
            return Json(shooter);
        }
    }
}

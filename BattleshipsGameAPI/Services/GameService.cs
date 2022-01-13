using BattleshipsGameDotNET.Models;


namespace BattleshipsGameAPI.Services
{
    public interface IGameService
    {
        Task<Player> CreatePlayerAsync(string playername, CancellationToken cancellationToken);
        Task<Player> GetPlayerAsync(string playerId, CancellationToken cancellationToken);
        Task<Player> InsertShip(int shipLength, Direction direction, string playerId, CancellationToken cancellationToken);
        Task<List<Point>> Fire(string shooterId, string targetId, CancellationToken cancellationToken);
    }

    internal class GameService : IGameService
    {
        private readonly IPlayerRepository playerRepository;

        public GameService(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task<Player> CreatePlayerAsync(string playername, CancellationToken cancellationToken)
        {
            Player player = new();
            CreateBoard(player);
            await playerRepository.SavePlayerAsync(player, cancellationToken);

            return player;
        }

        public async Task<Player> GetPlayerAsync(string playerId, CancellationToken cancellationToken) =>
            await playerRepository.LoadPlayerAsync(playerId, cancellationToken);

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

        public async Task<Player> InsertShip(int shipLength, Direction direction, string playerId, CancellationToken cancellationToken)
        {
            var player = await playerRepository.LoadPlayerAsync(playerId, cancellationToken);
            var isValid = true;                                                                                         //To know if the ship can be placed or not (because of map size or other ship beeing already in here)
            var lengthX = player.Board.Max(point => point.X);
            var lengthY = player.Board.Max(point => point.Y);
            int startX, startY;                                                                                         //For keeping the ship 'drawing' starting point
            do
            {
                startX = new Random().Next(0, lengthX + 1);
                startY = new Random().Next(0, lengthY + 1);                                                             //Random starting coorditanes of new ship

                if (player.Board.Single(p => p.X == startX && p.Y == startY).Field != Field.Empty)                             //This if will check, if field on which we try to place new ship is empty
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
                            var currentLeftPoint = player.Board.SingleOrDefault(p => p.X == startX - i && p.Y == startY);      //Getting current 'sector' of ship on board
                            if (currentLeftPoint is null ||
                                currentLeftPoint.Field != Field.Empty ||
                                player.Board.SingleOrDefault(p => p.X == (currentLeftPoint.X) + 1 && p.Y == currentLeftPoint.Y) is null ||
                                player.Board.SingleOrDefault(p => p.X == (currentLeftPoint.X) - 1 && p.Y == currentLeftPoint.Y) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y + 1) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y - 1) is null ||
                                player.Board.SingleOrDefault(p => p.X == (currentLeftPoint.X) + 1 && p.Y == currentLeftPoint.Y).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == (currentLeftPoint.X) - 1 && p.Y == currentLeftPoint.Y).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y + 1).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentLeftPoint.X && p.Y == currentLeftPoint.Y - 1).Field == Field.ShipPlaced)

                            {
                                isValid = false;
                            }
                            break;
                        case Direction.Right:
                            var currentRightPoint = player.Board.SingleOrDefault(p => p.X == startX + i && p.Y == startY);
                            if (currentRightPoint is null ||
                                currentRightPoint.Field != Field.Empty ||
                                player.Board.SingleOrDefault(p => p.X == (currentRightPoint.X) + 1 && p.Y == currentRightPoint.Y) is null ||
                                player.Board.SingleOrDefault(p => p.X == (currentRightPoint.X) - 1 && p.Y == currentRightPoint.Y) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y + 1) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y - 1) is null ||
                                player.Board.SingleOrDefault(p => p.X == (currentRightPoint.X) + 1 && p.Y == currentRightPoint.Y).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == (currentRightPoint.X) - 1 && p.Y == currentRightPoint.Y).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y + 1).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentRightPoint.X && p.Y == currentRightPoint.Y - 1).Field == Field.ShipPlaced)
                            {
                                isValid = false;
                            }
                            break;
                        case Direction.Up:
                            var currentUpPoint = player.Board.SingleOrDefault(p => p.X == startX && p.Y == startY + i);
                            if (currentUpPoint is null || currentUpPoint.Field != Field.Empty ||
                                player.Board.SingleOrDefault(p => p.X == currentUpPoint.X + 1 && p.Y == (currentUpPoint.Y)) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentUpPoint.X - 1 && p.Y == (currentUpPoint.Y)) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) + 1) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) - 1) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentUpPoint.X + 1 && p.Y == (currentUpPoint.Y)).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentUpPoint.X - 1 && p.Y == (currentUpPoint.Y)).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) + 1).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentUpPoint.X && p.Y == (currentUpPoint.Y) - 1).Field == Field.ShipPlaced)
                            {
                                isValid = false;
                            }
                            break;
                        case Direction.Down:
                            var currentDownPoint = player.Board.SingleOrDefault(p => p.X == startX && p.Y == startY - i);
                            if (currentDownPoint is null || currentDownPoint.Field != Field.Empty ||
                                player.Board.SingleOrDefault(p => p.X == currentDownPoint.X + 1 && p.Y == (currentDownPoint.Y)) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentDownPoint.X - 1 && p.Y == (currentDownPoint.Y)) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) + 1) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) - 1) is null ||
                                player.Board.SingleOrDefault(p => p.X == currentDownPoint.X + 1 && p.Y == (currentDownPoint.Y)).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentDownPoint.X - 1 && p.Y == (currentDownPoint.Y)).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) + 1).Field == Field.ShipPlaced ||
                                player.Board.SingleOrDefault(p => p.X == currentDownPoint.X && p.Y == (currentDownPoint.Y) - 1).Field == Field.ShipPlaced)
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
                        var pointLeft = player.Board.Single(p => p.X == startX - i && p.Y == startY);
                        pointLeft.Field = Field.ShipPlaced;
                        break;
                    case Direction.Right:
                        var pointRight = player.Board.Single(p => p.X == startX + i && p.Y == startY);
                        pointRight.Field = Field.ShipPlaced;
                        break;
                    case Direction.Up:
                        var pointUp = player.Board.Single(p => p.X == startX && p.Y == startY + i);
                        pointUp.Field = Field.ShipPlaced;
                        break;
                    case Direction.Down:
                        var pointDown = player.Board.Single(p => p.X == startX && p.Y == startY - i);
                        pointDown.Field = Field.ShipPlaced;
                        break;
                }
            }
            await playerRepository.SavePlayerAsync(player, cancellationToken);
            return player;
        }

        public async Task<List<Point>> Fire(string shooterId, string targetId, CancellationToken cancellationToken)
        {
            var target = await playerRepository.LoadPlayerAsync(targetId, cancellationToken);

            if (target.Board.Any(t => t.Field == Field.ShipPlaced))
                {
                    int x, y;
                    do
                    {
                        x = new Random().Next(0, 10);
                        y = new Random().Next(0, 10);
                    } while (!target.Board.Any(p => p.X == x && p.Y == y && p.Field != Field.Miss && p.Field != Field.ShipHit));      //because 'AI' knows previous shots 

                    var aimedPoint = target.Board.First(p => p.X == x && p.Y >= y);                                                   //actual shooting point
                    if (aimedPoint.Field != Field.ShipHit && aimedPoint.Field != Field.Miss)
                    {
                        if (aimedPoint.Field == Field.ShipPlaced)
                        {
                            aimedPoint.Field = Field.ShipHit;
                        }
                        else
                        {
                            aimedPoint.Field = Field.Miss;
                        }
                    }
                    await playerRepository.SavePlayerAsync(target, cancellationToken);
                    return target.Board;
                }
                else
                {
                    return new List<Point>(); //Content($"{shooterId} won!");        //TODO
                }
            }
        }
    }

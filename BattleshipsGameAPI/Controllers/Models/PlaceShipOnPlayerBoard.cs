using BattleshipsGameDotNET.Models;

namespace BattleshipsGameAPI.Controllers.Models
{
    record PlaceShipOnPlayerBoardRequest(int shipLength, Direction direction, string playerId);
    record PlaceShipOnPlayerBoardResponse(List<Point> Board);
}

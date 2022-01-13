using BattleshipsGameDotNET.Models;

namespace BattleshipsGameAPI.Controllers.Models
{
    public record PlaceShipOnPlayerBoardRequest(int shipLength, Direction direction, string playerId);
    public record PlaceShipOnPlayerBoardResponse(List<Point> Board);
}

using BattleshipsGameDotNET.Models;

namespace BattleshipsGameAPI.Controllers.Models
{
  public record GetPlayerBoardResponse(List<Point> Board);
}

using BattleshipsGameDotNET.Models;

namespace BattleshipsGameAPI.Controllers.Models
{
    public record FireAtPlayerBoardRequest(string shooterId, string targetId);
    public record FireAtPlayerBoardResponse(List<Point> targetBoard);
}

namespace BattleshipsGameAPI.Controllers.Models
{
    public class CreatePlayerBoard
    {
        public record CreatePlayerBoardRequest(string Playername);
        public record CreatePlayerBoardResponse(string PlayerId);
    }
}

namespace BattleshipsGameAPI.Controllers.Models
{
    public record CreatePlayerBoardRequest(string Playername);
    public record CreatePlayerBoardResponse(string PlayerId);

}

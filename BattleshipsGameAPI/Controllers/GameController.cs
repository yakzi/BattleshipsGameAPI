using BattleshipsGameAPI.Controllers.Models;
using BattleshipsGameAPI.Services;
using BattleshipsGameDotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipsGameAPI.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
        }

        [HttpPost]
        [Route("CreatePlayerBoard")]
        public async Task<ActionResult> CreatePlayerBoard(CreatePlayerBoardRequest createPlayerBoard, CancellationToken cancellationToken)
        {
            try
            {
                var player = await gameService.CreatePlayerAsync(createPlayerBoard.Playername, cancellationToken);
                return Created(Url.RouteUrl(nameof(GetPlayerBoard), new { playerId = player.Id }), new CreatePlayerBoardResponse(player.Id));
            }
            catch (Exception exc)
            {
                return BadRequest(exc);
            }
        }

        [HttpGet]
        [Route("Players/{playerId}/Board", Name = nameof(GetPlayerBoard))]
        public async Task<ActionResult> GetPlayerBoard(string playerId, CancellationToken cancellationToken)
        {
            var player = await gameService.GetPlayerAsync(playerId, cancellationToken);

            if (player is null)
                return NotFound();

            return Ok(new GetPlayerBoardResponse(player.Board));
        }

        [HttpPost]
        [Route("PlaceShipOnPlayerBoard/{shipLength}/{direction}/{playerId}", Name = nameof(PlaceShipOnPlayerBoard))]
        public async Task<ActionResult> PlaceShipOnPlayerBoard(PlaceShipOnPlayerBoardRequest placeShipOnPlayerBoardRequest, CancellationToken cancellationToken)
        {
            var insertShip = await gameService.InsertShip(placeShipOnPlayerBoardRequest.shipLength, placeShipOnPlayerBoardRequest.direction, placeShipOnPlayerBoardRequest.playerId, cancellationToken);
            return Ok(new PlaceShipOnPlayerBoardResponse(insertShip.Board));
        }
    }
}

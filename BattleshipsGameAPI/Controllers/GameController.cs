using BattleshipsGameDotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipsGameAPI.Controllers
{
    public class GameController : ControllerBase
    {
        [HttpGet]
        [Route("CreatePlayer/{name}")]
        public void CreatePlayer(string playername)
        {
            Player player = new();
            CreateBoard(player);
            SaveBoard(player.Board, @$"C:\Users\jakub\Desktop\TET\{playername}.json");
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
        [HttpPost]
        [Route("GetBoard/{playername}")]
        public List<Point> GetBoard(string playername)
        {
           return LoadBoard(@$"C:\Users\jakub\Desktop\TET\{playername}.json");
        }
    }
}

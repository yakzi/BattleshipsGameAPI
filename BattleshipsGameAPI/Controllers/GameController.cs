using BattleshipsGameDotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipsGameAPI.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        [Route("CreatePlayer")]
        public void CreatePlayer(string Name)
        {
            Player player = new Player();
            CreateBoard(player);
            player.SaveBoard(player.Board, @$"C:\Users\jakub\Desktop\TET\test{Name}.json");
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
        [Route("GetBoard/{id}")]
        public List<Point> GetBoard(Player player)
        {
           return player.LoadBoard(@"C:\Users\jakub\Desktop\TET\test0.json");
        }
    }
}

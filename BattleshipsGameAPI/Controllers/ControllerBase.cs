using BattleshipsGameDotNET.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BattleshipsGameAPI.Controllers
{
    public class ControllerBase : Controller
    {
        public List<Point> LoadBoard(string path)
        {
            //TODO IF FILE NOT EXISTS 
            return JsonConvert.DeserializeObject<List<Point>>(System.IO.File.ReadAllText(path));
        }
        public void SaveBoard(List<Point> board, string path)
        {
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(board, Formatting.Indented));
        }
    }
}
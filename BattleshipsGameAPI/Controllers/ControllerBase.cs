using BattleshipsGameDotNET.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BattleshipsGameAPI.Controllers
{
    public class ControllerBase : Controller
    {
        public List<Point> LoadBoard(string Path)
        {
            return JsonConvert.DeserializeObject<List<Point>>(System.IO.File.ReadAllText(Path));
        }
        public void SaveBoard(List<Point> Board, string path)
        {
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(Board));
        }
    }
}
using Newtonsoft.Json;

namespace BattleshipsGameDotNET.Models
{
    public class Player
    {
        public int Score;
        public List<Point> Hits = new List<Point>();
        public List<Point> Board { get; set; }
        public List<Point> LoadBoard(string Path)
        {
            return JsonConvert.DeserializeObject<List<Point>>(File.ReadAllText(Path));
        }
        public void SaveBoard(List<Point> Board, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(Board));
        }
    }
}

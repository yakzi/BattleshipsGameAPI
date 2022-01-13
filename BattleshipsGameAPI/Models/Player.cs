namespace BattleshipsGameDotNET.Models
{
    public class Player
    {
        public Player()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; init; }
        public List<Point> Board { get; set; }
    }
}

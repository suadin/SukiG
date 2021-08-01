
namespace SukiG.Shared.Model
{
    public class Achivement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Difficulty Difficulty { get; set; }
    }

    public enum Difficulty
    {
        Trivial, Easy, Medium, Hard, Impossible
    }
}

namespace CampusLoveDarwinDaniel.Modules.Interactions.Domain.Entities
{
    public class Interaction
    {
        public int InteractionId { get; set; }
        public int UserFromId { get; set; }
        public int UserToId { get; set; }
        public string Type { get; set; } = null!; // Like, SuperLike, Dislike
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}

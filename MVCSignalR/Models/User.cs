namespace MVCSignalR.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string ConnectionId { get; set; } = null;
    }
}

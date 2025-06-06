namespace ApplicationCore.Responses
{
    public class UserDetailsResponse
    {
        public long UserId { set; get; }
        public string FristName { set; get; } = default!;
        public string LastName { set; get; } = default!;
        public string Email { set; get; } = default!;
        public int StateId { set; get; }
        public int city { set; get; }
        public long PinCode { set; get; }

    }

    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public required string Email { get; set; }
        public int? Age { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

namespace ApplicationCore.Models
{
    public class CustomerRegisterModel
    {
        public required string FristName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

    }
}

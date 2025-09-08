namespace ApplicationCore.Models
{
    public class CustomerLoginResponse
    {
        public required string CustomerId { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public int PinNumber { get; set; }
        public int OTP { get; set; }
        public string FingerPrintCode { get; set; }

    }
}

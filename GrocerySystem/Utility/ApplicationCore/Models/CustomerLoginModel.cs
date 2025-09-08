namespace ApplicationCore.Models
{
    public class CustomerLoginModel
    {
        public required LoginMode LoginMode { get; set; }
        public string? Email { set; get; } = default!;
        public string? PhoneNumber { set; get; } = default!;
        public string? Password { set; get; }= default!;
        public string? FingerPrintCode { set; get; } = default!;
        public string? IMINumber { set; get; } = default!;
        public int? PinNumber { set; get; } = default!;
        public int? OTP { set; get; } = default!;


    }
    public enum LoginMode
    {
        LoginWithOtp,
        Password,
        PinNumber,
        FingerPrint
    }

}

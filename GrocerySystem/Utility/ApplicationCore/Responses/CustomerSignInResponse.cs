namespace ApplicationCore.Responses
{
    public class CustomerSignInResponse : ApplicationResponse
    {
        public CustomerSignInResponse() : base()
        {

        }
        public string JWTToken { set; get; } = default!;
        public string Email { set; get; } = default!;
        public AuthenticationStatus AuthenticationStatus { set; get; } = AuthenticationStatus.None;


    }

    public enum AuthenticationStatus
    {
        None = 0,
        Success = 1,
        Fail = 2,
        InvalidCredintials = 3,
        InvalidPin=4,
        InvalidOtp=5,



           

    }
}

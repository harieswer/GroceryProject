using System.Net;

namespace ApplicationCore.Responses
{
    public class ApplicationResponse
    {
        public ApplicationResponse()
        {
            Errors = [];
        }
        public List<string> Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }


    }
}

namespace CelanArchitecture.Core.ServiceWrapper
{
    public class ServiceResponse
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }
        public object Content { get; set; } = null;
        public string AccessToken { get; set; } = null;
    }
}

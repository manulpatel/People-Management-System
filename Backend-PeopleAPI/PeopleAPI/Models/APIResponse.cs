namespace PeopleAPI.Models
{
    public class APIResponse
    {
        //General format of all API requests

        public string Code { get; set; }
        public string Message { get; set; } 
        public object? ResponseData { get; set; }

    }
    public enum ResponseType
    {
        Success,
        NotFound,
        Failure
    }
}

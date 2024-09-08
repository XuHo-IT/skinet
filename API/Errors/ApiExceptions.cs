using System;

namespace API.Errors;

public class ApiExceptions : ApiResponse
{
    public ApiExceptions(int statusCode, string message = null, string details =null) : base(statusCode, message)
    {
       Datails = details;
    }
    public string Datails { get; set; }
}

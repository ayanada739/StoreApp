﻿namespace Store.G04.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch
            {
                400 => " a bad request, you have made",
                401 => " Authorized, you are not",
                404 => "Resiurce was not found",
                500 => "Server Error",
                _ => null
            };

            return message;
        }
    }
}

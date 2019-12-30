using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model
{
    public class AuthenticationResult
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }

        public string EmailConfirmationToken { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }

    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Error { get; set; }
    }
    public class EmailVerificationRequest 
    {
        public string Token { get; set; }
        public string Error { get; set; }
    }
    
    //TODO: remove commented code
    ////public class AuthFailedResponse
    ////{
    ////    public IEnumerable<string> Errors { get; set; }
    ////}
}

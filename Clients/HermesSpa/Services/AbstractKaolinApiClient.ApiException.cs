using System;

namespace HermesSpa.Services
{
    public partial class AbstractKaolinApiClient
    {
        public class ApiException : Exception
        {
            private readonly ApiErrorResponseResult _error;

            public ApiErrorResponseResult Error => _error;

            public ApiException(ApiErrorResponseResult error)
            {
                _error = error;
            }
        }
    }
}

namespace ECommerce.SharedLibirary.CommonResult
{
    public enum ErrorType : byte
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Unauthorized = 3,
        Forbidden = 4,
        InternalServerError = 5,
        BadRequest = 6 ,
        InvalidCredentials = 7,
    }

    public class Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType type { get; }


        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            this.type = type;
        }


        //centralize error handling and representation across the application


        //static factory methods for creating different types of errors

        #region Static Factory Methodes 
        public static Error Validation(string code, string description)
        {
            return new Error(code, description, ErrorType.Validation);
        }

        public static Error NotFound(string code, string description)
        {
            return new Error(code, description, ErrorType.NotFound);
        }

        public static Error Unauthorized(string code, string description)
        {
            return new Error(code, description, ErrorType.Unauthorized);
        }

        public static Error Forbidden(string code, string description)
        {
            return new Error(code, description, ErrorType.Forbidden);
        }

        public static Error InternalServerError(string code, string description)
        {
            return new Error(code, description, ErrorType.InternalServerError);
        }

        public static Error BadRequest(string code, string description)
        {
            return new Error(code, description, ErrorType.BadRequest);
        }

        public static Error InvalidCredentials(string code, string description)
        {
            return new Error(code, description, ErrorType.InvalidCredentials);  
        }
        public static Error Failure(string code = "General.Failure", string description = "A General Failure Occurred ")
        {
            return new Error(code, description, ErrorType.Failure);
        }


        #endregion
    }
}

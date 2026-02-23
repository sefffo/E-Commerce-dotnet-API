namespace ECommerce.SharedLibirary.CommonResult
{
    public class Result
    {
        //isSuccess 
        //isFailure
        //ErrorMessage


        protected readonly List<Error> _errors = [];
        public bool isSuccess => _errors.Count == 0;
        public bool isFailure => !isSuccess;
        public IReadOnlyCollection<Error> Errors => _errors;

        //3 ways of creating the result 
        //OK -> success result with no errors
        protected Result()
        {

        }
        public static Result Ok() => new Result();
        //fail with error message
        protected Result(Error error)
        {
            _errors.Add(error);
        }
        public static Result Fail(Error error) => new Result(error);
        //fail with Multiple   errors 
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }
        public static Result Fail(List<Error> errors) => new Result(errors);
    }

    public class Result<T> : Result
    {
        private readonly T Value;

        public T value
        {
            get
            {
                if (isFailure)
                    throw new InvalidOperationException("Cannot access the value of a failed result."); //system error 
                return Value;
            }
        }
        private Result(T value) : base()
        {
            Value = value;

        }
        public static Result<T> Ok(T value) => new Result<T>(value);

        private Result(Error error) : base(error)
        {
            Value = default!;
        }
        public static new Result<T> Fail(Error error) => new Result<T>(error);
        private Result(List<Error> errors) : base(errors)
        {
            Value = default!;
        }

        public new static Result<T> Fail(List<Error> errors) => new Result<T>(errors);


        public static implicit operator Result<T>(T value) => Ok(value);
        public static implicit operator Result<T>(Error error) => Fail(error);
        public static implicit operator Result<T>(List<Error> errors) => Fail(errors);



    }
}

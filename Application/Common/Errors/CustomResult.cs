using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors
{
    public class CustomResult<T>
    {
        private readonly T? _value;
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public T value
        {
            get
            {
                if (IsFailure)
                    throw new InvalidOperationException("there is not value for error");
                return _value;

            }
            private init => _value = value;
        }

        public CustomErrors? Error { get; }
        private CustomResult(T Value)
        {
            value = Value;
            IsSuccess = true;
            Error = CustomErrors.none;

        }

        private CustomResult(CustomErrors errors)
        {
            if (errors == CustomErrors.none)
            {
                throw new ArgumentException("InvalidError");
            }

            IsSuccess = false;
            Error = errors;
        }

        public static CustomResult<T> Success(T value) => new CustomResult<T>(value);
        public static CustomResult<T> Failure(CustomErrors error) => new CustomResult<T>(error);



    }
}

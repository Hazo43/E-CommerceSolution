using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Shared.CommonResult
{
    public class Result
    {
        // Errors [ Code - Description - ErrorType ]
        protected readonly List<Error> _errors = [];
        // IsSuccess 
        public bool IsSuccess => _errors.Count == 0;  // true =>  Count == 0  في حاله ان ال
        // IsFailure
        public bool IsFailure => !IsSuccess;  // false يبقي هيه true ب IsSuccess لو ال IsSuccess هيه عكس ال 
        public IReadOnlyList<Error> Errors => _errors;

        // OK - Success 
        protected Result()
        {
            
        }
        // Faild with Error ايرور واحد
        protected Result(Error error)
        {
            _errors.Add(error);
        }

        // Faild with Errors اكتر من ايرور 
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }

        public static Result Ok() => new Result();
        public static Result Fail(Error error) => new Result( error);
        public static Result Fail(List<Error> errors) => new Result( errors);

    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException(" Can Not Access The Value Of Failed Result");

        // Ok - Success 
        public Result( TValue value) : base()
        {
            _value = value;
        }
        // Faild - Faild With Error 
        public Result( Error error) : base(error) 
        {
            _value = default!;
        }
        // Faild - Faild With Errors 
        public Result(List<Error> errors) : base(errors)
        {
            _value = default!;
        }

        public static Result<TValue> OK(TValue value) => new Result<TValue>(value);
        public static new Result<TValue> Fail (Error error) => new Result<TValue>(error);
        public static new Result<TValue> Fail (List<Error> errors) => new Result<TValue>(errors);

        public static implicit operator Result<TValue>(TValue value) => OK(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);
        public static implicit operator Result<TValue>(List<Error> errors) => Fail(errors);



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public class ValidationResult<TResult> : Result<TResult>, IValidationResult 
    {
        private ValidationResult(Error[] errors)
            : base(default, false, IValidationResult.ValidationError) {
            Errors = errors;
        }

        public Error[] Errors { get; }

        public static ValidationResult<TResult> WithErrors(Error[] errors) {
            return new(errors);
        }
    }
}

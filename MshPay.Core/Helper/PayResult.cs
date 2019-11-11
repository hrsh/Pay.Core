using System.Collections.Generic;
using System.Linq;
using MshPay.Core.Config;

namespace MshPay.Core.Helper
{
    public class PayResult<T>
    {
        public bool Succeeded { get; protected set; }

        public T Result { get; protected set; }

        public static PayResult<T> Success { get; } = new PayResult<T>
        {
            Succeeded = false,
            Result = new PayResult<T>().Result
        };

        private readonly List<PayError> _errors = new List<PayError>();

        public IEnumerable<PayError> Errors => _errors;

        public static PayResult<T> Failed(params PayError[] errors)
        {
            var result = new PayResult<T> {Succeeded = false};
            if (errors != null)
                result._errors.AddRange(errors);
            return result;
        }

        public static PayResult<T> Invoke(T result, PayError[] errors = null)
        {
            var r = new PayResult<T>
            {
                Succeeded = result != null,
                Result = result
            };
            if (result == null)
                r._errors.Add(new PayError
                {
                    Code = $"{typeof(T)}",
                    Description = $"Could not find {typeof(T)} in the current context!"
                });
            if (errors != null)
                r._errors.AddRange(errors);
            return r;
        }

        public override string ToString()
        {
            return !Succeeded ? $"Failed : {string.Join(",", Errors.Select(x => x.Code).ToList())}" : "Succeeded";
        }
    }
}
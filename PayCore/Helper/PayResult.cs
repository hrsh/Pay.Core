// ***********************************************************************
// Assembly         : PayCore
// Author           : Mazdak Shojaie
// Created          : 09-24-2019
//
// Last Modified By : Mazdak Shojaie
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="PayResult.cs" company="PayCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PayCore.Config;

namespace PayCore.Helper {
    /// <summary>
    /// Class PayResult.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PayResult<T> {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PayResult{T}"/> is succeeded.
        /// </summary>
        /// <value><c>true</c> if succeeded; otherwise, <c>false</c>.</value>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public T Result { get; protected set; }

        /// <summary>
        /// Gets the this instance as success.
        /// </summary>
        /// <value>The success.</value>
        public static PayResult<T> Success { get; } = new PayResult<T> {
            Succeeded = false,
            Result = new PayResult<T> ().Result
        };

        /// <summary>
        /// The errors
        /// </summary>
        private readonly List<PayError> _errors = new List<PayError> ();

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IEnumerable<PayError> Errors => _errors;

        /// <summary>
        /// Adds the specified errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns>PayResult&lt;T&gt;.</returns>
        public static PayResult<T> Failed (params PayError[] errors) {
            var result = new PayResult<T> { Succeeded = false };
            if (errors != null)
                result._errors.AddRange (errors);
            return result;
        }

        /// <summary>
        /// Invokes the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>PayResult&lt;T&gt;.</returns>
        public static PayResult<T> Invoke (T result, PayError[] errors = null) {
            var r = new PayResult<T> {
            Succeeded = result != null,
            Result = result
            };
            if (result == null)
                r._errors.Add (new PayError {
                    Code = $"{typeof(T)}",
                        Description = $"Could not find {typeof(T)} in the current context!"
                });
            if (errors != null)
                r._errors.AddRange (errors);
            return r;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString () {
            return !Succeeded ? $"Failed : {string.Join(",", Errors.Select(x => x.Code).ToList())}" : "Succeeded";
        }
    }
}
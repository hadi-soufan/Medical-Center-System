using System;

namespace Application.Core
{
    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the result value if the operation was successful.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the error message if the operation failed.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="Result{T}"/> indicating a successful operation.
        /// </summary>
        /// <param name="value">The result value.</param>
        /// <returns>A <see cref="Result{T}"/> indicating success with the specified value.</returns>
        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };

        /// <summary>
        /// Creates a new instance of <see cref="Result{T}"/> indicating a failed operation.
        /// </summary>
        /// <param name="error">The error message.</param>
        /// <returns>A <see cref="Result{T}"/> indicating failure with the specified error message.</returns>
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace YP.Toolkit.System.Exceptions
{
	/// <summary>
	/// 	Generic exception for combining several other exceptions
	/// </summary>
	public class CombinedException : Exception
	{
		/// <summary>
		/// 	Initializes a new instance of the <see cref = "CombinedException" /> class.
		/// </summary>
		/// <param name = "message">The message.</param>
		/// <param name = "innerExceptions">The inner exceptions.</param>
		public CombinedException(string message, Exception[] innerExceptions) : base(message)
		{
			InnerExceptions = innerExceptions;
		}

		/// <summary>
		/// 	Gets the inner exceptions.
		/// </summary>
		/// <value>The inner exceptions.</value>
		public Exception[] InnerExceptions { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="innerExceptions"></param>
		/// <returns></returns>
		public static Exception Combine(string message, params Exception[] innerExceptions)
		{
		    return innerExceptions.Length == 1 ? innerExceptions[0] : new CombinedException(message, innerExceptions);
		}

	    /// <summary>
		/// Combines the specified exception.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerExceptions">The inner exceptions.</param>
		/// <returns></returns>
		public static Exception Combine(string message, IEnumerable<Exception> innerExceptions)
		{
			return Combine(message, innerExceptions.ToArray());
		}
	}
}

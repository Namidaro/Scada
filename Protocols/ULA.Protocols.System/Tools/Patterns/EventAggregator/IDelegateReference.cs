using System;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Represents a reference to a <see cref="T:System.Delegate"/>.
    /// </summary>
    public interface IDelegateReference
    {
        /// <summary>
        /// Gets the referenced <see cref="T:System.Delegate"/> object.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// A <see cref="T:System.Delegate"/> instance if the target is valid; otherwise <see langword="null"/>.
        /// </value>
        Delegate Target { get; }
    }
}
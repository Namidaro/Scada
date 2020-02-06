using System;
using System.Threading;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures
{
    /// <summary>
    /// Provides a simple, reference type wrapper for SpinLock.
    /// </summary>
    public class SpinLockClass
    {
        #region [Private fields]
        private SpinLock _spinLock; // NOTE: must *not* be readonly due to SpinLock being a mutable struct
        #endregion [Private fields]
        
        
        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of the SpinLockClass class.
        /// </summary>
        public SpinLockClass()
        {
            this._spinLock = new SpinLock();
        } 
        #endregion [Ctor's]


        /// <summary>
        /// Initializes an instance of the SpinLockClass class.
        /// </summary>
        /// <param name="enableThreadOwnerTracking">
        /// Controls whether the SpinLockClass should track
        /// thread-ownership fo the lock.
        /// </param>
        public SpinLockClass(bool enableThreadOwnerTracking)
        {
            this._spinLock = new SpinLock(enableThreadOwnerTracking);
        }
        /// <summary>
        /// Runs the specified delegate under the lock.
        /// </summary>
        /// <param name="runUnderLock">The delegate to be executed while holding the lock.</param>
        public void Execute(Action runUnderLock)
        {
            var lockTaken = false;
            try
            {
                this.Enter(ref lockTaken);
                runUnderLock();
            }
            finally
            {
                if (lockTaken) 
                    this.Exit();
            }
        }
        /// <summary>
        /// Enters the lock.
        /// </summary>
        /// <param name="lockTaken">
        /// Upon exit of the Enter method, specifies whether the lock was acquired. 
        /// The variable passed by reference must be initialized to false.
        /// </param>
        public void Enter(ref bool lockTaken)
        {
            this._spinLock.Enter(ref lockTaken);
        }
        /// <summary>
        /// Exits the SpinLock.
        /// </summary>
        public void Exit()
        {
            this._spinLock.Exit();
        }
        /// <summary>
        /// Exits the SpinLock
        /// </summary>
        /// <param name="useMemoryBarrier">
        /// A Boolean value that indicates whether a memory fence should be issued in
        /// order to immediately publish the exit operation to other threads.
        /// </param>
        public void Exit(bool useMemoryBarrier)
        {
            this._spinLock.Exit(useMemoryBarrier);
        }
    }
}

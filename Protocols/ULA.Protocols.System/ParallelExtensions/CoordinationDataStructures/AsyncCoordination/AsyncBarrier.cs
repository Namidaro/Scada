using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures.AsyncCoordination
{
    /// <summary>
    /// Provides an asynchronous barrier.
    /// </summary>
    [DebuggerDisplay("ParticipantCount={ParticipantCount}, RemainingCount={RemainingCount}")]
    public sealed class AsyncBarrier
    {
        #region [Private fields]
        /// <summary>
        /// The number of participants in the barrier.
        /// </summary>
        private readonly int _participantCount;
        /// <summary>
        /// The task used to signal completion of the current round.
        /// </summary>
        private TaskCompletionSource<object> _currentSignalTask;
        /// <summary>
        /// The number of participants remaining to arrive for this round.
        /// </summary>
        private int _remainingParticipants;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes the BarrierAsync with the specified number of participants.
        /// </summary>
        /// <param name="participantCount">The number of participants in the barrier.</param>
        public AsyncBarrier(int participantCount)
        {
            if (participantCount <= 0) throw new ArgumentOutOfRangeException("participantCount");
            this._participantCount = participantCount;

            this._remainingParticipants = participantCount;
            this._currentSignalTask = new TaskCompletionSource<object>();
        }
        #endregion [Ctor's]


        #region [Properties]
        /// <summary>
        /// Gets the participant count.
        /// </summary>
        public int ParticipantCount { get { return this._participantCount; } }
        /// <summary>
        /// Gets the number of participants still not yet arrived in this round.
        /// </summary>
        public int RemainingCount { get { return this._remainingParticipants; } } 
        #endregion [Properties]


        #region [Public members]
        /// <summary>
        /// Signals that a participant has arrived.
        /// </summary>
        /// <returns>A Task that will be signaled when the current round completes.</returns>
        public Task SignalAndWait()
        {
            var curCts = this._currentSignalTask;
#pragma warning disable 420
            if (Interlocked.Decrement(ref this._remainingParticipants) == 0)
#pragma warning restore 420
            {
                this._remainingParticipants = this._participantCount;
                this._currentSignalTask = new TaskCompletionSource<object>();
                curCts.SetResult(null);
            }
            return curCts.Task;
        }
        #endregion [Public members]
    }
}
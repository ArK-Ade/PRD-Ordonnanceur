using System;

namespace PRD_Ordonnanceur.Data
{
    /// <summary>
    /// In ordor to complete a step, an operator is needed before and after the actual operation.Structure that represents those moments
    /// </summary>
    public struct Duration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="durationBeforeOp"></param>
        /// <param name="durationAfterOp"></param>
        /// <param name="durationOp"></param>
        public Duration(TimeSpan durationBeforeOp, TimeSpan durationAfterOp, TimeSpan durationOp)
        {
            this.DurationBeforeOp = durationBeforeOp;
            this.DurationAfterOp = durationAfterOp;
            this.DurationOp = durationOp;
        }

        /// <summary>
        /// Amount of time an operator need to prepare the step
        /// </summary>
        public TimeSpan DurationBeforeOp { get; set; }

        /// <summary>
        /// Amount of time when the operator is not needed
        /// </summary>
        public TimeSpan DurationAfterOp { get; set; }

        /// <summary>
        /// Amount of time an operator need to finish the step
        /// </summary>
        public TimeSpan DurationOp { get; set; }
    }
}
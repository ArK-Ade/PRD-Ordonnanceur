using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    /// <summary>
    /// This class represents the step to do in order to complete an OF
    /// </summary>
    public class Step
    {
        private Duration duration;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Step()
        { }

        /// <summary>
        /// Confortable Constructor
        /// </summary>
        /// <param name="idStep"></param>
        /// <param name="typeMachineNeeded"></param>
        /// <param name="duration"></param>
        /// <param name="durationMaxNextStep"></param>
        /// <param name="nextStepReportable"></param>
        /// <param name="consumableUsed"></param>
        /// <param name="quantityConsumable"></param>
        public Step(double idStep, TypeMachine typeMachineNeeded, Duration duration, DateTime durationMaxNextStep, bool nextStepReportable, List<Consumable> consumableUsed, List<double> quantityConsumable)
        {
            this.Uid = idStep;
            this.TypeMachineNeeded = typeMachineNeeded;
            this.duration = duration;
            this.DurationMaxNextStep = durationMaxNextStep;
            this.NextStepReportable = nextStepReportable;
            this.ConsumableUsed = consumableUsed;
            this.QuantityConsumable = quantityConsumable;
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        public double Uid { get; set; }

        /// <summary>
        /// Represent the type of the machine
        /// </summary>
        public TypeMachine TypeMachineNeeded { get; set; }

        /// <summary>
        /// Indicate the time of the Operation
        /// </summary>
        public Duration Duration { get => duration; set => duration = value; }

        /// <summary>
        ///
        /// </summary>
        public DateTime DurationMaxNextStep { get; set; }

        /// <summary>
        /// Bool that indicate if the next step is reportable to next day
        /// </summary>
        public bool NextStepReportable { get; set; }

        /// <summary>
        /// List that contain the consumable needed
        /// </summary>
        public List<Consumable> ConsumableUsed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<double> QuantityConsumable { get; set; }

        /// <summary>
        /// Name of a step
        /// </summary>
        public string Name { get; set; }
    }
}
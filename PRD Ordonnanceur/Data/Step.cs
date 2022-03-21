﻿using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    public struct Duration
    {
        private TimeSpan durationBeforeOp;
        private TimeSpan durationAfterOp;
        private TimeSpan durationOp;

        public Duration(TimeSpan durationBeforeOp, TimeSpan durationAfterOp, TimeSpan durationOp)
        {
            this.durationBeforeOp = durationBeforeOp;
            this.durationAfterOp = durationAfterOp;
            this.durationOp = durationOp;
        }

        public TimeSpan DurationBeforeOp { get => durationBeforeOp; set => durationBeforeOp = value; }
        public TimeSpan DurationAfterOp { get => durationAfterOp; set => durationAfterOp = value; }
        public TimeSpan DurationOp { get => durationOp; set => durationOp = value; }
    }

    public class Step
    {
        private double idStep;
        private string name;
        private TypeMachine typeMachineNeeded;
        private Duration duration;
        private DateTime durationMaxNextStep;
        private bool nextStepReportable;
        private List<Consumable> consumableUsed;
        private List<double> quantityConsumable;

        public Step()
        { }

        public Step(double idStep, TypeMachine typeMachineNeeded, Duration duration, DateTime durationMaxNextStep, bool nextStepReportable, List<Consumable> consumableUsed, List<double> quantityConsumable)
        {
            this.idStep = idStep;
            this.typeMachineNeeded = typeMachineNeeded;
            this.duration = duration;
            this.durationMaxNextStep = durationMaxNextStep;
            this.nextStepReportable = nextStepReportable;
            this.ConsumableUsed = consumableUsed;
            this.QuantityConsumable = quantityConsumable;
        }

        public double IdStep { get => idStep; set => idStep = value; }
        public TypeMachine TypeMachineNeeded { get => typeMachineNeeded; set => typeMachineNeeded = value; }
        public Duration Duration { get => duration; set => duration = value; }
        public DateTime DurationMaxNextStep { get => durationMaxNextStep; set => durationMaxNextStep = value; }
        public bool NextStepReportable { get => nextStepReportable; set => nextStepReportable = value; }
        public List<Consumable> ConsumableUsed { get => consumableUsed; set => consumableUsed = value; }
        public List<double> QuantityConsumable { get => quantityConsumable; set => quantityConsumable = value; }
        public string Name { get => name; set => name = value; }
    }
}
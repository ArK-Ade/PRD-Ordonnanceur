using System;

namespace PRD_Ordonnanceur.Data
{
    public struct Duration
    {
        DateTime DurationBeforeOp;
        DateTime DurationAfterOp;
        DateTime DurationOp;
    }

    public class Step
    {
        private int idStep;

        private TypeMachine typeMachineNeeded;

        private Duration duration;

        private Operator[] setOperatorAvailable;

        private DateTime durationMaxNextStep;

        private bool nextStepReportable;

        public Step() { }

        public Step(int idStep, TypeMachine typeMachineNeeded, Duration duration, Operator[] setOperatorAvailable, DateTime durationMaxNextStep, bool nextStepReportable)
        {
            this.idStep = idStep;
            this.typeMachineNeeded = typeMachineNeeded;
            this.duration = duration;
            this.setOperatorAvailable = setOperatorAvailable;
            this.durationMaxNextStep = durationMaxNextStep;
            this.nextStepReportable = nextStepReportable;
        }

        public int IdStep { get => idStep; set => idStep = value; }
        public TypeMachine TypeMachineNeeded { get => typeMachineNeeded; set => typeMachineNeeded = value; }
        public Duration Duration { get => duration; set => duration = value; }
        public DateTime DurationMaxNextStep { get => durationMaxNextStep; set => durationMaxNextStep = value; }
        public bool NextStepReportable { get => nextStepReportable; set => nextStepReportable = value; }
        public Operator[] OperatorAvailable { get => setOperatorAvailable; set => setOperatorAvailable = value; }
    }
}

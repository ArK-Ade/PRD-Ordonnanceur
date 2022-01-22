using System;

namespace PRD_Ordonnanceur.Data
{
    class Step
    {
        private int Id { get; set; }

        private Machine.type_machine Type_machine_needed { get; set; }

        private struct Duration
        {
            DateTime DurationBeforeOp;
            DateTime DurationAfterOp;
            DateTime DurationOp;
        }

        private Operator[] Set_operatorAvailable { get; set; }

        private DateTime Duration_max_next_step { get; set; }

        private bool Next_step_reportable { get; set; }

        public Step() { }

        public Step(int id, Machine.type_machine type_machine_needed, Operator[] set_operatorAvailable, DateTime duration_max_next_step, bool next_step_reportable)
        {
            Id = id;
            this.Type_machine_needed = type_machine_needed;
            this.Set_operatorAvailable = set_operatorAvailable ?? throw new ArgumentNullException(nameof(set_operatorAvailable));
            this.Duration_max_next_step = duration_max_next_step;
            this.Next_step_reportable = next_step_reportable;
        }
    }
}

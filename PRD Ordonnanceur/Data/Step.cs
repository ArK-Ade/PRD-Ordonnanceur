using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRD_Ordonnanceur.Data
{
    class Step
    {
        public Machine.type_machine type_machine_needed;

        public DateTime duration;

        public Operator[] set_operator;

        public DateTime duration_max_next_step;

        public bool next_step_reportable;

        public Step() { }
    }
}

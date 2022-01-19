using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRD_Ordonnanceur.Data
{
    class Machine
    {
        public enum type_machine
        {
            blender,
            Mixer,
            disperser
        };

        public struct calendar
        {
            public DateTime day;
            public DateTime beginning_hour;
            public DateTime finishing_hour;
        }

        public DateTime duration_cleaning;

        public Operator[] operator_available_cleaning;


        public Machine() { }
    }
}

using CsvHelper;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PRD_Ordonnanceur.Parser
{
    /// <summary>
    /// This class allows to transform the schedules of the algorithm in csv file
    /// </summary>
    public static class DataToCsv
    {
        /// <summary>
        /// Methods that transform the Operator in a csv file
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataOperator(List<SolutionPlanning> plannings, string path)
        {
            string nameFile = "/Operator.csv";
            using StreamWriter streamWriter = new(path + nameFile);
            using CsvWriter csvWriter = new(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.WriteField("Job Start");
            csvWriter.WriteField("Job End");
            csvWriter.WriteField("Code");
            csvWriter.WriteField("ID");
            csvWriter.NextRecord();

            foreach (var planning in plannings)
            {
                foreach (var list in planning.PlanningOperator)
                {
                    csvWriter.WriteField<DateTime>((DateTime)list[1]);
                    csvWriter.WriteField<DateTime>((DateTime)list[2]);
                    csvWriter.WriteField((string)list[3]);
                    csvWriter.WriteField<int>((int)list[4]);
                    csvWriter.NextRecord();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataMachine(List<SolutionPlanning> plannings, string path)
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataConsumable(List<SolutionPlanning> plannings, string path)
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataOF(List<SolutionPlanning> plannings, string path)
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataTank(List<SolutionPlanning> plannings, string path)
        {
            // Method intentionally left empty.
        }
    }
}

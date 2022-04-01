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
            csvWriter.WriteField("ID");
            csvWriter.WriteField("Job Start");
            csvWriter.WriteField("Job End");
            csvWriter.WriteField("Code");
            
            csvWriter.NextRecord();

            foreach (var planning in plannings)
            {
                foreach (var list in planning.PlanningOperator)
                {
                    csvWriter.WriteField<int>((int)list[4]);
                    csvWriter.WriteField<DateTime>((DateTime)list[1]);
                    csvWriter.WriteField<DateTime>((DateTime)list[2]);
                    csvWriter.WriteField((string)list[3]);
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
            string nameFile = "/Machines.csv";
            using StreamWriter streamWriter = new(path + nameFile);
            using CsvWriter csvWriter = new(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.WriteField("UID Machine");
            csvWriter.WriteField("Job Start");
            csvWriter.WriteField("Job End");
            csvWriter.WriteField("UID OF");
            csvWriter.WriteField("UID Beginning Operator");
            csvWriter.WriteField("UID End Operator");
            csvWriter.NextRecord();

            foreach (var planning in plannings)
            {
                foreach (var list in planning.PlanningOperator)
                {
                    csvWriter.WriteField<int>((int)list[7]);
                    csvWriter.WriteField<DateTime>((DateTime)list[1]);
                    csvWriter.WriteField<DateTime>((DateTime)list[2]);
                    csvWriter.WriteField<int>((int)list[3]);
                    csvWriter.WriteField<int>((int)list[4]);
                    csvWriter.WriteField<int>((int)list[5]);
                    csvWriter.WriteField<int>((int)list[6]);
                    csvWriter.NextRecord();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataConsumable(List<SolutionPlanning> plannings, string path)
        {
            string nameFile = "/Consumable.csv";
            using StreamWriter streamWriter = new(path + nameFile);
            using CsvWriter csvWriter = new(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.WriteField("UID Consumable");
            csvWriter.WriteField("Quantity Used");
            csvWriter.NextRecord();

            foreach (var planning in plannings)
            {
                foreach (var list in planning.PlanningOperator)
                {
                    csvWriter.WriteField<int>((int)list[1]);
                    csvWriter.WriteField<double>((double)list[2]);
                    csvWriter.NextRecord();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataOF(List<SolutionPlanning> plannings, string path)
        {
            string nameFile = "/OFs.csv";
            using StreamWriter streamWriter = new(path + nameFile);
            using CsvWriter csvWriter = new(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.WriteField("Date");
            csvWriter.WriteField("UID Tank");
            csvWriter.WriteField("UID Machine");
            csvWriter.WriteField("");
            csvWriter.WriteField("Quantity Used");
            csvWriter.WriteField("Quantity Used");
            csvWriter.NextRecord();

            foreach (var planning in plannings)
            {
                foreach (var list in planning.PlanningOperator)
                {
                    csvWriter.WriteField<int>((int)list[1]);
                    csvWriter.WriteField<double>((double)list[2]);
                    csvWriter.NextRecord();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataTank(List<SolutionPlanning> plannings, string path)
        {
            string nameFile = "/Tanks.csv";
            using StreamWriter streamWriter = new(path + nameFile);
            using CsvWriter csvWriter = new(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.WriteField("UID Tank");
            csvWriter.WriteField("Job Start");
            csvWriter.WriteField("Job End");
            csvWriter.WriteField("UID OF");
            csvWriter.WriteField("UID Operator");
            csvWriter.NextRecord();

            foreach (var planning in plannings)
            {
                foreach (var list in planning.PlanningOperator)
                {
                    csvWriter.WriteField<int>((int)list[6]);
                    csvWriter.WriteField<DateTime>((DateTime)list[1]);
                    csvWriter.WriteField<DateTime>((DateTime)list[2]);
                    csvWriter.WriteField<int>((int)list[4]);
                    csvWriter.WriteField<int>((int)list[5]);
                    csvWriter.NextRecord();
                }
            }
        }
    }
}

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
        /// Launch all the parsing
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void LaunchParsing(List<SolutionPlanning> plannings, string path)
        {
            DataToCsv.ParsingDataOperator(plannings, path);
            DataToCsv.ParsingDataStep(plannings, path);
            DataToCsv.ParsingDataMachine(plannings, path);
            DataToCsv.ParsingDataConsumable(plannings, path);
            DataToCsv.ParsingDataTank(plannings, path);
        }

        /// <summary>
        /// Methods that transform the Operator in a csv file
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataOperator(List<SolutionPlanning> plannings, string path)
        {
            string nameFile = "/Operateurs_Planning.csv";
            using StreamWriter streamWriter = new(path + nameFile);
            using CsvWriter csvWriter = new(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.WriteField("UID Operator");
            csvWriter.WriteField("UID OF");
            csvWriter.WriteField("Job Start");
            csvWriter.WriteField("Job End");
            csvWriter.WriteField("Code");
            
            csvWriter.NextRecord();

            foreach (var planning in plannings)
            {
                foreach (var list in planning.PlanningOperator)
                {
                    switch((string)list[3])
                    {
                        case "OPNetMachine":
                            csvWriter.WriteField<uint>((uint)list[5]);
                            csvWriter.WriteField("/");
                            csvWriter.WriteField<DateTime>((DateTime)list[1]);
                            csvWriter.WriteField<DateTime>((DateTime)list[2]);
                            csvWriter.WriteField((string)list[3]);
                            csvWriter.NextRecord();
                            break;

                        case "OPNetTank":
                            csvWriter.WriteField<uint>((uint)list[5]);
                            csvWriter.WriteField("//");
                            csvWriter.WriteField<DateTime>((DateTime)list[1]);
                            csvWriter.WriteField<DateTime>((DateTime)list[2]);
                            csvWriter.WriteField((string)list[3]);
                            csvWriter.NextRecord();
                            break;

                        default :
                            csvWriter.WriteField<uint>((uint)list[5]);
                            csvWriter.WriteField<int>((int)list[4]);
                            csvWriter.WriteField<DateTime>((DateTime)list[1]);
                            csvWriter.WriteField<DateTime>((DateTime)list[2]);
                            csvWriter.WriteField((string)list[3]);
                            csvWriter.NextRecord();
                            break;
                    }
                }
            }

            csvWriter.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataMachine(List<SolutionPlanning> plannings, string path)
        {
            string nameFile = "/Machines_Planning.csv";
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
                foreach (var list in planning.PlanningMachine)
                {
                    csvWriter.WriteField<int>((int)list[6]);
                    csvWriter.WriteField<DateTime>((DateTime)list[1]);
                    csvWriter.WriteField<DateTime>((DateTime)list[2]);
                    csvWriter.WriteField<int>((int)list[3]);
                    csvWriter.WriteField<uint>((uint)list[4]);
                    csvWriter.WriteField<uint>((uint)list[5]);
                    csvWriter.NextRecord();
                }
            }

            csvWriter.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataConsumable(List<SolutionPlanning> plannings, string path)
        {
            string nameFile = "/Consommables_Planning.csv";
            using StreamWriter streamWriter = new(path + nameFile);
            using CsvWriter csvWriter = new(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.WriteField("UID Consumable");
            csvWriter.WriteField("Quantity Used");
            csvWriter.NextRecord();

            foreach (var planning in plannings)
            {
                foreach (var list in planning.PlanningCons)
                {
                    csvWriter.WriteField<int>((int)list[2]);
                    csvWriter.WriteField<double>((double)list[1]);
                    csvWriter.NextRecord();
                }
            }

            csvWriter.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataStep(List<SolutionPlanning> plannings, string path)
        {
            string nameFile = "/Export_Planning.csv";
            using StreamWriter streamWriter = new(path + nameFile);
            using CsvWriter csvWriter = new(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.WriteField("IDENTIFIANT");
            csvWriter.WriteField("DATE_DEBUT");
            csvWriter.WriteField("DATE_FIN");  

            csvWriter.NextRecord();

            foreach (var planning in plannings)
            {
                foreach (var list in planning.PlanningStep)
                {
                    csvWriter.WriteField<double>((double)list[0]);
                    csvWriter.WriteField<DateTime>((DateTime)list[1]);
                    csvWriter.WriteField<DateTime>((DateTime)list[2]);
                    csvWriter.NextRecord();
                }
            }

            csvWriter.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="path"></param>
        public static void ParsingDataTank(List<SolutionPlanning> plannings, string path)
        {
            string nameFile = "/Cuves_Planning.csv";
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
                foreach (var list in planning.PlanningTank)
                {
                    csvWriter.WriteField<int>((int)list[6]);
                    csvWriter.WriteField<DateTime>((DateTime)list[1]);
                    csvWriter.WriteField<DateTime>((DateTime)list[2]);
                    csvWriter.WriteField<int>((int)list[4]);
                    csvWriter.WriteField<uint>((uint)list[5]);
                    csvWriter.NextRecord();
                }
            }

            csvWriter.Dispose();
        }
    }
}

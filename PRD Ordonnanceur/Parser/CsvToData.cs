using CsvHelper;
using PRD_Ordonnanceur.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PRD_Ordonnanceur.Parser
{
    /// <summary>
    /// This class transform the csv into parsed data
    /// </summary>
    public class CsvToData
    {
        private const string defaultPath = "C:/";

        /// <summary>
        /// Parse Consumable data
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static List<Consumable> ParsingDataConsommable(string rootPath)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            // Lecture des données Consommables

            string path = rootPath + "/Stocks.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            var records = new List<Consumable>();
            csv.Read();
            csv.ReadHeader();

            int count = 0;

            while (csv.Read())
            {
                var record = new Consumable
                {
                    Id = count,
                    Name = csv.GetField("ItemCode"),
                    QuantityAvailable = csv.GetField<double>("QTE_DISPO"),
                };

                count++;
                records.Add(record);
            }

            return records;
        }

        /// <summary>
        /// Parse Operator data
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static List<Operator> ParsingDataOperator(string rootPath)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            // Lecture des données Consommables

            string path = rootPath + "/Operateurs.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            var records = new List<Operator>();
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                string list = csv.GetField<String>("Competences");

                List<TypeMachine> typeMachines = new();

                if (list.Contains("blender"))
                    typeMachines.Add(TypeMachine.blender);

                if (list.Contains("mixer"))
                    typeMachines.Add(TypeMachine.Mixer);

                if (list.Contains("cleaning"))
                    typeMachines.Add(TypeMachine.cleaning);

                var record = new Operator
                {
                    Uid = csv.GetField<int>("Id"),
                    SkillSet = new(typeMachines),
                    StartWorkSchedule = csv.GetField<DateTime>("Debut"),
                    End = csv.GetField<DateTime>("Fin"),
                };

                records.Add(record);
            }

            return records;
        }

        /// <summary>
        /// Parse Machine data
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static List<Machine> ParsingDataMachine(string rootPath)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            // Data reading Consumables
            string path = rootPath + "/Machines.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            var records = new List<Machine>();
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                string typeMachineCsv = csv.GetField("Type");

                var type = typeMachineCsv switch
                {
                    "Qualite" => TypeMachine.blender,
                    "Disperseur" => TypeMachine.Mixer,
                    _ => TypeMachine.cleaning,
                };

                var record = new Machine
                {
                    Id = csv.GetField<int>("Id"),
                    TypeMachine = type,
                    CleaningDuration = new TimeSpan(0, csv.GetField<int>("dureeNettoyage"), 0),
                };

                records.Add(record);
            }

            return records;
        }

        /// <summary>
        /// Parse OF data
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="consumables"></param>
        /// <returns></returns>
        public static List<OF> ParsingDataOF(string rootPath, List<Consumable> consumables)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            // Reading the data
            string path = rootPath + "/OFS.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            var recordsOF = new List<OF>();
            var recordsStep = new List<Step>();

            csv.Read();
            csv.ReadHeader();

            // Reading OFs and steps
            bool sameOF = false;
            var currentOFName = "";

            while (csv.Read())
            {
                if (currentOFName == csv.GetField("ACTIVITE"))
                    sameOF = true;
                else
                    sameOF = false;

                currentOFName = csv.GetField("ACTIVITE");

                if (currentOFName == "")
                    break;

                var recordOF = new OF
                {
                    Uid = csv.GetField<int>("ACTIVITE"),
                    StartingHour = DateTime.MinValue,
                    StepSequence = new(),
                    EarliestDate = DateTime.MinValue,
                    NextStep = 0,
                };

                TimeSpan durationOp = new(0, csv.GetField<int>("TPS2"), 0);
                TimeSpan durationBeforeOp = new(0, 10, 0);
                TimeSpan durationAfterOp = new(0, 10, 0);

                Duration duration = new(durationBeforeOp, durationAfterOp, durationOp);

                if (!sameOF)
                {
                    // New OF with new step
                    var recordStep = new Step
                    {
                        Uid = csv.GetField<double>("IDENTIFIANT"),
                        ConsumableUsed = new(),
                        QuantityConsumable = new(),
                        Duration = duration,
                    };

                    recordsOF.Add(recordOF);
                    recordsStep.Add(recordStep);

                    recordsOF[recordsOF.Count - 1].StepSequence.Add(recordStep);
                }
                else
                {
                    if (csv.GetField("DATE_CIBLE") != "")
                    {
                        recordsOF[recordsOF.Count - 1].LatestDate = csv.GetField<DateTime>("DATE_CIBLE");
                    }

                    // New step
                    var recordStep = new Step
                    {
                        Uid = csv.GetField<double>("IDENTIFIANT"),
                        ConsumableUsed = new(),
                        QuantityConsumable = new(),
                        Duration = duration,
                    };

                    recordsStep.Add(recordStep);
                    recordsOF[recordsOF.Count - 1].StepSequence.Add(recordStep);
                }
            }

            // Allocation of consumables to steps
            path = rootPath + "/Etapes.csv";
            using var reader2 = new StreamReader(path);
            using var csv2 = new CsvReader(reader2, CultureInfo.CurrentCulture);

            csv2.Read();
            csv2.ReadHeader();

            while (csv2.Read())
            {
                foreach (OF oF in recordsOF)
                {
                    foreach (Step step in oF.StepSequence)
                    {
                        if (step.Uid == csv2.GetField<double>("ETAPES"))
                        {
                            foreach (Consumable consumable in consumables)
                            {
                                if (consumable.Name == csv2.GetField("COMPOSANT"))
                                {
                                    step.ConsumableUsed.Add(consumable);
                                    step.QuantityConsumable.Add(csv2.GetField<float>("QTE_T"));
                                }
                            }
                        }
                    }
                }
            }

            return recordsOF;
        }

        /// <summary>
        /// Parse Tank data
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static List<Tank> ParsingDataTank(string rootPath)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            string path = rootPath + "/Cuves.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            var records = new List<Tank>();
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var record = new Tank
                {
                    IdTank = csv.GetField<int>("Id"),
                };

                records.Add(record);
            }

            return records;
        }
    }
}
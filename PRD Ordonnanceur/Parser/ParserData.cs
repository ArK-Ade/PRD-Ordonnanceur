using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using PRD_Ordonnanceur.Parser;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace PRD_Ordonnanceur.Parser
{
    public class ParserData
    {
        private const string defaultPath = "C:/";

        public static List<Consumable> ParsingDataConsommable(string rootPath)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            // Lecture des données Consommables

            string path = rootPath + "Stocks2.csv";
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

        public static List<Operator> ParsingDataOperator(string rootPath)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            // Lecture des données Consommables

            string path = rootPath + "Operateurs.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            var records = new List<Operator>();
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var record = new Operator
                {
                    Id = csv.GetField<uint>("Id"),
                    Beginning = csv.GetField<DateTime>("Debut"),
                    End = csv.GetField<DateTime>("Fin"),
                };

                records.Add(record);
            }

            return records;
        }

        public static List<Machine> ParsingDataMachine(string rootPath)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            // Lecture des données Consommables

            string path = rootPath + "Machines.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            var records = new List<Machine>();
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                string qqch = csv.GetField("Type");

                var type = qqch switch
                {
                    "Qualite" => TypeMachine.blender,
                    "Disperseur" => TypeMachine.Mixer,
                    _ => TypeMachine.cleaning,
                };

                var record = new Machine
                {
                    Id = csv.GetField<int>("Id"),
                    TypeMachine = type,
                    Duration_cleaning = new TimeSpan(csv.GetField<int>("dureeNettoyage")),
                };

                records.Add(record);
            }

            return records;
        }

        public static List<OF> ParsingDataOF(string rootPath, List<Consumable> consumables)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            // Lecture des données

            string path = rootPath + "OF_ROU2.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            var recordsOF = new List<OF>();
            var recordsStep = new List<Step>();

            csv.Read();
            csv.ReadHeader();

            // Lecture des OFs et etapes

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
                    IdOF = csv.GetField<int>("ACTIVITE"),
                    Starting_hour = DateTime.MinValue,
                    StepSequence = new(),
                };

                if (!sameOF)
                {
                    // Nouvelle OF avec des etapes
                    var recordStep = new Step
                    {
                        IdStep = csv.GetField<double>("IDENTIFIANT"),
                        ConsumableUsed = new(),
                        QuantityConsumable = new(),
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

                    // Nouvelle etape
                    var recordStep = new Step
                    {
                        IdStep = csv.GetField<double>("IDENTIFIANT"),
                        ConsumableUsed = new(),
                        QuantityConsumable = new(),
                    };

                    recordsStep.Add(recordStep);
                    recordsOF[recordsOF.Count - 1].StepSequence.Add(recordStep);
                }               
            }

            DataParsed data = new();

            // Attribution des consommables aux etapes
            path = rootPath + "OF_BOM2.csv";
            using var reader2 = new StreamReader(path);
            using var csv2 = new CsvReader(reader2, CultureInfo.CurrentCulture);

            csv2.Read();
            csv2.ReadHeader();

            while (csv2.Read())
            {
                foreach(OF oF in recordsOF)
                {
                    foreach (Step step in oF.StepSequence)
                    {
                        if (step.IdStep == csv2.GetField<double>("ETAPES"))
                        {
                            Consumable consumable = consumables.Find(x => x.Name == csv2.GetField("COMPOSANT"));
                            step.ConsumableUsed.Add(consumable);
                            step.QuantityConsumable.Add(csv2.GetField<float>("QTE_T"));
                        }
                    }
                }
            }

            return recordsOF;
        }

        public static List<Tank> ParsingDataTank(string rootPath)
        {
            if (rootPath == "")
            {
                rootPath = defaultPath;
            }

            string path = rootPath + "Cuves.csv";
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

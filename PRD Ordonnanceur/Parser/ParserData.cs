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

                if(list.Contains("blender"))
                    typeMachines.Add(TypeMachine.blender);

                if(list.Contains("mixer"))
                    typeMachines.Add(TypeMachine.Mixer);

                if(list.Contains("cleaning"))
                    typeMachines.Add(TypeMachine.cleaning);

                var record = new Operator
                {
                    Uid = csv.GetField<uint>("Id"),
                    SkillSet = new(typeMachines),
                    StartWorkSchedule = csv.GetField<DateTime>("Debut"),
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

            string path = rootPath + "/Machines.csv";
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
                    CleaningDuration = new TimeSpan(0,csv.GetField<int>("dureeNettoyage"),0),
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

            string path = rootPath + "/OFS.csv";
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
                    // Nouvelle OF avec des etapes
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

                    // Nouvelle etape
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

            // Attribution des consommables aux etapes
            path = rootPath + "/Etapes.csv";
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
                        if (step.Uid == csv2.GetField<double>("ETAPES"))
                        {
                            foreach(Consumable consumable in consumables)
                            {
                                if(consumable.Name == csv2.GetField("COMPOSANT"))
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

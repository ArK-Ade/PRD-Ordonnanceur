using PRD_Ordonnanceur.Algorithms;
using PRD_Ordonnanceur.Checker;
using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace PRD_Ordonnanceur.View
{
    /// <summary>
    /// This class represents the view where you can use the algorithm
    /// </summary>
    public partial class Importation_Dossier : Form
    {
        private string pathToCsvData = "";
        private string pathToResultFolder = "";
        private TimeSpan duration;

        private DateTime date = DateTime.MaxValue;

        /// <summary>
        /// Loading the view
        /// </summary>
        public Importation_Dossier()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button1 listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                AutoClosingMessageBox.Show("Chemin selectionné: " + fbd.SelectedPath, "Notification", 1000);
                pathToCsvData = fbd.SelectedPath;
            }

            if (pathToCsvData != "" && pathToResultFolder != "" && date != DateTime.MaxValue)
                button2.Visible = true;
        }

        /// <summary>
        /// Label1 listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            // Method intentionally left empty.
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Importation_Dossier_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = new DateTime(2022, 1, 1);
            dateTimePicker1.MaxDate = DateTime.Today.AddDays(7);
            dateTimePicker1.ShowCheckBox = false;
            dateTimePicker1.ShowUpDown = true;

            button2.Visible = false;
        }

        /// <summary>
        /// Button2 listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            AutoClosingMessageBox.Show("Lancement de l'algorithme", "Notification", 1000);

            List<Consumable> consumables = CsvToData.ParsingDataConsommable(pathToCsvData);
            List<OF> oFs = CsvToData.ParsingDataOF(pathToCsvData, consumables);
            List<Operator> operators = CsvToData.ParsingDataOperator(pathToCsvData);
            List<Machine> machines = CsvToData.ParsingDataMachine(pathToCsvData);
            List<Tank> tanks = CsvToData.ParsingDataTank(pathToCsvData);

            // Selecting the strategy for the heuristic
            Heuristic heuristic = new();
            Context context = new(heuristic);

            oFs = context.Launch(1, oFs);

            DataParsed dataParsed = new(oFs, consumables, machines, tanks, operators, null);

            // Launching the algorithm
            JobShopAlgorithm algorithm = new(dataParsed, new(), new(), new());

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            int numberConstraint = algorithm.StepAlgorithm(date);
            stopwatch.Stop();

            duration = stopwatch.Elapsed;

            label2.Text = String.Format("Elapsed Time is {0:00}:{1:00}:{2:00}.{3}",
                            duration.Hours, duration.Minutes, duration.Seconds, duration.Milliseconds);

            AutoClosingMessageBox.Show("Nombre de contrainte non respectés : " + numberConstraint, "Alerte", 1000);

            // Verifing the constraints
            bool constraintOF = false;
            bool constrainOperator = false;
            bool constrainMachine = false;
            bool constrainTank = false;
            bool constrainConsummable = false;

            for (int i = 0; i < algorithm.Plannings.Count; i++)
            {
                constraintOF = CheckerOF.CheckConstraintOF(algorithm.Plannings[i]);
                constrainOperator = CheckerOF.CheckConstraintOperator(algorithm.Plannings[i], algorithm.DataParsed.Operators);
                constrainMachine = CheckerOF.CheckConstraintMachine(algorithm.Plannings[i]);
                constrainTank = CheckerOF.CheckConstraintTank(algorithm.Plannings[i]);
                constrainConsummable = CheckerOF.CheckConstraintConsumable(algorithm.Plannings[i], algorithm.DataParsed.Consummables);

                if (!constraintOF || !constrainOperator || !constrainTank || !constrainMachine || !constrainConsummable)
                    break;
            }

            if (constraintOF && constrainOperator && constrainTank && constrainMachine && constrainConsummable)
                AutoClosingMessageBox.Show("Le vérificateur n'a trouvé aucune erreur", "Alerte", 1000);
            else
                AutoClosingMessageBox.Show("Le vérificateur a trouvé une erreur", "Alerte", 1000);

            DataToCsv.LaunchParsing(algorithm.Plannings, pathToResultFolder);
            MessageBox.Show("Operation terminée");
        }

        /// <summary>
        /// DateTimePicker listening
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateTime = dateTimePicker1.Value;
            int year = dateTime.Year;
            int mouth = dateTime.Month;
            int day = dateTime.Day;

            date = new(year, mouth, day, 8, 0, 0);

            if (pathToCsvData != "" && pathToResultFolder != "" && date != DateTime.MaxValue)
                button2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                AutoClosingMessageBox.Show("Chemin selectionné: " + fbd.SelectedPath, "Notification", 1000);
                pathToResultFolder = fbd.SelectedPath;
            }

            if (pathToCsvData != "" && pathToResultFolder != "" && date != DateTime.MaxValue)
                button2.Visible = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}
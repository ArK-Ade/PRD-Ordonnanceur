using PRD_Ordonnanceur.Algorithms;
using PRD_Ordonnanceur.Checker;
using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRD_Ordonnanceur.View
{
    public partial class Importation_Dossier : Form
    {
        string pathCSV = "";

        public Importation_Dossier()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    AutoClosingMessageBox.Show("Chemin selectionné: " + fbd.SelectedPath, "Notification", 1000);
                    pathCSV = fbd.SelectedPath;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Method intentionally left empty.
        }

        private void Importation_Dossier_Load(object sender, EventArgs e)
        {
            // Method intentionally left empty.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pathCSV != "")
            {
                AutoClosingMessageBox.Show("Lancement de l'algorithme", "Notification", 1000);

                List<Consumable> consumables = ParserData.ParsingDataConsommable(pathCSV);
                List<OF> oFs = ParserData.ParsingDataOF(pathCSV, consumables);
                List<Operator> operators = ParserData.ParsingDataOperator(pathCSV);
                List<Machine> machines = ParserData.ParsingDataMachine(pathCSV);
                List<Tank> tanks = ParserData.ParsingDataTank(pathCSV);

                Heuristic heuristic = new();
                oFs = new(heuristic.SortCrescentDtiCrescentDli(oFs));

                DataParsed dataParsed = new(oFs, consumables, machines, tanks, operators, null);

                jobShopAlgorithm algorithm = new(dataParsed, new(), new(), new()); //TODO Changer le type de quantitéConsomable
                int numberConstraint = algorithm.StepAlgorithm(new(2022,02,01,8,0,0));

                bool constraintOF = false;
                bool constrainOperator = false;
                bool constrainMachine = false;
                bool constrainTank = false;
                bool constrainConsummable = false;

                for (int i = 0; i < algorithm.Plannings.Count; i++)
                {
                    constraintOF = CheckerOF.CheckConstrainOF(algorithm.Plannings[i]);
                    constrainOperator = CheckerOF.CheckConstrainOperator(algorithm.Plannings[i], algorithm.DataParsed.Operators);
                    constrainMachine = CheckerOF.CheckConstrainMachine(algorithm.Plannings[i]);
                    constrainTank = CheckerOF.CheckConstrainTank(algorithm.Plannings[i]);
                    constrainConsummable = CheckerOF.CheckConstrainConsommable(algorithm.Plannings[i], algorithm.DataParsed.Consummables);

                    if (!constraintOF || !constrainOperator || !constrainTank || !constrainMachine || !constrainConsummable)
                        break;
                }

                if (constraintOF && constrainOperator && constrainTank && constrainMachine && constrainConsummable)
                    AutoClosingMessageBox.Show("Toutes les contraintes ont été respectées", "Alerte", 1000);
                else
                    AutoClosingMessageBox.Show("Une contrainte de l'algorithme n'a pas été respectée", "Alerte", 1000);

                // TODO Ajouter l'affichage d'une date pour le lancement de l'algorithme
                // TODO Changer les durées d'opérations etc
                //string[] files = Directory.GetFiles(fbd.SelectedPath);

            }
            else
            {
                MessageBox.Show("Aucun dossier choisi");
            }
        }
    }
}

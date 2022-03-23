using PRD_Ordonnanceur.Algorithms;
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
                    MessageBox.Show("Chemin selectionné : " + fbd.SelectedPath, "Message");
                    pathCSV = fbd.SelectedPath;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Importation_Dossier_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pathCSV != "")
            {
                
                    MessageBox.Show("Lancement de l'algorithme", "Message");

                    List<Consumable> consumables = ParserData.ParsingDataConsommable(pathCSV);
                    List<OF> oFs = ParserData.ParsingDataOF(pathCSV, consumables);
                    List<Operator> operators = ParserData.ParsingDataOperator(pathCSV);
                    List<Machine> machines = ParserData.ParsingDataMachine(pathCSV);
                    List<Tank> tanks = ParserData.ParsingDataTank(pathCSV);

                    Heuristic heuristic = new();
                    oFs = new(heuristic.SortCrescentDtiCrescentDli(oFs));

                    DataParsed dataParsed = new(oFs, consumables, machines, tanks, operators, null);

                    Job_shop_algorithm algorithm = new(dataParsed, new(), new(), new());
                    int numberConstraint = algorithm.StepAlgorithm(DateTime.Now);

                    MessageBox.Show("Nombre de contraintes : " + numberConstraint, "Message");

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

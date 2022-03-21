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
                try
                {
                    MessageBox.Show("Lancement de l'algorithme", "Message");
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + exp.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + exp.Message);
                }
            }
            else
            {
                MessageBox.Show("Aucun dossier choisi");
            }
        }
    }
}

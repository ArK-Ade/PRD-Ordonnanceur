using PRD_Ordonnanceur.View;
using System;
using System.Windows.Forms;

namespace PRD_Ordonnanceur
{
    /// <summary>
    /// Main View of the application
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Method intentionally left empty.
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Method intentionally left empty.
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Method intentionally left empty.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new Importation_Dossier();
            form.ShowDialog();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            // Method intentionally left empty.
        }
    }
}
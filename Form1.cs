using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace TubesGraph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Open file
            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;
            textBox1.Text = fileName;

            Processor Processor = new Processor(fileName);


            //Read file per line
            for (int i = 0; i <= Processor.totalEdge; i++)
            {
                if (i != 0)
                {
                    Processor.fileContent += Processor.fileLines[i] + "\n";
                }
            }

            //Read all nodes
            Processor.setupNodes();
            richTextBox1.Text = Processor.fileContent;


            gViewer1.Graph = Processor.process();


            if (checkBox1.Checked)
            {
                //isi algoritma BFS
            }

            else if(checkBox2.Checked)
            {
                //isi algoritma DFS
            }
   
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void gViewer1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
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

            Processor Process = new Processor(fileName);


            //Read file per line
            for (int i = 0; i <= Process.totalEdge; i++)
            {
                if (i != 0)
                {
                    Process.fileContent += Process.fileLines[i] + "\n";
                }
            }

            //Read all nodes
            Process.setupNodes();
            richTextBox1.Text = Process.fileContent;


            gViewer1.Graph = Process.process();


            /* if (checkBox1.Checked)
            {
                //isi algoritma BFS
            }

            else if(checkBox2.Checked)
            {
                //isi algoritma DFS
            } */

            int totalNode = Process.allNode.Count();
            for (int i = 0; i < totalNode; i++)
            {
                comboBox1.Items.Add(Process.nodeIn[i]);
                comboBox1.Items.Add(Process.nodeOut[i]);
            }

            List<string> allNodes = new List<string>();
            foreach (string S in comboBox1.Items)
            {
                if (!allNodes.Contains(S))
                {
                    allNodes.Add(S);
                }
            }
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(allNodes.ToArray());

            
            foreach (string S in comboBox2.Items)
            {
                if (!allNodes.Contains(S))
                {
                    allNodes.Add(S);
                }
            }
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(allNodes.ToArray());


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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.AutoCheck = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.AutoCheck = true;
        }
    }
}
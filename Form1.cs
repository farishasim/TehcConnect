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
using System.Threading;

namespace TubesGraph
{
    public partial class Form1 : Form
    {
        private Processor Process;
        private bool fileLoaded;
        public delegate void delUpdateVisGraph(Microsoft.Msagl.Drawing.Graph graph);

        ThreadStart threadStart;
        Thread processingThread;
        private bool locked = false; // for mutual exclusion

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

            Process = new Processor(fileName, this);
            fileLoaded = true;

            //Read file per line
            for (int i = 0; i <= Process.totalEdge; i++)
            {
                if (i != 0)
                {
                    Process.fileContent += Process.fileLines[i] + "\n";
                }
            }

            //Read all nodes
            richTextBox1.Text = Process.fileContent;


            gViewer1.Graph = Process.process();


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

            /*
            threadStart = new ThreadStart(StartProcessing);
            processingThread = new Thread(threadStart);

            processingThread.Name = "Solving Process";
            processingThread.Start();
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fileLoaded && !locked)
            {
                locked = true; // turn on lock

                threadStart = new ThreadStart(StartProcessing);
                processingThread = new Thread(threadStart);

                processingThread.Name = "Solving Process";
                processingThread.Start();
            }


        }

        private void StartProcessing()
        {
            // thread process
            // thread ini akan melakukan proses algoritma

            Process.ProcessPath();

            delUpdateVisGraph DelUpdateVisGraph = new delUpdateVisGraph(VisualizeGraph);

            this.gViewer1.BeginInvoke(DelUpdateVisGraph, Process.GetVisualGraph());

            locked = false; // turn off lock
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
            string selectedNode = comboBox1.SelectedItem.ToString();
            Process.SetNodeSrc(selectedNode);
        }      

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedNode = comboBox2.SelectedItem.ToString();
            Process.SetNodeDst(selectedNode);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.AutoCheck = true;
            Process.SetAlgorithm(1);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.AutoCheck = true;
            Process.SetAlgorithm(2);
        }

        public void UpdateGraphFromThread(Microsoft.Msagl.Drawing.Graph visualGraph)
        {
            delUpdateVisGraph DelUpdateVisGraph = new delUpdateVisGraph(VisualizeGraph);

            this.gViewer1.BeginInvoke(DelUpdateVisGraph, visualGraph);
        }

        public void VisualizeGraph(Microsoft.Msagl.Drawing.Graph visualGraph)
        {
            this.gViewer1.Graph = visualGraph;
        }
    }
}
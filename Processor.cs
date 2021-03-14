using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TubesGraph
{
    public class Processor 
    {
        public int totalNodes;
        public string[] fileLines;
        public string[] nodeIn;
        public string[] nodeOut;
        private Microsoft.Msagl.Drawing.Graph graph;

        public Processor(string fileName)
        {
            this.fileLines = File.ReadAllLines(fileName);
            this.totalNodes = int.Parse(this.fileLines[0]);
            this.setupNodes();
        }

        private void setupNodes()
        {
            this.nodeIn = new string[this.totalNodes];
            this.nodeOut = new string[this.totalNodes];
            for (int i = 0; i <= this.totalNodes; i++)
            {
                if (i > 0)
                {
                    this.nodeIn[i - 1] += this.fileLines[i][0];
                    this.nodeOut[i - 1] += this.fileLines[i][2];
                }
            }
        }

        public Microsoft.Msagl.Drawing.Graph process()
        {
            this.graph = new Microsoft.Msagl.Drawing.Graph("graph");

            for (int i = 0; i < this.totalNodes; i++)
            {
                this.graph.AddEdge(this.nodeIn[i], this.nodeOut[i]).Attr.Color = Microsoft.Msagl.Drawing.Color.MediumSpringGreen;

                this.graph.FindNode(this.nodeIn[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.SpringGreen;
                this.graph.FindNode(this.nodeIn[i]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;

                this.graph.FindNode(this.nodeOut[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.SpringGreen;
                this.graph.FindNode(this.nodeOut[i]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
            }

            // kalo nanti versi lengkapnya, sebelum di return si graphnya harusnya di solve dulu
            // berdasrkan pilihan algoritmanya BFS atau DFS
            return this.graph;
        }
    }
}

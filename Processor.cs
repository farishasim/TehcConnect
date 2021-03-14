using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace TubesGraph
{
    public class Processor
    {
        public int totalEdge;
        public string fileContent;
        public string[] fileLines;
        public string[] nodeIn;
        public string[] nodeOut;
        private Microsoft.Msagl.Drawing.Graph graph;

        public Processor(string fileName)
        {
            this.fileContent = "";
            this.fileLines = File.ReadAllLines(fileName);
            this.totalEdge = int.Parse(this.fileLines[0]);
            this.setupNodes();
        }

        public void setupNodes()
        {
            this.nodeOut = new string[this.totalEdge];
            this.nodeIn = new string[this.totalEdge];
            for (int i = 0; i <= this.totalEdge; i++)
            {
                if (i > 0)
                {
                    this.nodeOut[i - 1] += this.fileLines[i][0];
                    this.nodeIn[i - 1] += this.fileLines[i][2];
                }
            }
        }

        public Microsoft.Msagl.Drawing.Graph process()
        {
            this.graph = new Microsoft.Msagl.Drawing.Graph("graph");

            for (int i = 0; i < this.totalEdge; i++)
            {
                // graph.AddEdge(this.nodeOut[i], this.nodeIn[i]).Attr.Color = Microsoft.Msagl.Drawing.Color.MediumSpringGreen;
                var Edge = graph.AddEdge(this.nodeOut[i], this.nodeIn[i]);

                Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                Edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;


                graph.FindNode(this.nodeOut[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.SpringGreen;
                graph.FindNode(this.nodeOut[i]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;

                graph.FindNode(this.nodeIn[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.SpringGreen;
                graph.FindNode(this.nodeIn[i]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
            }

            // kalo nanti versi lengkapnya, sebelum di return si graphnya harusnya di solve dulu
            // berdasrkan pilihan algoritmanya BFS atau DFS
            return this.graph;
        }
    }
}

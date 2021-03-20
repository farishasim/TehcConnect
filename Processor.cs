using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;


namespace TubesGraph
{
    public class Processor
    {
        public int totalEdge;
        public string fileContent;
        public string[] fileLines;
        public string[] nodeIn;
        public string[] nodeOut;
        public string[] allNode;

        private int choice; // pilihan algoritma
        private Graph graph;
        private Microsoft.Msagl.Drawing.Graph visualGraph;

        private string nodeSrc;
        private string nodeDst;
        private List<string> nodes;

        private Form1 form1;

        public Processor()
        {

        }

        public Processor(string fileName, Form1 form1)
        {
            this.fileContent = "";
            this.fileLines = File.ReadAllLines(fileName);
            this.totalEdge = int.Parse(this.fileLines[0]);
            this.SetupNodes();
            this.SetupGraph();
            this.form1 = form1;
        }

        private void SetupNodes()
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

            allNode = nodeOut.ToArray();

            this.nodes = new List<string>();
            nodes = allNode.Union(nodeIn).Distinct().ToList();
            nodes.Sort(); //sort nodes berdasakan alfabet
        }

        private void SetupGraph()
        {
            graph = new Graph(nodes.Count());
            foreach (string node in nodes)
            {
                graph.AddNode(nodes.IndexOf(node));
            }
            for (int i = 0; i < totalEdge; i++)
            {
                graph.AddEdge(nodes.IndexOf(nodeIn[i]), nodes.IndexOf(nodeOut[i]));
            }
        }

        public Microsoft.Msagl.Drawing.Graph process()
        {
            this.visualGraph = new Microsoft.Msagl.Drawing.Graph("graph");

            for (int i = 0; i < this.totalEdge; i++)
            {
                // graph.AddEdge(this.nodeOut[i], this.nodeIn[i]).Attr.Color = Microsoft.Msagl.Drawing.Color.MediumSpringGreen;
                var Edge = visualGraph.AddEdge(this.nodeOut[i], this.nodeIn[i]);

                Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                Edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                Edge.Attr.Color = Microsoft.Msagl.Drawing.Color.SpringGreen;

                visualGraph.FindNode(this.nodeOut[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.SpringGreen;
                visualGraph.FindNode(this.nodeOut[i]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;

                visualGraph.FindNode(this.nodeIn[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.SpringGreen;
                visualGraph.FindNode(this.nodeIn[i]).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
            }

            return this.visualGraph;
        }

        public void ProcessPath()
        {
            List<int> path = new List<int>();
            Searcher searcher;

            this.process(); // reset visual graph

            if (choice == 1)
            {
                // gunakan DFS
                searcher = new DFSearcher(this, form1);
                path = searcher.Search(nodes.IndexOf(nodeSrc), nodes.IndexOf(nodeDst), graph);
            } else
            {
                // gunakan BFS
            }

            if (path.Count() > 0)
            {
                // path ditemukan
                UpdateGraph(path); // update visual graph
            }

            //return this; // supaya dapat dilakukan method-chaining
        }

        public Processor UpdateGraph(List<int> path)
        {
            int i; string thisNode;
            for (i = 0; i < path.Count(); i++) // traversal satu per satu pada path
            {
                thisNode = nodes[path[i]];
                visualGraph.FindNode(thisNode).Attr.FillColor = Microsoft.Msagl.Drawing.Color.OrangeRed; // tandai node pada path dengan warna biru
                if (i < path.Count()-1)
                {
                    string nextNode = nodes[path[i+1]];
                    foreach(var e in visualGraph.Edges)
                    {
                        // cari edge dimana src = path[i] dan dst = path[i+1], atau sebaliknya
                        if ( (e.Source.Equals(thisNode) && e.Target.Equals(nextNode))
                            || (e.Source.Equals(nextNode) && e.Target.Equals(thisNode)) )
                        {
                            e.Attr.Color = Microsoft.Msagl.Drawing.Color.OrangeRed;
                        }

                    }
                }
            }

            return this;
        }

        public void SetNodeSrc(string node)
        {
            nodeSrc = node;
        }

        public void SetNodeDst(string node)
        {
            nodeDst = node;
        }

        public void SetAlgorithm(int choice)
        {
            this.choice = choice;
        }

        public Microsoft.Msagl.Drawing.Graph GetVisualGraph()
        {
            return visualGraph;
        }
    }
}


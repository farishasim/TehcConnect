using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TubesGraph
{
    class BFSearcher : Searcher
    {
        private bool[] visited;
        private List<int> path;
        private Queue<int> queue;

        private Processor processor;
        private Form1 form1;

        public BFSearcher() // default constructr biar gk eror
        {

        }

        public BFSearcher(Processor processor, Form1 form1)
        {
            this.processor = processor;
            this.form1 = form1;
        }

        private void Initial(int n_nodes)
        {
            visited = new bool[n_nodes];
            for (int i = 0; i < n_nodes; i++)
            {
                visited[i] = false;
            }

            path = new List<int>();
            queue = new Queue<int>();
        }

        public List<int> Search(int node1, int node2, Graph graph)
        {
            bool found = false;
            Initial(graph.CountNode());

            queue.Enqueue(node1);

            while (queue.Count != 0 && !found)
            {
                int node = queue.Dequeue();
                visited[node] = true;
                found = BreadthFirstSearch(graph, node,  node2);
            } 

            return path;
        }


        private bool BreadthFirstSearch(Graph graph, int node, int target)
        {
            if (node == target) {
                return true;
            } 
            else
            {
                ExpandNode(graph, node);
                path.Add(node);
                return false;
            }
        }


        private void ExpandNode(Graph graph, int node)
        {
            for (int i = 0; i < graph.CountNode(); i++)
            {
                if (graph.FindEdge(node,i) && !visited[i])
                {
                    queue.Enqueue(i);
                }
            }
        }


        private void VisualizeStep()
        {
            processor.process();
            form1.UpdateGraphFromThread(processor.UpdateGraph(path).GetVisualGraph());

            Thread.Sleep(500);
        }
    }
}

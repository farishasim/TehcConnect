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
        private Queue<List<int>> nodePath;

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
            nodePath = new Queue<List<int>>();
        }

        public List<int> Search(int node1, int node2, Graph graph)
        {
            bool found = false;
            List<int> currentPath = new List<int>();
            Initial(graph.CountNode());

            queue.Enqueue(node1);
            currentPath.Add(node1);

            nodePath.Enqueue(currentPath);

            while (queue.Count != 0 && !found)
            {
                int node = queue.Dequeue();
                path = nodePath.Dequeue();
                visited[node] = true;
                found = BreadthFirstSearch(graph, node, node2);
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
                return false;
            }
        }


        private void ExpandNode(Graph graph, int node)
        {
            for (int i = 0; i < graph.CountNode(); i++)
            {
                if (graph.FindEdge(node,i) && !visited[i])
                {
                    // tambahkan node i ke queue
                    queue.Enqueue(i);

                    // tambahkan path menuju node i ke stack
                    path.Add(i);

                    List<int> newPath = new List<int>(path);

                    nodePath.Enqueue(newPath);
                    path.Remove(i);
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

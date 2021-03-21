using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace TubesGraph
{
    class BFSearcher : Searcher
    {
            private bool[] visited;
            private List<int> path;
            private Queue<int> queue; // queue berisi node yang akan dikunjungi
            private Queue<List<int>> nodePath; // queue berisi path menuju node yang akan dikunjungi

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

            // inisiasi node awal
            queue.Enqueue(node1);
            currentPath.Add(node1);

            nodePath.Enqueue(currentPath);

            while (queue.Count != 0 && !found)
            {
                // pilih node beserta path menuju node tersbut pada queue
                int node = queue.Dequeue();
                path = nodePath.Dequeue();
                
                // tandai sudah dikunjungi
                visited[node] = true;

                // tampilkan step
                VisualizeStep();

                // proses bfs pada node ini, jika found == true artinya solusi ditemukan
                found = BreadthFirstSearch(graph, node, node2);
            } 

            if (!found)
            {
                // path tidak ditemukan
                path.Clear();
            }

            return path;
        }


        private bool BreadthFirstSearch(Graph graph, int node, int target)
        {
            if (node == target) {
                // jika sampai pada target, bernilai true
                return true;
            } 
            else
            {
                // jika belum sampai, maka node ini akan di-ekspan
                ExpandNode(graph, node);
                return false;
            }
        }


        private void ExpandNode(Graph graph, int node)
        {
            // proses ekspan untuk suatu node
            for (int i = 0; i < graph.CountNode(); i++)
            {
                // jika node i bersisian dan belum dikunjungi
                if (graph.FindEdge(node,i) && !visited[i])
                {
                    // tambahkan node i ke queue
                    queue.Enqueue(i);

                    // tambahkan path menuju node i ke queue
                    path.Add(i);

                    List<int> newPath = new List<int>(path);

                    nodePath.Enqueue(newPath);
                    path.Remove(i);
                }
            }
        }

        public List<int> GetListFirstDegree(int nodeSrc, Graph graph)
        {
            Queue<int> friends = new Queue<int>();

            Initial(graph.CountNode());

            visited[nodeSrc] = true;

            ExpandNode(graph, nodeSrc);

            while (queue.Count != 0)
            {
                int node = queue.Dequeue();
                visited[node] = true;
                friends.Enqueue(node);
            }

            while (friends.Count != 0)
            {
                int node = friends.Dequeue();
                ExpandNode(graph, node);
            }

            List<int> list = new List<int>(queue);

            list = list.Distinct().ToList();

            return list;
        }

        private void VisualizeStep()
        {
            processor.process();
            form1.UpdateGraphFromThread(processor.UpdateGraph(path).GetVisualGraph());

            Thread.Sleep(500);
        }
    }
}

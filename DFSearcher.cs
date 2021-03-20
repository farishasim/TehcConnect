using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TubesGraph
{
    class DFSearcher : Searcher
    {
        private bool[] visited;
        private List<int> path;

        private Processor processor;
        private Form1 form1;

        public DFSearcher() // default constructr biar gk eror
        {

        }

        public DFSearcher(Processor processor, Form1 form1)
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
        }

        public List<int> Search(int node1, int node2, Graph graph)
        {
            bool found;
            
            Initial(graph.CountNode());

            visited[node1] = true;
            path.Add(node1);
            VisualizeStep();
            found = DepthFirstSearch(graph, node1, node2);

            if (!found)
            {
                path.Remove(node1);
            }

            return path;
        }

        private bool DepthFirstSearch(Graph graph, int node, int target)
        {
            // Algoritma pencarian DFS secara rekursif
            bool found = false;

            // kasus basis, ketika ada node sama dengan target
            if (node == target)
            {
                return true; 
            }

            // kasus rekurens
            for(int i = 0; i < graph.CountNode(); i++)
            {
                if (graph.FindEdge(node, i) && !visited[i])
                {
                    // tandai node sudah dikunjungi
                    visited[i] = true;

                    // masukkan node ke path
                    path.Add(i);
                    VisualizeStep();

                    // periksa secara rekursif untuk node selanjutnya secara DFS
                    found = DepthFirstSearch(graph, i, target);
                    if (found)
                    {
                        // jika pemanggilan secara rekursif bernilai true, maka solusi ditemukan, pencarian dihentikan
                        return true; 
                    } else
                    {
                        // jika tidak ditemukan, maka hilangkan i dari path, pencarian diteruskan,
                        // node lain yang bersisian akan dibangkitkan
                        path.Remove(i);
                    }
                }
            }

            // jika tidak ditemukan satupun path dari node ini, maka return false dan backtrack
            return found;
        }

        private void VisualizeStep()
        {
            processor.process();
            form1.UpdateGraphFromThread(processor.UpdateGraph(path).GetVisualGraph());

            Thread.Sleep(500);
        }
    }
}

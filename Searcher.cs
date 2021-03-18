using System;
using System.Collections.Generic;
using System.Text;

namespace TubesGraph
{
    interface Searcher
    {
        // Mencari Path dari node node1 ke node node2 pada graph 
        // return type berupa list of integer
        public List<int> Search(int node1, int node2, Graph graph);
    }
}

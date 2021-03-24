using System;
using System.Collections.Generic;
using System.Text;

namespace TubesGraph
{
    static class DFSTester
    {
        static void Mainr()
        {
            Graph graph = new Graph(8);

            String akun = "ABCDEFGH";
            char akun1 = 'A';
            char akun2 = 'D';

            // akan dicari path dari akun1 ke akun2;

            // init node graph
            graph.AddNode(0);
            graph.AddNode(1);
            graph.AddNode(2);
            graph.AddNode(3);
            graph.AddNode(4);
            graph.AddNode(5);
            graph.AddNode(6);
            graph.AddNode(7);

            // init edge graph
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(1, 5);
            graph.AddEdge(2, 5);
            graph.AddEdge(2, 6);
            graph.AddEdge(3, 6);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 7);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 7);

            DFSearcher searcher = new DFSearcher();
            List<int> path = new List<int>();

            // call searcher to search path
            path = searcher.Search(akun.IndexOf(akun1), akun.IndexOf(akun2), graph);

            // print result
            Console.WriteLine("Problem : " + akun1 + " -> " + akun2);
            for (int i = 0; i < path.Count; i++)
            {
                Console.WriteLine(akun[path[i]]);
            }

            /*
             compile : csc DFSTester.cs DFSearcher.cs Searcher.cs Graph.cs
             */
        }
    }
}

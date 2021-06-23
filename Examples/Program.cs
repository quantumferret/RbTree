using System;
using RbTree;
using static System.Console;

namespace Examples {
    class Program {
        static void Display() {
            var tree = new RbTree<int>();
            var rng = new Random();
            for (int i = 0; i < 50; ++i)
                tree.Add(rng.Next(0, 100));
            tree.Print();
        }
        
        static void Main(string[] args) {
            Display();
        }
    }
}
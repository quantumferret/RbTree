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

        static void JoinExample() {
            var t1 = new RbTree<int>();
            t1.Add(3);
            t1.Add(2);
            t1.Add(4);
            t1.Add(1);
            t1.Add(5);
            t1.Add(0);
            t1.Add(-1);
            t1.Add(-2);
            t1.Add(-3);
            t1.Add(-4);
            t1.Add(-5);

            var t2 = new RbTree<int>();
            t2.Add(9);
            t2.Add(8);
            t2.Add(10);
            t2.Add(7);
            t2.Add(11);
            
            WriteLine($"Note: t1's black height > t2's black height in this example." + Environment.NewLine);
            var t3 = RbTree<int>.Join(t1, 6, t2);
            t3.Print();
        }

        static void SubtreeCheck() {
            var t1 = new RbTree<int>();
            t1.Add(3);
            t1.Add(2);
            t1.Add(4);
            t1.Add(1);
            t1.Add(5);
            t1.Add(0);
            t1.Add(-1);
            t1.Add(-2);
            t1.Add(-3);
            t1.Add(-4);
            t1.Add(-5);
            
            t1.Print();
            
            WriteLine(Environment.NewLine);

            var t2 = t1.Subtree(t1.Root.Left.Left);
            t2.Print();

            t1.Remove(-5);
            
            WriteLine(Environment.NewLine);

            t2.Print();
        }
        
        static void Main(string[] args) {
        }
    }
}
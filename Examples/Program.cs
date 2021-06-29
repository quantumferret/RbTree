using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RbTree;
using static System.Console;

namespace Examples {
    static class RandomSingleton
    {
        private static Random _instance;

        public static Random GetInstance()
        {
            return _instance ??= new Random();
        }
    }

    class Program {
        private static Stopwatch watch = new Stopwatch();

        static void Shuffle<T>(IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = RandomSingleton.GetInstance().Next(n + 1);
                T val = list[k];
                list[k] = list[n];
                list[n] = val;
            }
        }
        
        static void GenerateTree() {
            WriteLine("Enter a size, start value, and end value, separated by spaces.");
            var s = ReadLine()!.Trim();
            var l = s.Split().Select(x => int.Parse(x)).ToList();
            var (size, from, to) = (l[0], l[1], l[2]);
            var tree = new RbTree<int>();
            var rng = RandomSingleton.GetInstance();
            var toInclusive = to + 1;
            
            watch.Start();
            for (int i = 0; i < size; ++i) {
                int j = rng.Next(from, toInclusive);
                tree.Add(j);
            }
            watch.Stop();

            WriteLine($"Added {size} elements in {watch.ElapsedMilliseconds} milliseconds.");
            WriteLine("Display tree? (Y/n)");
            if (ReadLine()!.Trim().ToLower() == "y")
                tree.Print();
            var list = tree.InOrderKeys();
            Shuffle(list);
            
            watch.Restart();
            foreach (var key in list)
                tree.Remove(key);
            watch.Stop();
            WriteLine($"Removed {size} elements in {watch.ElapsedMilliseconds} milliseconds.");
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
            RbTree<int>.Node subroot = t1.Add(-3);
            t1.Add(-4);
            t1.Add(-5);
            
            t1.Print();
            
            WriteLine(Environment.NewLine);

            var t2 = t1.Subtree(subroot);
            t2.Print();

            t1.Remove(-5);
            
            WriteLine(Environment.NewLine);

            t2.Print();
        }
        
        static void Main(string[] args) {
            GenerateTree();
        }
    }
}
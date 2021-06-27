using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Console;

[assembly : InternalsVisibleTo("Tests")]

namespace RbTree {
    public class RbTree<T> where T : IComparable<T> {
        public class Node {
            public enum ColorEnum {
                Red,
                Black,
            }

            public Node Left;
            public Node Right;
            public Node Parent;
            public T Key {get; private set;}
            public override string ToString() => IsLeaf ? "Nil" : $"({Key.ToString()}: {Count}, {Color})";
            public ColorEnum Color;
            public readonly bool IsLeaf;
            public int Count {get; internal set;}

            public static Node Leaf() => Nil;
            private static readonly Node Nil = new();

            public Node() {
                Color = ColorEnum.Black;
                IsLeaf = true;
            }

            public Node(T key) {
                Key = key;
                Parent = Left = Right = Leaf();
                Count = 1;
            }

            public Node(T key, Node parent) {
                Key = key;
                Parent = parent;
                Left = Right = Leaf();
                Count = 1;
            }
        }


        public Node Root;
        public readonly Node Nil = Node.Leaf();
        internal int Bh;

        public RbTree() {
            Root = Nil;
            Bh = 0;
        }

        public RbTree(T key) {
            Root = new Node(key, Nil);
            Root.Left = Root.Right = Nil;
            Bh = 0;
        }

        public Node Minimum(Node subtreeRoot) {
            while (subtreeRoot.Left != Nil)
                subtreeRoot = subtreeRoot.Left;
            return subtreeRoot;
        }

        public Node Maximum(Node subtreeRoot) {
            while (subtreeRoot.Right != Nil)
                subtreeRoot = subtreeRoot.Right;
            return subtreeRoot;
        }

        public Node Predecessor(Node n) {
            if (n.Left != Nil)
                return Maximum(n.Left);
            var p = n.Parent;
            while (p != Nil && n == p.Left) {
                n = p;
                p = p.Parent;
            }
            return p;
        }

        public Node Successor(Node n) {
            if (n.Right != Nil)
                return Minimum(n.Right);
            var p = n.Parent;
            while (p != Nil && n == p.Right) {
                n = p;
                p = p.Parent;
            }
            return p;
        }

        public bool Contains(T key) {
            var node = Root;
            while (node != Nil) {
                if (key.CompareTo(node.Key) == 0)
                    return true;
                node = key.CompareTo(node.Key) < 0 ? node.Left : node.Right;
            }
            return false;
        }

        internal void LeftRotate(Node node) {
            Node temp = node.Right;
            node.Right = temp.Left;
            if (temp.Left != Nil)
                temp.Left.Parent = node;
            temp.Parent = node.Parent;
            if (node.Parent == Nil)
                Root = temp;
            else if (node == node.Parent.Left)
                node.Parent.Left = temp;
            else
                node.Parent.Right = temp;
            temp.Left = node;
            node.Parent = temp;
        }

        internal void RightRotate(Node node) {
            Node temp = node.Left;
            node.Left = temp.Right;
            if (temp.Right != Nil)
                temp.Right.Parent = node;
            temp.Parent = node.Parent;
            if (node.Parent == Nil)
                Root = temp;
            else if (node == node.Parent.Right)
                node.Parent.Right = temp;
            else
                node.Parent.Left = temp;
            temp.Right = node;
            node.Parent = temp;
        }

        private void InsertFixup(Node z) {
            while (z.Parent.Color == Node.ColorEnum.Red) {
                if (z.Parent == z.Parent.Parent.Left) {
                    Node y = z.Parent.Parent.Right;
                    if (y.Color == Node.ColorEnum.Red) {
                        z.Parent.Color = Node.ColorEnum.Black;
                        y.Color = Node.ColorEnum.Black;
                        z.Parent.Parent.Color = Node.ColorEnum.Red;
                        z = z.Parent.Parent;
                    } else {
                        if (z == z.Parent.Right) {
                            z = z.Parent;
                            LeftRotate(z);
                        }
                        z.Parent.Color = Node.ColorEnum.Black;
                        z.Parent.Parent.Color = Node.ColorEnum.Red;
                        RightRotate(z.Parent.Parent);
                    }
                } else {
                    Node y = z.Parent.Parent.Left;
                    if (y.Color == Node.ColorEnum.Red) {
                        z.Parent.Color = Node.ColorEnum.Black;
                        y.Color = Node.ColorEnum.Black;
                        z.Parent.Parent.Color = Node.ColorEnum.Red;
                        z = z.Parent.Parent;
                    } else {
                        if (z == z.Parent.Left) {
                            z = z.Parent;
                            RightRotate(z);
                        }
                        z.Parent.Color = Node.ColorEnum.Black;
                        z.Parent.Parent.Color = Node.ColorEnum.Red;
                        LeftRotate(z.Parent.Parent);
                    }
                }
            }
            Root.Color = Node.ColorEnum.Black;
            SetBlackHeight();
        }

        private void Insert(Node z) {
            Node y = Nil;
            Node x = Root;
            while (x != Nil) {
                y = x;
                x = z.Key.CompareTo(x.Key) < 0 ? x.Left : x.Right;
            }
            z.Parent = y;
            if (y == Nil)
                Root = z;
            else if (z.Key.CompareTo(y.Key) < 0)
                y.Left = z;
            else
                y.Right = z;
            z.Left = Nil;
            z.Right = Nil;
            z.Color = Node.ColorEnum.Red;
            InsertFixup(z);
        }

        public void Add(T key) {
            Node duplicate = Get(key);
            if (duplicate != Nil) {
                duplicate.Count += 1;
                return;
            }
            Node node = new Node(key);
            Insert(node);
            SetBlackHeight();
        }

        private void Transplant(Node u, Node v) {
            if (u.Parent == Nil)
                Root = v;
            else if (u == u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;
            v.Parent = u.Parent;
        }

        private void DeleteFixup(Node x) {
            while (x != Root && x.Color == Node.ColorEnum.Black) {
                if (x == x.Parent.Left) {
                    Node w = x.Parent.Right;
                    if (w.Color == Node.ColorEnum.Red) {
                        w.Color = Node.ColorEnum.Black;
                        x.Parent.Color = Node.ColorEnum.Red;
                        LeftRotate(x.Parent);
                        w = x.Parent.Right;
                    }
                    if (w.Left.Color == Node.ColorEnum.Black && w.Right.Color == Node.ColorEnum.Black) {
                        w.Color = Node.ColorEnum.Red;
                        x = x.Parent;
                    } else {
                        if (w.Right.Color == Node.ColorEnum.Black) {
                            w.Left.Color = Node.ColorEnum.Black;
                            w.Color = Node.ColorEnum.Red;
                            RightRotate(w);
                            w = x.Parent.Right;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = Node.ColorEnum.Black;
                        w.Right.Color = Node.ColorEnum.Black;
                        LeftRotate(x.Parent);
                        x = Root;
                    }
                } else {
                    Node w = x.Parent.Left;
                    if (w.Color == Node.ColorEnum.Red) {
                        w.Color = Node.ColorEnum.Black;
                        x.Parent.Color = Node.ColorEnum.Red;
                        RightRotate(x.Parent);
                        w = x.Parent.Left;
                    }
                    if (w.Right.Color == Node.ColorEnum.Black && w.Left.Color == Node.ColorEnum.Black) {
                        w.Color = Node.ColorEnum.Red;
                        x = x.Parent;
                    } else {
                        if (w.Left.Color == Node.ColorEnum.Black) {
                            w.Right.Color = Node.ColorEnum.Black;
                            w.Color = Node.ColorEnum.Red;
                            LeftRotate(w);
                            w = x.Parent.Left;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = Node.ColorEnum.Black;
                        w.Left.Color = Node.ColorEnum.Black;
                        RightRotate(x.Parent);
                        x = Root;
                    }
                }
            }
            x.Color = Node.ColorEnum.Black;
            SetBlackHeight();
        }

        internal void Delete(Node z) {
            Node y = z;
            Node.ColorEnum yOriginalColor = y.Color;
            Node x;
            if (z.Left == Nil) {
                x = z.Right;
                Transplant(z, z.Right);
            } else if (z.Right == Nil) {
                x = z.Left;
                Transplant(z, z.Left);
            } else {
                y = Minimum(z.Right);
                yOriginalColor = y.Color;
                x = y.Right;
                if (y.Parent == z)
                    x.Parent = y;
                else {
                    Transplant(y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }
                Transplant(z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
                y.Color = z.Color;
            }
            if (yOriginalColor == Node.ColorEnum.Black)
                DeleteFixup(x);
        }

        public void Remove(T key) {
            if (Root == Nil)
                return;
            Node del = Get(key);
            if (del == Nil)
                return;
            if (del.Count > 1) {
                del.Count -= 1;
                return;
            }
            Delete(del);
        }

        public Node Get(T key) {
            var node = Root;
            while (node != Nil && key.CompareTo(node.Key) != 0)
                node = key.CompareTo(node.Key) < 0 ? node.Left : node.Right;
            return node;
        }

        internal Node MaxWithBh(int blackHeight) {
            if (Bh == blackHeight)
                return Root;
            Node y = Root;
            int nodeBh = Bh;
            while (y != Nil) {
                y = y.Right;
                if (y.Color == Node.ColorEnum.Black && nodeBh == blackHeight)
                    return y;
                if (y.Color == Node.ColorEnum.Black && nodeBh != blackHeight)
                    nodeBh -= 1;
            }
            return Nil;
        }

        /*
         * Runs in O(log(n)) time, so using it in inserts and deletes doesn't change the asymptotic running time
         * of those operations.
         */
        internal void SetBlackHeight() {
            Node y = Root;
            Bh = 0;
            while (y != Nil) {
                y = y.Right != Nil ? y.Right : y.Left;
                if (y.Color == Node.ColorEnum.Black)
                    Bh += 1;
            }
        }

        public static RbTree<T> Join(RbTree<T> t1, T key, RbTree<T> t2) {
            T t1Max = t1.Maximum(t1.Root).Key;
            T t2Min = t2.Minimum(t2.Root).Key;
            if (key.CompareTo(t1Max) > 0 && key.CompareTo(t2Min) < 0) {
                if (t1.Bh >= t2.Bh) {
                    Node k = new Node(key) { Color = Node.ColorEnum.Red };
                    t1.Root.Parent = t2.Root.Parent = k;
                    k.Left = t1.Root;
                    k.Right = t2.Root;
                    t1.Root = k;
                    t1.InsertFixup(k);
                }
            }
            else {
                throw new ArgumentException(
                    "The join key must be greater than all keys in Tree 1 and less than all keys in Tree 2.");
            }
            return t1;
        }
        
        public static (RbTree<T> left, RbTree<T> right) Split(T t) {
            throw new NotImplementedException();
        }

        public static RbTree<T> Union(RbTree<T> t1, RbTree<T> t2) {
            throw new NotImplementedException();
        }

        public static RbTree<T> Intersect(RbTree<T> t1, RbTree<T> t2) {
            throw new NotImplementedException();
        }

        public static RbTree<T> SetDifference(RbTree<T> t1, RbTree<T> t2) {
            throw new NotImplementedException();
        }

        public void Print() {
            Queue<(int depth, Node node)> queue = new Queue<(int depth, Node node)>();
            List<(int depth, Node node)> list = new();
            queue.Enqueue((0, Root));

            while (queue.Count != 0) {
                (int level, Node temp) pair = queue.Dequeue();
                list.Add(pair);
                pair.level += 1;
                if (pair.temp.Left != Nil)
                    queue.Enqueue((pair.level, pair.temp.Left));
                if (pair.temp.Right != Nil)
                    queue.Enqueue((pair.level, pair.temp.Right));
            }
            
            list = list.OrderByDescending(p => p.node.Key).ToList();
            
            foreach (var n in list) {
                string pad = string.Concat(Enumerable.Repeat("    ", n.depth));
                Write(pad);
                BackgroundColor = ConsoleColor.White;
                ForegroundColor = n.node.Color == Node.ColorEnum.Red ? ConsoleColor.Red : ConsoleColor.Black;
                WriteLine($"{n.node.Key}: {n.node.Count}");
                ResetColor();
            }
        }
    }
}
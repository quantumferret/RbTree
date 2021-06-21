using System;

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
            public T Key {get; set;}
            public override string ToString() => IsLeaf? "Nil" : $"({Key.ToString()}, {Color})";
            public ColorEnum Color;
            public readonly bool IsLeaf;

            public static Node Leaf() => Nil;
            private static readonly Node Nil = new Node();

            internal Node() {
                Color = ColorEnum.Black;
                IsLeaf = true;
            }

            public Node(T t, Node parent) {
                Parent = parent;
                Left = Right = Leaf();
                Key = t;
            }
        }


        public Node Root;
        public readonly Node Nil = Node.Leaf();

        public RbTree() {
            Root = Nil;
        }

        public RbTree(T t) {
            Root = new Node(t, Nil);
            Root.Left = Root.Right = Nil;
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
        
        public void LeftRotate(Node node) {
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
        
        public void RightRotate(Node node) {
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
        }
        
        public void Insert(Node z) {
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

        private void DeleteFixup(ref Node n) {
            throw new NotImplementedException();
        }

        public bool Delete(T t) {
            throw new NotImplementedException();
        }

        public Node Search(T t) {
            var root = Root;
            while (root != Nil && t.CompareTo(root.Key) != 0)
                root = t.CompareTo(root.Key) < 0 ? root.Left : root.Right;
            return root;
        }

        public static RbTree<T> Join(RbTree<T> t1, RbTree<T> t2) {
            throw new NotImplementedException();
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
    }
}
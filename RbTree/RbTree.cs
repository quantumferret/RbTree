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

        private void InsertFixup(ref Node n) {
            throw new NotImplementedException();
        }

        private void DeleteFixup(ref Node n) {
            throw new NotImplementedException();
        }

        public bool Insert(T t) {
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
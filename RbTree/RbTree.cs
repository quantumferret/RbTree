using System;

namespace RbTree {
    public class RbTree<T> where T : IComparable<T> {

        public class Node {
            public enum ColorEnum {
                Red,
                Black,
            }
            
            public Node Left {get; set;}
            public Node Right {get; set;}
            public Node Parent {get; set;}
            public T Key {get; set;}
            public int BlackHeight {get; set;}
            public override string ToString() => Key.ToString();
            public ColorEnum Color {get; set;}

            public Node() {
            }

            public Node(T t, Node parent) {
                Parent = parent;
                Left = Right = null;
                Key = t;
            }
        }

        
        public Node Root {get; private set;}
        private readonly Node _nil = new Node();

        public RbTree() {
            Root = _nil; // TODO make sure to set root to something else on first insert...
        }

        public RbTree(T t) {
            Root = new Node(t, _nil);
            Root.Left = Root.Right = _nil;
        }

        public Node Minimum(Node subtreeRoot) {
            while (subtreeRoot.Left != _nil)
                subtreeRoot = subtreeRoot.Left;
            return subtreeRoot;
        }

        public Node Maximum(Node subtreeRoot) {
            while (subtreeRoot.Right != _nil)
                subtreeRoot = subtreeRoot.Right;
            return subtreeRoot;
        }
        
        public Node Predecessor(Node n) {
            if (n.Left != _nil)
                return Maximum(n.Left);
            var p = n.Parent;
            while (p != _nil && n == p.Left) {
                n = p;
                p = p.Parent;
            }
            return p;
        }

        public Node Successor(Node n) {
            if (n.Right != _nil)
                return Minimum(n.Right);
            var p = n.Parent;
            while (p != _nil && n == p.Right) {
                n = p;
                p = p.Parent;
            }
            return p;
        }
        
        private void LeftRotate(ref Node node) {
            Node temp = node.Right;
            node.Right = temp.Left;
            if (temp.Left != _nil)
                temp.Left.Parent = node;
            temp.Parent = node.Parent;
            if (node.Parent == _nil)
                Root = node;
            else if (node == node.Parent.Left)
                node.Parent.Left = temp;
            else
                node.Parent.Right = temp;
            temp.Left = node;
            node.Parent = temp;
        }
        
        private void RightRotate(ref Node node) {
            Node temp = node.Left;
            node.Left = temp.Right;
            if (temp.Right != _nil)
                temp.Right.Parent = node;
            temp.Parent = node.Parent;
            if (node.Parent == _nil)
                Root = node;
            else if (node == node.Parent.Right)
                node.Parent.Right = temp;
            else {
                node.Parent.Left = temp;
                temp.Right = node;
                node.Parent = temp;
            }
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
            while (root != _nil && t.CompareTo(root.Key) != 0)
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
using NUnit.Framework;
using RbTree;

namespace Tests {
    [TestFixture]
    public class TestBstMethods {
        private RbTree<int> tree;

        [SetUp]
        public void Setup() {
            tree = new RbTree<int>();
            tree.Add(8);
            tree.Add(5);
            tree.Add(9);
            tree.Add(2);
            tree.Add(6);
            tree.Add(11);
            tree.Add(1);
            tree.Add(0);
            tree.Add(-1);
            tree.Add(-2);
        }

        [Test]
        public void TestMinimum() {
            var min = tree.Minimum(tree.Root);
            Assert.AreEqual(-2, min.Key);

            var submin = tree.Minimum(tree.Root.Right);
            Assert.AreEqual(6, submin.Key);
        }

        [Test]
        public void TestMaximum() {
            var max = tree.Maximum(tree.Root);
            Assert.AreEqual(11, max.Key);

            var submax = tree.Maximum(tree.Root.Left);
            Assert.AreEqual(2, submax.Key);
        }

        [Test]
        public void TestGet() {
            var node = tree.Get(5);
            Assert.AreEqual(tree.Root, node);
            Assert.AreSame(tree.Root, node);

            node = tree.Get(2);
            Assert.AreEqual(tree.Root.Left.Right, node);
            Assert.AreSame(tree.Root.Left.Right, node);
        }

        [Test]
        public void TestPredecessor() {
            Assert.AreEqual(tree.Nil, tree.Predecessor(tree.Minimum(tree.Root)));
            Assert.AreSame(tree.Nil, tree.Predecessor(tree.Minimum(tree.Root)));
            
            Assert.AreEqual(tree.Root.Left.Right, tree.Predecessor(tree.Root));
            Assert.AreSame(tree.Root.Left.Right, tree.Predecessor(tree.Root));
            
            Assert.AreEqual(tree.Root.Left.Left, tree.Predecessor(tree.Get(0)));
            Assert.AreSame(tree.Root.Left.Left, tree.Predecessor(tree.Get(0)));
        }

        [Test]
        public void TestSuccessor() {
            Assert.AreEqual(tree.Nil, tree.Successor(tree.Maximum(tree.Root)));
            Assert.AreSame(tree.Nil, tree.Successor(tree.Maximum(tree.Root)));
            
            Assert.AreEqual(tree.Root.Right.Left, tree.Successor(tree.Root));
            Assert.AreSame(tree.Root.Right.Left, tree.Successor(tree.Root));
            
            Assert.AreEqual(tree.Root.Right.Right, tree.Successor(tree.Root.Right));
            Assert.AreSame(tree.Root.Right.Right, tree.Successor(tree.Root.Right));
        }

        [Test]
        public void TestContains() {
            Assert.IsTrue(tree.Contains(5));
            Assert.IsFalse(tree.Contains(10));
        }
    }
}
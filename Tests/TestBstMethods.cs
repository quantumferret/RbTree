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
    }
}
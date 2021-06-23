using NUnit.Framework;
using RbTree;

namespace Tests {
    [TestFixture]
    public class TestInserts {
        private RbTree<int> tree;

        [SetUp]
        public void Setup() {
            tree = new RbTree<int>();
        }

        [Test]
        public void TestInsert() {
            tree.Add(8);
            tree.Add(5);
            tree.Add(9);
            tree.Add(2);
            tree.Add(6);
            tree.Add(11);
            tree.Add(1);
            tree.Add(0);

            Assert.AreEqual(8, tree.Root.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Black, tree.Root.Color);
            Assert.AreEqual(5, tree.Root.Left.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Red, tree.Root.Left.Color);
            Assert.AreEqual(9, tree.Root.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Black, tree.Root.Right.Color);
            Assert.AreEqual(1, tree.Root.Left.Left.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Black, tree.Root.Left.Left.Color);
            Assert.AreEqual(6, tree.Root.Left.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Black, tree.Root.Left.Right.Color);
            Assert.AreEqual(11, tree.Root.Right.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Red, tree.Root.Right.Right.Color);
            Assert.AreEqual(0, tree.Root.Left.Left.Left.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Red, tree.Root.Left.Left.Left.Color);
            Assert.AreEqual(2, tree.Root.Left.Left.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Red, tree.Root.Left.Left.Right.Color);

            tree.Add(-1); // here, 5 becomes the root after rebalancing.
            tree.Add(-2);

            Assert.AreEqual(5, tree.Root.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Black, tree.Root.Color);
            Assert.AreEqual(1, tree.Root.Left.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Red, tree.Root.Left.Color);
            Assert.AreEqual(-1, tree.Root.Left.Left.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Black, tree.Root.Left.Left.Color);
            Assert.AreEqual(2, tree.Root.Left.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Black, tree.Root.Left.Right.Color);
            Assert.AreEqual(-2, tree.Root.Left.Left.Left.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Red, tree.Root.Left.Left.Left.Color);
            Assert.AreEqual(0, tree.Root.Left.Left.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Red, tree.Root.Left.Left.Right.Color);
            Assert.AreEqual(8, tree.Root.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Red, tree.Root.Right.Color);
            Assert.AreEqual(6, tree.Root.Right.Left.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Black, tree.Root.Right.Left.Color);
            Assert.AreEqual(9, tree.Root.Right.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Black, tree.Root.Right.Right.Color);
            Assert.AreEqual(11, tree.Root.Right.Right.Right.Key);
            Assert.AreEqual(RbTree<int>.Node.ColorEnum.Red, tree.Root.Right.Right.Right.Color);
        }
    }
}
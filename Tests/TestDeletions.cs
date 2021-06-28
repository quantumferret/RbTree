using System;
using NUnit.Framework;
using RbTree;

namespace Tests {
    [TestFixture]
    public class TestDeletions {
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
        public void TestDelete() {
            tree.Remove(5);
            Assert.AreEqual(6, tree.Root.Key);
            Assert.AreEqual(9, tree.Root.Right.Key);
            Assert.AreEqual(1, tree.Root.Left.Key);
            Assert.AreEqual(11, tree.Root.Right.Right.Key);
            Assert.AreEqual(8, tree.Root.Right.Left.Key);
            Assert.AreEqual(2, tree.Root.Left.Right.Key);
            Assert.AreEqual(-1, tree.Root.Left.Left.Key);
            Assert.AreEqual(0, tree.Root.Left.Left.Right.Key);
            Assert.AreEqual(-2, tree.Root.Left.Left.Left.Key);

            tree.Remove(1);
            Assert.AreEqual(6, tree.Root.Key);
            Assert.AreEqual(9, tree.Root.Right.Key);
            Assert.AreEqual(11, tree.Root.Right.Right.Key);
            Assert.AreEqual(8, tree.Root.Right.Left.Key);
            Assert.AreEqual(-1, tree.Root.Left.Key);
            Assert.AreEqual(-2, tree.Root.Left.Left.Key);
            Assert.AreEqual(2, tree.Root.Left.Right.Key);
            Assert.AreEqual(0, tree.Root.Left.Right.Left.Key);
            
            Assert.AreEqual(true, tree.Validate());
        }

    }
}
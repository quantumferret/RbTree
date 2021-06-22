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
            tree.Remove(8);
            System.Console.SetOut(TestContext.Progress);
            tree.Print();
            Assert.Pass();
        }

    }
}